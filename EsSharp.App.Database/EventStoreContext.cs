using System;
using System.Collections.Generic;
using System.Linq;
using EsSharp.Storage;
using Microsoft.EntityFrameworkCore;

namespace EsSharp.App.Database
{
	public class EventStoreContext : DbContext, IEventDataStorage
	{
		public DbSet<SerializedEvent> Events { get; set; }

		public IEnumerable<SerializedEvent> Get(Guid aggregateId, int version = 0)
		{
			return this.Events.AsNoTracking().Where(x =>
				x.AggregateId == aggregateId
				&& x.ExpectedVersion >= version).ToList();
		}

		public void Add(SerializedEvent @event)
		{
			this.Events.Add(@event);
		}

		public void Add(IEnumerable<SerializedEvent> events)
		{
			this.Events.AddRange(events);
		}

		public void Commit()
		{
			this.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SerializedEvent>()
				.HasKey(e => new { e.AggregateId, e.ExpectedVersion });
		}
	}
}
