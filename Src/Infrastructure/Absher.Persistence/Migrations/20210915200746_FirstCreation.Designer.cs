﻿// <auto-generated />
using System;
using Absher.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Absher.Persistence.Migrations
{
    [DbContext(typeof(AbsherDbContext))]
    [Migration("20210915200746_FirstCreation")]
    partial class FirstCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Absher.Domain.Entities.Audit.AuditChangedData", b =>
                {
                    b.Property<Guid>("AuditDataChangesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChangeType")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ChangedColumns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdentifierSaveChangesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NewValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldValues")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimaryKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SchemaName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TableName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AuditDataChangesId");

                    b.HasIndex("ChangeType")
                        .HasDatabaseName("IX_AuditDataChangeType");

                    b.HasIndex("CreatedBy")
                        .HasDatabaseName("IX_CreatedBy");

                    b.HasIndex("CreatedDate")
                        .HasDatabaseName("IX_CreationDate");

                    b.HasIndex("PrimaryKey")
                        .HasDatabaseName("IX_PrimaryKey");

                    b.HasIndex("SchemaName")
                        .HasDatabaseName("IX_SchemaName");

                    b.HasIndex("TableName")
                        .HasDatabaseName("IX_TableName");

                    b.ToTable("AuditChangedData", "Audit");
                });

            modelBuilder.Entity("Absher.Domain.Entities.Audit.AuditUserAction", b =>
                {
                    b.Property<long>("AuditUserActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JsonData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("AuditUserActionId");

                    b.HasIndex("CreatedBy")
                        .HasDatabaseName("IX_CreatedBy");

                    b.HasIndex("CreatedDate")
                        .HasDatabaseName("IX_CreationDate");

                    b.ToTable("AuditUserAction", "Audit");
                });
#pragma warning restore 612, 618
        }
    }
}
