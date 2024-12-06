using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application_expenses.Migrations
{
    /// <inheritdoc />
    public partial class AddBudgetToUserV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budget_Users_UserId",
                table: "Budget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Budget",
                table: "Budget");

            migrationBuilder.RenameTable(
                name: "Budget",
                newName: "Budgets");

            migrationBuilder.RenameIndex(
                name: "IX_Budget_UserId",
                table: "Budgets",
                newName: "IX_Budgets_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Budgets",
                table: "Budgets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Users_UserId",
                table: "Budgets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Users_UserId",
                table: "Budgets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Budgets",
                table: "Budgets");

            migrationBuilder.RenameTable(
                name: "Budgets",
                newName: "Budget");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_UserId",
                table: "Budget",
                newName: "IX_Budget_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Budget",
                table: "Budget",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Budget_Users_UserId",
                table: "Budget",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
