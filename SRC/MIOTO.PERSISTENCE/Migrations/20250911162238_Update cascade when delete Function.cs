using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MIOTO.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class UpdatecascadewhendeleteFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionInFunctions_Functions_FunctionId",
                table: "ActionInFunctions");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionInFunctions_Functions_FunctionId",
                table: "ActionInFunctions",
                column: "FunctionId",
                principalTable: "Functions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionInFunctions_Functions_FunctionId",
                table: "ActionInFunctions");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionInFunctions_Functions_FunctionId",
                table: "ActionInFunctions",
                column: "FunctionId",
                principalTable: "Functions",
                principalColumn: "Id");
        }
    }
}
