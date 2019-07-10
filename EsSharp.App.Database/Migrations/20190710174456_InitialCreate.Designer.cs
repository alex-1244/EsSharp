﻿// <auto-generated />
using System;
using EsSharp.App.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EsSharp.App.Database.Migrations
{
    [DbContext(typeof(EventStoreContext))]
    [Migration("20190710174456_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EsSharp.SerializedEvent", b =>
                {
                    b.Property<Guid>("AggregateId");

                    b.Property<int>("ExpectedVersion");

                    b.Property<Guid>("EventId");

                    b.Property<string>("EventType");

                    b.Property<byte[]>("data");

                    b.HasKey("AggregateId", "ExpectedVersion");

                    b.ToTable("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
