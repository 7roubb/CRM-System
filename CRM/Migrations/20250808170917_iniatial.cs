using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Migrations
{
    public partial class iniatial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Task_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todo_Desc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo_Desc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Todo_Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    User_Status_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Users_Status_User_Status_ID",
                        column: x => x.User_Status_ID,
                        principalTable: "Users_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contact_Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_First = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_Middle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_Last = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_of_Initial_Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street_1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street_2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Linkedin_Profile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sales_Rep_ID = table.Column<int>(type: "int", nullable: false),
                    Sales_RepUserId = table.Column<int>(type: "int", nullable: false),
                    Contact_Status_ID = table.Column<int>(type: "int", nullable: false),
                    Contact_StatusId = table.Column<int>(type: "int", nullable: false),
                    Project_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proposal_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proposal_Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deliverables = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Contact_Status_Contact_StatusId",
                        column: x => x.Contact_StatusId,
                        principalTable: "Contact_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contact_Users_Sales_RepUserId",
                        column: x => x.Sales_RepUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNew_Todo = table.Column<bool>(type: "bit", nullable: false),
                    Todo_Type_ID = table.Column<int>(type: "int", nullable: false),
                    Todo_Desc_ID = table.Column<int>(type: "int", nullable: false),
                    Task_Status_ID = table.Column<int>(type: "int", nullable: false),
                    Sales_Rep_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Task_Status_Task_Status_ID",
                        column: x => x.Task_Status_ID,
                        principalTable: "Task_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Todo_Desc_Todo_Desc_ID",
                        column: x => x.Todo_Desc_ID,
                        principalTable: "Todo_Desc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Todo_Type_Todo_Type_ID",
                        column: x => x.Todo_Type_ID,
                        principalTable: "Todo_Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Users_Sales_Rep_ID",
                        column: x => x.Sales_Rep_ID,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Contact_StatusId",
                table: "Contact",
                column: "Contact_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Sales_RepUserId",
                table: "Contact",
                column: "Sales_RepUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Sales_Rep_ID",
                table: "Notes",
                column: "Sales_Rep_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Task_Status_ID",
                table: "Notes",
                column: "Task_Status_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Todo_Desc_ID",
                table: "Notes",
                column: "Todo_Desc_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Todo_Type_ID",
                table: "Notes",
                column: "Todo_Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_User_Status_ID",
                table: "Users",
                column: "User_Status_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Contact_Status");

            migrationBuilder.DropTable(
                name: "Task_Status");

            migrationBuilder.DropTable(
                name: "Todo_Desc");

            migrationBuilder.DropTable(
                name: "Todo_Type");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users_Status");
        }
    }
}
