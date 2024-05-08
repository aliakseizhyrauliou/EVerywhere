using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EVerywhere.ChargerPoint.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chargers",
                columns: table => new
                {
                    charger_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    operator_id = table.Column<long>(type: "bigint", nullable: false),
                    aggregator_id = table.Column<long>(type: "bigint", nullable: false),
                    serial_number = table.Column<string>(type: "text", nullable: true),
                    web_id = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    operator_system_charger_id = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    is_connected = table.Column<bool>(type: "boolean", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: true),
                    lon = table.Column<double>(type: "double precision", nullable: true),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chargers", x => x.charger_id);
                });

            migrationBuilder.CreateTable(
                name: "specific_operator_charger_configs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    operator_id = table.Column<long>(type: "bigint", nullable: false),
                    charger_status_available_icon_url = table.Column<string>(type: "text", nullable: true),
                    charger_status_occupied_icon_url = table.Column<string>(type: "text", nullable: true),
                    charger_status_unavailable_icon_url = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specific_operator_charger_configs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "connectors",
                columns: table => new
                {
                    connector_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(type: "integer", nullable: false),
                    charger_id = table.Column<long>(type: "bigint", nullable: false),
                    power = table.Column<double>(type: "double precision", nullable: false),
                    serial_num = table.Column<string>(type: "text", nullable: true),
                    tariffed = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_connectors", x => x.connector_id);
                    table.ForeignKey(
                        name: "fk_connectors_chargers_charger_id",
                        column: x => x.charger_id,
                        principalTable: "chargers",
                        principalColumn: "charger_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_chargers_web_id",
                table: "chargers",
                column: "web_id");

            migrationBuilder.CreateIndex(
                name: "ix_connectors_charger_id",
                table: "connectors",
                column: "charger_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "connectors");

            migrationBuilder.DropTable(
                name: "specific_operator_charger_configs");

            migrationBuilder.DropTable(
                name: "chargers");
        }
    }
}
