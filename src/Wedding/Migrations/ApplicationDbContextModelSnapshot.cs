﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Wedding.Data;

#nullable disable

namespace Wedding.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.Property<Guid>("PostsPageId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagsTagId")
                        .HasColumnType("uuid");

                    b.HasKey("PostsPageId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("Wedding.Data.Entities.AccomodationOptions", b =>
                {
                    b.Property<Guid>("AccomodationOptionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccomodationDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("AccomodationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AccomodationType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AccomodationUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PictureId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WeddingSetupId")
                        .HasColumnType("uuid");

                    b.HasKey("AccomodationOptionsId");

                    b.HasIndex("PictureId");

                    b.HasIndex("WeddingSetupId");

                    b.ToTable("AccomodationOptions");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Comment", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<Guid>("PostPageId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CommentId");

                    b.HasIndex("PostPageId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Guest", b =>
                {
                    b.Property<Guid>("GuestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AgeBracket")
                        .HasColumnType("integer");

                    b.Property<string>("Allergies")
                        .HasColumnType("text");

                    b.Property<int[]>("CommonRequirements")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("InviteAccepted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("IsAttending")
                        .HasColumnType("boolean");

                    b.Property<bool?>("IsAttendingRehersalDinner")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Other")
                        .HasColumnType("text");

                    b.Property<Guid?>("PartyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PictureId")
                        .HasColumnType("uuid");

                    b.HasKey("GuestId");

                    b.HasIndex("PartyId");

                    b.HasIndex("PictureId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Page", b =>
                {
                    b.Property<Guid>("PageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("WeddingSetupId")
                        .HasColumnType("uuid");

                    b.HasKey("PageId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.HasIndex("WeddingSetupId");

                    b.ToTable("Pages");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Page");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Wedding.Data.Entities.Party", b =>
                {
                    b.Property<Guid>("PartyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<bool>("InvitationOpened")
                        .HasColumnType("boolean");

                    b.Property<bool>("InvitationSent")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("InviteSentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsInvited")
                        .HasColumnType("boolean");

                    b.Property<string>("PartyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("SaveTheDateSent")
                        .HasColumnType("boolean");

                    b.Property<string>("UniqueInviteId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("WeddingSetupId")
                        .HasColumnType("uuid");

                    b.HasKey("PartyId");

                    b.HasIndex("WeddingSetupId");

                    b.ToTable("Party");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Picture", b =>
                {
                    b.Property<Guid>("PictureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AlternativeText")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<DateTime>("DateTimeUploadedUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FileDescription")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<byte[]>("FileHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OriginalFileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Permalink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("PixelsX")
                        .HasColumnType("bigint");

                    b.Property<long?>("PixelsY")
                        .HasColumnType("bigint");

                    b.Property<long?>("ThumbnailSize")
                        .HasColumnType("bigint");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("WeddingSetupId")
                        .HasColumnType("uuid");

                    b.HasKey("PictureId");

                    b.HasIndex("WeddingSetupId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Tag", b =>
                {
                    b.Property<Guid>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Wedding.Data.Entities.WeddingSetup", b =>
                {
                    b.Property<Guid>("WeddingSetupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PartnerOneId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PartnerTwoId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("WeddingSetupId");

                    b.HasIndex("PartnerOneId");

                    b.HasIndex("PartnerTwoId");

                    b.ToTable("WeddingSetups");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Post", b =>
                {
                    b.HasBaseType("Wedding.Data.Entities.Page");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.HasIndex("CategoryId");

                    b.HasDiscriminator().HasValue("Post");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostTag", b =>
                {
                    b.HasOne("Wedding.Data.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsPageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wedding.Data.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Wedding.Data.Entities.AccomodationOptions", b =>
                {
                    b.HasOne("Wedding.Data.Entities.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wedding.Data.Entities.WeddingSetup", "WeddingSetup")
                        .WithMany()
                        .HasForeignKey("WeddingSetupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Picture");

                    b.Navigation("WeddingSetup");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Comment", b =>
                {
                    b.HasOne("Wedding.Data.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostPageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Guest", b =>
                {
                    b.HasOne("Wedding.Data.Entities.Party", "Party")
                        .WithMany("Guests")
                        .HasForeignKey("PartyId");

                    b.HasOne("Wedding.Data.Entities.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");

                    b.Navigation("Party");

                    b.Navigation("Picture");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Page", b =>
                {
                    b.HasOne("Wedding.Data.Entities.WeddingSetup", "WeddingSetup")
                        .WithMany()
                        .HasForeignKey("WeddingSetupId");

                    b.Navigation("WeddingSetup");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Party", b =>
                {
                    b.HasOne("Wedding.Data.Entities.WeddingSetup", "WeddingSetup")
                        .WithMany()
                        .HasForeignKey("WeddingSetupId");

                    b.Navigation("WeddingSetup");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Picture", b =>
                {
                    b.HasOne("Wedding.Data.Entities.WeddingSetup", "Wedding")
                        .WithMany()
                        .HasForeignKey("WeddingSetupId");

                    b.Navigation("Wedding");
                });

            modelBuilder.Entity("Wedding.Data.Entities.WeddingSetup", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "PartnerOne")
                        .WithMany()
                        .HasForeignKey("PartnerOneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "PartnerTwo")
                        .WithMany()
                        .HasForeignKey("PartnerTwoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PartnerOne");

                    b.Navigation("PartnerTwo");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Post", b =>
                {
                    b.HasOne("Wedding.Data.Entities.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Category", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Party", b =>
                {
                    b.Navigation("Guests");
                });

            modelBuilder.Entity("Wedding.Data.Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
