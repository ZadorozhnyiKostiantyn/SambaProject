using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SambaProject.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccessRoles",
                columns: table => new
                {
                    AccessRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRoles", x => x.AccessRoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccessRules",
                columns: table => new
                {
                    AccessRuleRolesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Copy = table.Column<int>(type: "int", nullable: false),
                    Download = table.Column<int>(type: "int", nullable: false),
                    Write = table.Column<int>(type: "int", nullable: false),
                    Read = table.Column<int>(type: "int", nullable: false),
                    WriteContents = table.Column<int>(type: "int", nullable: false),
                    Upload = table.Column<int>(type: "int", nullable: false),
                    IsFile = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRules", x => x.AccessRuleRolesId);
                    table.ForeignKey(
                        name: "FK_AccessRules_AccessRoles_AccessRoleId",
                        column: x => x.AccessRoleId,
                        principalTable: "AccessRoles",
                        principalColumn: "AccessRoleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccessRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_AccessRoles_AccessRoleId",
                        column: x => x.AccessRoleId,
                        principalTable: "AccessRoles",
                        principalColumn: "AccessRoleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRules_AccessRoleId",
                table: "AccessRules",
                column: "AccessRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccessRoleId",
                table: "Users",
                column: "AccessRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AccessRoles");
        }
    }
}
