﻿using System.Reflection;
using EsSharp.App.Database;
using EsSharp.App.Domain;
using EsSharp.Projections;
using EsSharp.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EsSharp.App
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddDbContext<EventStoreContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("EventsDatabase"),
					sqlOptions =>
						sqlOptions.MigrationsAssembly(typeof(EventStoreContext).GetTypeInfo().Assembly.GetName().Name));
			});

			services.AddScoped<ProjectionsContainer>();
			services.AddScoped<SynchronizedAggregateStore>();
			services.AddScoped<IEventDataStorage, EventStoreContext>();
			services.AddTransient<IEventSerializer, BinaryEventSerializer>();
			services.AddTransient(x =>
				new ProjectionsDatabaseConnection(Configuration.GetConnectionString("ProjectionDatabase")));
			services.AddScoped<EventStore>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
