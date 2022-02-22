using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Devon4Net.WebAPI.Implementation.Migrations
{
    public partial class initialBBDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    clientid = table.Column<string>(type: "character varying", nullable: false),
                    role = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.clientid);
                });

            migrationBuilder.CreateTable(
                name: "visitor",
                columns: table => new
                {
                    uid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("visitor_pkey", x => x.uid);
                });

            migrationBuilder.CreateTable(
                name: "queue",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    logo = table.Column<string>(type: "character varying", nullable: true),
                    accesslink = table.Column<string>(type: "character varying", nullable: true),
                    minattentiontime = table.Column<int>(nullable: true),
                    opentime = table.Column<string>(type: "character varying", nullable: true),
                    closetime = table.Column<string>(type: "character varying", nullable: true),
                    started = table.Column<bool>(nullable: true),
                    closed = table.Column<bool>(nullable: true),
                    user_clientid = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_queue", x => x.id);
                    table.ForeignKey(
                        name: "fk_user",
                        column: x => x.user_clientid,
                        principalTable: "user",
                        principalColumn: "clientid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "access_code",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying", nullable: true),
                    createdtime = table.Column<TimeSpan>(type: "time without time zone", nullable: true),
                    endtime = table.Column<TimeSpan>(type: "time without time zone", nullable: true),
                    status = table.Column<string>(maxLength: 50, nullable: false),
                    visitor_uid = table.Column<Guid>(nullable: false),
                    queue_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_code", x => x.id);
                    table.ForeignKey(
                        name: "access_code_queue_id_fkey",
                        column: x => x.queue_id,
                        principalTable: "queue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "access_code_visitor_uid_fk",
                        column: x => x.visitor_uid,
                        principalTable: "visitor",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "clientid", "role" },
                values: new object[,]
                {
                    { "OP1", "Owner" },
                    { "OP2", "Owner" },
                    { "OP3", "Owner" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_access_code_queue_id",
                table: "access_code",
                column: "queue_id");

            migrationBuilder.CreateIndex(
                name: "IX_access_code_visitor_uid",
                table: "access_code",
                column: "visitor_uid");

            migrationBuilder.CreateIndex(
                name: "IX_queue_user_clientid",
                table: "queue",
                column: "user_clientid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_code");

            migrationBuilder.DropTable(
                name: "queue");

            migrationBuilder.DropTable(
                name: "visitor");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
