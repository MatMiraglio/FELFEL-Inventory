﻿// <auto-generated />
using System;
using FELFEL.External.EntityFrameworkDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FELFELInventory.WebApi.Migrations
{
    [DbContext(typeof(FELFELContext))]
    [Migration("20190621132751_AddBatchChanges")]
    partial class AddBatchChanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FELFEL.Domain.Batch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Arrival");

                    b.Property<DateTime>("Expiration");

                    b.Property<long>("OriginalUnitAmount");

                    b.Property<int?>("ProductTypeId");

                    b.Property<long>("RemainingUnits");

                    b.HasKey("Id");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Batches");
                });

            modelBuilder.Entity("FELFEL.Domain.BatchChange", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BatchId");

                    b.Property<string>("Message");

                    b.Property<long>("NewAmount");

                    b.Property<long>("OldAmount");

                    b.Property<DateTime>("TimeOfChange");

                    b.HasKey("ID");

                    b.HasIndex("BatchId");

                    b.ToTable("BatchChanges");
                });

            modelBuilder.Entity("FELFEL.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FELFEL.Domain.Batch", b =>
                {
                    b.HasOne("FELFEL.Domain.Product", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId");
                });

            modelBuilder.Entity("FELFEL.Domain.BatchChange", b =>
                {
                    b.HasOne("FELFEL.Domain.Batch", "Batch")
                        .WithMany("History")
                        .HasForeignKey("BatchId");
                });
#pragma warning restore 612, 618
        }
    }
}