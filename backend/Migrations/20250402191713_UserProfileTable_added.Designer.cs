﻿// <auto-generated />
using System;
using JwtBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20250402191713_UserProfileTable_added")]
    partial class UserProfileTable_added
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("backend.Data.DataModels.RefreshSession", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionId"));

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("SessionId");

                    b.HasIndex("RefreshToken")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("RefreshSessions", (string)null);
                });

            modelBuilder.Entity("backend.Data.DataModels.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("backend.Data.DataModels.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<bool>("RememberMe")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("backend.Data.DataModels.UserProfile", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("ProfileImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UsersProfile", (string)null);
                });

            modelBuilder.Entity("backend.Data.DataModels.UserRoles", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UsersRoles");
                });

            modelBuilder.Entity("backend.Data.DataModels.RefreshSession", b =>
                {
                    b.HasOne("backend.Data.DataModels.User", "User")
                        .WithMany("Sessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Data.DataModels.UserProfile", b =>
                {
                    b.HasOne("backend.Data.DataModels.User", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("backend.Data.DataModels.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Data.DataModels.UserRoles", b =>
                {
                    b.HasOne("backend.Data.DataModels.Role", "Role")
                        .WithMany("Roles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("backend.Data.DataModels.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("backend.Data.DataModels.Role", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("backend.Data.DataModels.User", b =>
                {
                    b.Navigation("Sessions");

                    b.Navigation("UserProfile")
                        .IsRequired();

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
