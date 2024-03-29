﻿// <auto-generated />
using System;
using KingTransports.FleetService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KingTransports.FleetService.Migrations
{
    [DbContext(typeof(FleetDbContext))]
    partial class FleetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KingTransports.FleetService.Entities.FleetVehicle", b =>
                {
                    b.Property<Guid>("FleetVehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("InServiceFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("InServiceTo")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("VehicleId")
                        .HasColumnType("uuid");

                    b.Property<int>("VehicleOperabilityStatus")
                        .HasColumnType("integer");

                    b.Property<string>("VehicleVin")
                        .HasColumnType("text");

                    b.HasKey("FleetVehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("FleetVehicles");
                });

            modelBuilder.Entity("KingTransports.FleetService.Entities.FleetVehicleMaintanceLog", b =>
                {
                    b.Property<Guid>("FleetVehicleMaintanceLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FleetVehicleId")
                        .HasColumnType("uuid");

                    b.Property<int>("MaintainceType")
                        .HasColumnType("integer");

                    b.Property<decimal>("MaintanceCost")
                        .HasColumnType("numeric");

                    b.Property<string>("MaintanceDescription")
                        .HasColumnType("text");

                    b.Property<DateTime>("MaintanceFinishedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("MaintanceStartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("NewVehicleOperabilityStatus")
                        .HasColumnType("integer");

                    b.Property<decimal>("OdometerKm")
                        .HasColumnType("numeric");

                    b.HasKey("FleetVehicleMaintanceLogId");

                    b.HasIndex("FleetVehicleId");

                    b.ToTable("FleetVehicleMaintanceLog");
                });

            modelBuilder.Entity("KingTransports.FleetService.Entities.FleetVehicleMetric", b =>
                {
                    b.Property<Guid>("FleetVehicleMetricId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FleetVehicleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LoggedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("OdometerKm")
                        .HasColumnType("numeric");

                    b.HasKey("FleetVehicleMetricId");

                    b.HasIndex("FleetVehicleId");

                    b.ToTable("FleetVehicleMetrics");
                });

            modelBuilder.Entity("KingTransports.FleetService.Entities.Vehicle", b =>
                {
                    b.Property<Guid>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("MaintanceKm")
                        .HasColumnType("integer");

                    b.Property<int>("MaintanceMonths")
                        .HasColumnType("integer");

                    b.Property<string>("Make")
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .HasColumnType("text");

                    b.Property<int>("VehicleType")
                        .HasColumnType("integer");

                    b.HasKey("VehicleId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("MassTransit.EntityFrameworkCoreIntegration.InboxState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("Consumed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ConsumerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Delivered")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("LastSequenceNumber")
                        .HasColumnType("bigint");

                    b.Property<Guid>("LockId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<int>("ReceiveCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Received")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasAlternateKey("MessageId", "ConsumerId");

                    b.HasIndex("Delivered");

                    b.ToTable("InboxState");
                });

            modelBuilder.Entity("MassTransit.EntityFrameworkCoreIntegration.OutboxMessage", b =>
                {
                    b.Property<long>("SequenceNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("SequenceNumber"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid?>("ConversationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CorrelationId")
                        .HasColumnType("uuid");

                    b.Property<string>("DestinationAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime?>("EnqueueTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FaultAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Headers")
                        .HasColumnType("text");

                    b.Property<Guid?>("InboxConsumerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InboxMessageId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InitiatorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OutboxId")
                        .HasColumnType("uuid");

                    b.Property<string>("Properties")
                        .HasColumnType("text");

                    b.Property<Guid?>("RequestId")
                        .HasColumnType("uuid");

                    b.Property<string>("ResponseAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SourceAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("SequenceNumber");

                    b.HasIndex("EnqueueTime");

                    b.HasIndex("ExpirationTime");

                    b.HasIndex("OutboxId", "SequenceNumber")
                        .IsUnique();

                    b.HasIndex("InboxMessageId", "InboxConsumerId", "SequenceNumber")
                        .IsUnique();

                    b.ToTable("OutboxMessage");
                });

            modelBuilder.Entity("MassTransit.EntityFrameworkCoreIntegration.OutboxState", b =>
                {
                    b.Property<Guid>("OutboxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("Delivered")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("LastSequenceNumber")
                        .HasColumnType("bigint");

                    b.Property<Guid>("LockId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("OutboxId");

                    b.HasIndex("Created");

                    b.ToTable("OutboxState");
                });

            modelBuilder.Entity("KingTransports.FleetService.Entities.FleetVehicle", b =>
                {
                    b.HasOne("KingTransports.FleetService.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("KingTransports.FleetService.Entities.FleetVehicleMaintanceLog", b =>
                {
                    b.HasOne("KingTransports.FleetService.Entities.FleetVehicle", "FleetVehicle")
                        .WithMany()
                        .HasForeignKey("FleetVehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FleetVehicle");
                });

            modelBuilder.Entity("KingTransports.FleetService.Entities.FleetVehicleMetric", b =>
                {
                    b.HasOne("KingTransports.FleetService.Entities.FleetVehicle", "FleetVehicle")
                        .WithMany()
                        .HasForeignKey("FleetVehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FleetVehicle");
                });
#pragma warning restore 612, 618
        }
    }
}
