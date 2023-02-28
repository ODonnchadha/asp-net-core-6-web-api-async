using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Books.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2500, nullable: true),
                    AuthorId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("34379433-6c09-4538-bf0d-8cbace8c8019"), "Flann", "O'Brien" },
                    { new Guid("8104c5bf-befa-480a-97b6-c5d9311b34f8"), "William", "Faulkner" },
                    { new Guid("99d115c8-06be-49ef-ac56-6c324c72a707"), "James", "Joyce" },
                    { new Guid("a67882b2-21c0-48a7-89a8-b835ffe27a08"), "G.K.", "Chesterton" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("4d3d9fcf-ad9f-4c36-b8c4-7353b4cad664"), new Guid("a67882b2-21c0-48a7-89a8-b835ffe27a08"), "The dreary succession of randomly selected Kings of England is broken up when Auberon Quin, who cares for nothing but a good joke, is chosen. To amuse himself, he institutes elaborate costumes for the provosts of the districts of London", "The Napoleon Of Notting Hill" },
                    { new Guid("8dd56361-4243-4db9-b728-f1e1421704a3"), new Guid("99d115c8-06be-49ef-ac56-6c324c72a707"), "A Portrait of the Artist as a Young Man is the first novel of Irish writer James Joyce. A Künstlerroman written in a modernist style, it traces the religious and intellectual awakening of young Stephen Dedalus, Joyce's fictional alter ego, whose surname alludes to Daedalus, Greek mythology's consummate craftsman.", "A Portrait Of The Artist As A Young Man" },
                    { new Guid("c233e878-e224-46d9-b17b-34f36f736f59"), new Guid("8104c5bf-befa-480a-97b6-c5d9311b34f8"), "Absalom, Absalom! details the rise and fall of Thomas Sutpen, a white man born into poverty in western Virginia who moves to Mississippi with the dual aims of gaining wealth and becoming a powerful family patriarch.", "Absalom, Absalom!" },
                    { new Guid("dfd86c74-f89f-4530-8d0f-a2fd2da7e181"), new Guid("34379433-6c09-4538-bf0d-8cbace8c8019"), "The Third Policeman is set in rural Ireland and is narrated by a dedicated amateur scholar of de Selby, a scientist and philosopher.", "The Third Policeman" },
                    { new Guid("e9767333-49be-45b6-8518-a6d072920794"), new Guid("34379433-6c09-4538-bf0d-8cbace8c8019"), "At Swim-Two-Birds presents itself as a first-person story by an unnamed Irish student of literature. The student believes that 'one beginning and one ending for a book was a thing I did not agree with', and he accordingly sets three apparently quite separate stories in motion.", "At Swim-Two-Birds" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
