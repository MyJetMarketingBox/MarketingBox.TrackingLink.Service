﻿// <auto-generated />
using MarketingBox.TrackingLink.Service.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketingBox.TrackingLink.Service.Postgres.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220411185952_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("trackinglink-service")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MarketingBox.TrackingLink.Service.Domain.Models.TrackingLink", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AffiliateId")
                        .HasColumnType("bigint");

                    b.Property<long>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<long>("ClickId")
                        .HasColumnType("bigint");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UniqueId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UniqueId");

                    b.ToTable("trackinglinks", "trackinglink-service");
                });

            modelBuilder.Entity("MarketingBox.TrackingLink.Service.Domain.Models.TrackingLink", b =>
                {
                    b.OwnsOne("MarketingBox.TrackingLink.Service.Domain.Models.LinkParameters", "LinkParameterNames", b1 =>
                        {
                            b1.Property<long>("TrackingLinkId")
                                .HasColumnType("bigint");

                            b1.Property<string>("ClickId")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.Property<string>("Language")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.Property<string>("MPC_1")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.Property<string>("MPC_2")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.Property<string>("MPC_3")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.Property<string>("MPC_4")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.HasKey("TrackingLinkId");

                            b1.ToTable("trackinglinks", "trackinglink-service");

                            b1.WithOwner()
                                .HasForeignKey("TrackingLinkId");
                        });

                    b.OwnsOne("MarketingBox.TrackingLink.Service.Domain.Models.LinkParameters", "LinkParameterValues", b1 =>
                        {
                            b1.Property<long>("TrackingLinkId")
                                .HasColumnType("bigint");

                            b1.Property<string>("ClickId")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.Property<string>("Language")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.Property<string>("MPC_1")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.Property<string>("MPC_2")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.Property<string>("MPC_3")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.Property<string>("MPC_4")
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)");

                            b1.HasKey("TrackingLinkId");

                            b1.ToTable("trackinglinks", "trackinglink-service");

                            b1.WithOwner()
                                .HasForeignKey("TrackingLinkId");
                        });

                    b.Navigation("LinkParameterNames");

                    b.Navigation("LinkParameterValues");
                });
#pragma warning restore 612, 618
        }
    }
}
