﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PARKIT_enterprise_final.Models.DBContext;

#nullable disable

namespace PARKIT_enterprise_final.Migrations
{
    [DbContext(typeof(PARKITDBContext))]
    partial class PARKITDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("PARKIT_enterprise_final.Models.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ListingId")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<double>("TotalCost")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Bookings");
                });

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
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("WalletId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PARKIT_enterprise_final.Models.Wallet", b =>
                {
                    b.Property<Guid>("WalletId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("WalletId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("PARKIT_enterprise_final.Models.Listing", b =>
                {
                    b.HasOne("PARKIT_enterprise_final.Models.User", "User")
                        .WithMany("Listings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PARKIT_enterprise_final.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("AddressId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Latitude")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Longitude")
                                .HasColumnType("TEXT");

                            b1.Property<string>("StreetAddress")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("AddressId");

                            b1.ToTable("Listings");

                            b1.WithOwner()
                                .HasForeignKey("AddressId");
                        });

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

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Images");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PARKIT_enterprise_final.Models.User", b =>
                {
                    b.OwnsOne("PARKIT_enterprise_final.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("AddressId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Latitude")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Longitude")
                                .HasColumnType("TEXT");

                            b1.Property<string>("StreetAddress")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("AddressId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("AddressId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("PARKIT_enterprise_final.Models.Wallet", b =>
                {
                    b.HasOne("PARKIT_enterprise_final.Models.User", null)
                        .WithOne("Wallet")
                        .HasForeignKey("PARKIT_enterprise_final.Models.Wallet", "WalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PARKIT_enterprise_final.Models.User", b =>
                {
                    b.Navigation("Listings");

                    b.Navigation("Wallet");
                });
#pragma warning restore 612, 618
        }
    }
}
