using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StationControl.Migrations
{
    /// <inheritdoc />
    public partial class new_mail_feature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MailId",
                table: "Deputies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    MailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeputyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.MailId);
                    table.ForeignKey(
                        name: "FK_Mails_Deputies_DeputyId",
                        column: x => x.DeputyId,
                        principalTable: "Deputies",
                        principalColumn: "DeputyId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mails_DeputyId",
                table: "Mails",
                column: "DeputyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropColumn(
                name: "MailId",
                table: "Deputies");
        }
    }
}
