﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PARKIT_enterprise_final.Models.DBContext;

#nullable disable

namespace PARKIT_enterprise_final.Migrations
{
    [DbContext(typeof(PARKITDBContext))]
    [Migration("20231123173356_Initial-Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("PARKIT_enterprise_final.Models.Listing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Listings");
                });

            modelBuilder.Entity("PARKIT_enterprise_final.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PARKIT_enterprise_final.Models.Listing", b =>
                {
                    b.HasOne("PARKIT_enterprise_final.Models.User", "User")
                        .WithMany("Listings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("PARKIT_enterprise_final.Models.Image", "Images", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("TEXT");

                            b1.Property<byte[]>("ImageData")
                                .IsRequired()
                                .HasColumnType("BLOB");

                            b1.Property<Guid>("ListingId")
                                .HasColumnType("TEXT");

                            b1.HasKey("Id");

                            b1.HasIndex("ListingId");

                            b1.ToTable("Images");

                            b1.WithOwner("Listing")
                                .HasForeignKey("ListingId");

                            b1.Navigation("Listing");
                        });

                    b.OwnsOne("PARKIT_enterprise_final.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("ListingId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Latitude")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Longitude")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("StreetAddress")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("ListingId");

                            b1.ToTable("Listings");

                            b1.WithOwner()
                                .HasForeignKey("ListingId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Images");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PARKIT_enterprise_final.Models.User", b =>
                {
                    b.OwnsOne("PARKIT_enterprise_final.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Latitude")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Longitude")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("StreetAddress")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("PARKIT_enterprise_final.Models.Wallet", "Wallet", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("WalletId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("cardHolderName")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("cardNumber")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Wallet")
                        .IsRequired();
                });

            modelBuilder.Entity("PARKIT_enterprise_final.Models.User", b =>
                {
                    b.Navigation("Listings");
                });
#pragma warning restore 612, 618
        }
    }
}
