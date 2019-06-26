﻿// <auto-generated />
using System;
using Bookify.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bookify.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Bookify.API.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Adults");

                    b.Property<bool>("AdvancePaid");

                    b.Property<int>("Animals");

                    b.Property<DateTime>("BookingDate");

                    b.Property<int?>("ClientId");

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<int?>("HouseId");

                    b.Property<bool>("IsPaid");

                    b.Property<int>("Kids");

                    b.HasKey("BookingId");

                    b.HasIndex("ClientId");

                    b.HasIndex("HouseId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Bookify.API.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Lastname");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Bookify.API.Models.Equipment", b =>
                {
                    b.Property<int>("EquipmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("HouseId");

                    b.Property<string>("Name");

                    b.HasKey("EquipmentId");

                    b.HasIndex("HouseId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("Bookify.API.Models.House", b =>
                {
                    b.Property<int>("HouseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApartmentNumber");

                    b.Property<int>("Bathrooms");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Description");

                    b.Property<int>("Floors");

                    b.Property<string>("HouseNumber");

                    b.Property<decimal>("Price");

                    b.Property<int>("Rooms");

                    b.Property<string>("Street");

                    b.Property<string>("Title");

                    b.HasKey("HouseId");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("Bookify.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Lastname");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Bookify.API.Models.Booking", b =>
                {
                    b.HasOne("Bookify.API.Models.Client", "Client")
                        .WithMany("Bookings")
                        .HasForeignKey("ClientId");

                    b.HasOne("Bookify.API.Models.House", "House")
                        .WithMany("Bookings")
                        .HasForeignKey("HouseId");
                });

            modelBuilder.Entity("Bookify.API.Models.Equipment", b =>
                {
                    b.HasOne("Bookify.API.Models.House")
                        .WithMany("Equipment")
                        .HasForeignKey("HouseId");
                });
#pragma warning restore 612, 618
        }
    }
}
