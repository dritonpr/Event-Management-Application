using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class userFeedback_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFeedback_EventId",
                table: "UserFeedback");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedback_EventId",
                table: "UserFeedback",
                column: "EventId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFeedback_EventId",
                table: "UserFeedback");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedback_EventId",
                table: "UserFeedback",
                column: "EventId");
        }
    }
}
