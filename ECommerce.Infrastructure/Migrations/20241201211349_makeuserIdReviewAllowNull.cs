using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class makeuserIdReviewAllowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_userId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_userId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_userId",
                table: "Reviews",
                column: "userId",
                unique: true,
                filter: "[userId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_userId",
                table: "Reviews",
                column: "userId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_userId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_userId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_userId",
                table: "Reviews",
                column: "userId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_userId",
                table: "Reviews",
                column: "userId",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
