using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeSocial.Infrastructure.Migrations
{
    public partial class CriacaoTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerfilId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Perfis_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacionamentos",
                columns: table => new
                {
                    PerfilIdA = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerfilIdB = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacionamentos", x => new { x.PerfilIdA, x.PerfilIdB });
                    table.ForeignKey(
                        name: "FK_Relacionamentos_Perfis_PerfilIdA",
                        column: x => x.PerfilIdA,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relacionamentos_Perfis_PerfilIdB",
                        column: x => x.PerfilIdB,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerfilId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Perfis_PerfilId",
                        column: x => x.PerfilId,
                        principalTable: "Perfis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PerfilId",
                table: "Comentarios",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PostId",
                table: "Comentarios",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PerfilId",
                table: "Posts",
                column: "PerfilId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacionamentos_PerfilIdB",
                table: "Relacionamentos",
                column: "PerfilIdB");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Relacionamentos");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Perfis");
        }
    }
}
