using Microsoft.EntityFrameworkCore.Migrations;

namespace Devon4Net.WebAPI.Implementation.Migrations
{
    public partial class InsertUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "clientid", "role" },
                values: new object[,]
                {
                    { "OP1", "Owner" },
                    { "OP2", "Owner" },
                    { "OP3", "Owner" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "clientid",
                keyValue: "OP1");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "clientid",
                keyValue: "OP2");

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "clientid",
                keyValue: "OP3");
        }
    }
}
