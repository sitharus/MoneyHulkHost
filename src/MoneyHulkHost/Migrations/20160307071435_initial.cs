using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace MoneyHulkHost.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    Kind = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });
            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    BudgetId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.BudgetId);
                });
            migrationBuilder.CreateTable(
                name: "Import",
                columns: table => new
                {
                    ImportId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImportDateUTC = table.Column<DateTime>(nullable: false),
                    StatementDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import", x => x.ImportId);
                });
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    BudgetBudgetId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsIncome = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Budget_BudgetBudgetId",
                        column: x => x.BudgetBudgetId,
                        principalTable: "Budget",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Budget_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Budget",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "ImportLine",
                columns: table => new
                {
                    ImportLineId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImportImportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportLine", x => x.ImportLineId);
                    table.ForeignKey(
                        name: "FK_ImportLine_Import_ImportImportId",
                        column: x => x.ImportImportId,
                        principalTable: "Import",
                        principalColumn: "ImportId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AccountLine",
                columns: table => new
                {
                    AccountLineId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountAccountId = table.Column<int>(nullable: false),
                    CategoryCategoryId = table.Column<int>(nullable: true),
                    ImportedFromImportLineId = table.Column<int>(nullable: true),
                    StatementImportId = table.Column<int>(nullable: true),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLine", x => x.AccountLineId);
                    table.ForeignKey(
                        name: "FK_AccountLine_Account_AccountAccountId",
                        column: x => x.AccountAccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountLine_Category_CategoryCategoryId",
                        column: x => x.CategoryCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountLine_ImportLine_ImportedFromImportLineId",
                        column: x => x.ImportedFromImportLineId,
                        principalTable: "ImportLine",
                        principalColumn: "ImportLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountLine_Import_StatementImportId",
                        column: x => x.StatementImportId,
                        principalTable: "Import",
                        principalColumn: "ImportId",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Category",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AccountLine");
            migrationBuilder.DropTable("Account");
            migrationBuilder.DropTable("Category");
            migrationBuilder.DropTable("ImportLine");
            migrationBuilder.DropTable("Budget");
            migrationBuilder.DropTable("Import");
        }
    }
}
