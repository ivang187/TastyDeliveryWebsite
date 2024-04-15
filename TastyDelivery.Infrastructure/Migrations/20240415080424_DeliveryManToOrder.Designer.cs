﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TastyDelivery.Infrastructure.Data;

#nullable disable

namespace TastyDelivery.Infrastructure.Migrations
{
    [DbContext(typeof(TastyDeliveryDbContext))]
    [Migration("20240415080424_DeliveryManToOrder")]
    partial class DeliveryManToOrder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.IdentityModels.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HomeAddress")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DeliveryManId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HomeAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryManId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.OrderProducts", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 0,
                            Description = "Crispy Chicken Fillet, Tomato, Iceberg Lettuce, Mayonnaise, Burger Bun, Fries",
                            Name = "Chicken Burger"
                        },
                        new
                        {
                            Id = 2,
                            Category = 0,
                            Description = "Beef Patty, Pickles, Caramelized Onion, Sauce, Burger Bun, Fries",
                            Name = "Beef Burger"
                        },
                        new
                        {
                            Id = 3,
                            Category = 0,
                            Description = "300g",
                            Name = "Pork Schnitzel with Fries"
                        },
                        new
                        {
                            Id = 4,
                            Category = 1,
                            Description = "Fresh Tomatoes, Cucumber, Bell Peppers, Feta Cheese, Olives",
                            Name = "Shopska Salad"
                        },
                        new
                        {
                            Id = 5,
                            Category = 3,
                            Description = "300ml",
                            Name = "Chicken Soup"
                        },
                        new
                        {
                            Id = 6,
                            Category = 2,
                            Description = "120g",
                            Name = "Cheesecake"
                        },
                        new
                        {
                            Id = 7,
                            Category = 3,
                            Description = "300ml",
                            Name = "Shkembe Chorba"
                        },
                        new
                        {
                            Id = 8,
                            Category = 0,
                            Description = "Pasta, Pancetta, Parmesan",
                            Name = "Pasta Carbonara"
                        },
                        new
                        {
                            Id = 9,
                            Category = 2,
                            Description = "100g",
                            Name = "Chocolate Cake"
                        },
                        new
                        {
                            Id = 10,
                            Category = 1,
                            Description = "Romaine Lettuce, Parmesan Cheese, Croutons, Dressing",
                            Name = "Caesar Salad"
                        },
                        new
                        {
                            Id = 11,
                            Category = 0,
                            Description = "200g",
                            Name = "Grilled Trout"
                        },
                        new
                        {
                            Id = 12,
                            Category = 0,
                            Description = "100g",
                            Name = "Meatball"
                        },
                        new
                        {
                            Id = 13,
                            Category = 0,
                            Description = "150g",
                            Name = "Chicken bites with Cornflakes"
                        });
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.ProductsRestaurants", b =>
                {
                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("RestaurantId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductsRestaurants");

                    b.HasData(
                        new
                        {
                            RestaurantId = 1,
                            ProductId = 1,
                            Price = 10.5
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 1,
                            Price = 9.6999999999999993
                        },
                        new
                        {
                            RestaurantId = 1,
                            ProductId = 2,
                            Price = 11.9
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 2,
                            Price = 10.5
                        },
                        new
                        {
                            RestaurantId = 1,
                            ProductId = 3,
                            Price = 15.5
                        },
                        new
                        {
                            RestaurantId = 2,
                            ProductId = 3,
                            Price = 15.6
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 3,
                            Price = 15.5
                        },
                        new
                        {
                            RestaurantId = 2,
                            ProductId = 4,
                            Price = 9.0999999999999996
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 4,
                            Price = 8.0
                        },
                        new
                        {
                            RestaurantId = 1,
                            ProductId = 5,
                            Price = 4.5
                        },
                        new
                        {
                            RestaurantId = 2,
                            ProductId = 5,
                            Price = 4.7000000000000002
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 5,
                            Price = 4.5
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 6,
                            Price = 4.9000000000000004
                        },
                        new
                        {
                            RestaurantId = 2,
                            ProductId = 7,
                            Price = 6.2000000000000002
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 7,
                            Price = 5.0
                        },
                        new
                        {
                            RestaurantId = 1,
                            ProductId = 8,
                            Price = 9.5
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 8,
                            Price = 9.5
                        },
                        new
                        {
                            RestaurantId = 2,
                            ProductId = 9,
                            Price = 7.9000000000000004
                        },
                        new
                        {
                            RestaurantId = 1,
                            ProductId = 10,
                            Price = 11.9
                        },
                        new
                        {
                            RestaurantId = 2,
                            ProductId = 10,
                            Price = 12.4
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 10,
                            Price = 10.800000000000001
                        },
                        new
                        {
                            RestaurantId = 1,
                            ProductId = 11,
                            Price = 14.5
                        },
                        new
                        {
                            RestaurantId = 2,
                            ProductId = 11,
                            Price = 14.9
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 11,
                            Price = 11.800000000000001
                        },
                        new
                        {
                            RestaurantId = 2,
                            ProductId = 12,
                            Price = 3.2999999999999998
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 12,
                            Price = 2.5
                        },
                        new
                        {
                            RestaurantId = 2,
                            ProductId = 13,
                            Price = 10.800000000000001
                        },
                        new
                        {
                            RestaurantId = 3,
                            ProductId = 13,
                            Price = 8.8000000000000007
                        });
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OrderCount")
                        .HasColumnType("int");

                    b.Property<string>("WorkingHours")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Location = "Iskar Boulevard 65, Samokov",
                            Name = "Mishel Bar & Dinner",
                            OrderCount = 0,
                            WorkingHours = "8:00-23:00"
                        },
                        new
                        {
                            Id = 2,
                            Location = "Tourist Garden Park 82, Samokov",
                            Name = "Mehana Pri Sote",
                            OrderCount = 0,
                            WorkingHours = "8:00-0:00"
                        },
                        new
                        {
                            Id = 3,
                            Location = "Tsar Boris III Boulevard 127A, Samokov",
                            Name = "Delight Bar & Dinner",
                            OrderCount = 0,
                            WorkingHours = "8:00-23:00"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.IdentityModels.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.Order", b =>
                {
                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.IdentityModels.ApplicationUser", "DeliveryMan")
                        .WithMany()
                        .HasForeignKey("DeliveryManId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.IdentityModels.ApplicationUser", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryMan");

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.OrderProducts", b =>
                {
                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.Product", "Product")
                        .WithMany("Products")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.ProductsRestaurants", b =>
                {
                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TastyDelivery.Infrastructure.Data.Models.Restaurant", "Restaurant")
                        .WithMany("Products")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.IdentityModels.ApplicationUser", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.Product", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TastyDelivery.Infrastructure.Data.Models.Restaurant", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
