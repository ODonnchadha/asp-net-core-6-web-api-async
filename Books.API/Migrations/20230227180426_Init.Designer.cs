﻿// <auto-generated />
using System;
using Books.API.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Books.API.Migrations
{
    [DbContext(typeof(BookContext))]
    [Migration("20230227180426_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("Books.API.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = new Guid("34379433-6c09-4538-bf0d-8cbace8c8019"),
                            FirstName = "Flann",
                            LastName = "O'Brien"
                        },
                        new
                        {
                            Id = new Guid("8104c5bf-befa-480a-97b6-c5d9311b34f8"),
                            FirstName = "William",
                            LastName = "Faulkner"
                        },
                        new
                        {
                            Id = new Guid("99d115c8-06be-49ef-ac56-6c324c72a707"),
                            FirstName = "James",
                            LastName = "Joyce"
                        },
                        new
                        {
                            Id = new Guid("a67882b2-21c0-48a7-89a8-b835ffe27a08"),
                            FirstName = "G.K.",
                            LastName = "Chesterton"
                        });
                });

            modelBuilder.Entity("Books.API.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(2500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e9767333-49be-45b6-8518-a6d072920794"),
                            AuthorId = new Guid("34379433-6c09-4538-bf0d-8cbace8c8019"),
                            Description = "At Swim-Two-Birds presents itself as a first-person story by an unnamed Irish student of literature. The student believes that 'one beginning and one ending for a book was a thing I did not agree with', and he accordingly sets three apparently quite separate stories in motion.",
                            Title = "At Swim-Two-Birds"
                        },
                        new
                        {
                            Id = new Guid("dfd86c74-f89f-4530-8d0f-a2fd2da7e181"),
                            AuthorId = new Guid("34379433-6c09-4538-bf0d-8cbace8c8019"),
                            Description = "The Third Policeman is set in rural Ireland and is narrated by a dedicated amateur scholar of de Selby, a scientist and philosopher.",
                            Title = "The Third Policeman"
                        },
                        new
                        {
                            Id = new Guid("c233e878-e224-46d9-b17b-34f36f736f59"),
                            AuthorId = new Guid("8104c5bf-befa-480a-97b6-c5d9311b34f8"),
                            Description = "Absalom, Absalom! details the rise and fall of Thomas Sutpen, a white man born into poverty in western Virginia who moves to Mississippi with the dual aims of gaining wealth and becoming a powerful family patriarch.",
                            Title = "Absalom, Absalom!"
                        },
                        new
                        {
                            Id = new Guid("8dd56361-4243-4db9-b728-f1e1421704a3"),
                            AuthorId = new Guid("99d115c8-06be-49ef-ac56-6c324c72a707"),
                            Description = "A Portrait of the Artist as a Young Man is the first novel of Irish writer James Joyce. A Künstlerroman written in a modernist style, it traces the religious and intellectual awakening of young Stephen Dedalus, Joyce's fictional alter ego, whose surname alludes to Daedalus, Greek mythology's consummate craftsman.",
                            Title = "A Portrait Of The Artist As A Young Man"
                        },
                        new
                        {
                            Id = new Guid("4d3d9fcf-ad9f-4c36-b8c4-7353b4cad664"),
                            AuthorId = new Guid("a67882b2-21c0-48a7-89a8-b835ffe27a08"),
                            Description = "The dreary succession of randomly selected Kings of England is broken up when Auberon Quin, who cares for nothing but a good joke, is chosen. To amuse himself, he institutes elaborate costumes for the provosts of the districts of London",
                            Title = "The Napoleon Of Notting Hill"
                        });
                });

            modelBuilder.Entity("Books.API.Entities.Book", b =>
                {
                    b.HasOne("Books.API.Entities.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });
#pragma warning restore 612, 618
        }
    }
}
