using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EVerywhere.Balance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "paid_resource_type_configurations",
                columns: table => new
                {
                    paid_resource_type_configuration_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_paid_resource_type_configurations", x => x.paid_resource_type_configuration_id);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    payment_method_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    first1 = table.Column<string>(type: "text", nullable: true),
                    last4 = table.Column<string>(type: "text", nullable: true),
                    card_number_data = table.Column<string>(type: "text", nullable: false),
                    card_type = table.Column<int>(type: "integer", nullable: false),
                    expiry_year = table.Column<int>(type: "integer", nullable: false),
                    expiry_month = table.Column<int>(type: "integer", nullable: false),
                    is_selected = table.Column<bool>(type: "boolean", nullable: false),
                    payment_system_stamp = table.Column<string>(type: "text", nullable: true),
                    payment_system_token = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_methods", x => x.payment_method_id);
                });

            migrationBuilder.CreateTable(
                name: "payment_system_configurations",
                columns: table => new
                {
                    payment_system_configuration_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    payment_system_name = table.Column<string>(type: "text", nullable: false),
                    data = table.Column<string>(type: "jsonb", nullable: false),
                    is_current_schema = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_system_configurations", x => x.payment_system_configuration_id);
                });

            migrationBuilder.CreateTable(
                name: "holds",
                columns: table => new
                {
                    hold_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    paid_resource_type_id = table.Column<long>(type: "bigint", nullable: true),
                    paid_resource_id = table.Column<string>(type: "text", nullable: false),
                    payment_system_transaction_id = table.Column<string>(type: "text", nullable: false),
                    operator_id = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    is_captured = table.Column<bool>(type: "boolean", nullable: false),
                    is_voided = table.Column<bool>(type: "boolean", nullable: false),
                    payment_method_id = table.Column<long>(type: "bigint", nullable: true),
                    additional_data = table.Column<string>(type: "jsonb", nullable: true),
                    payment_system_configuration_id = table.Column<long>(type: "bigint", nullable: false),
                    receipt_url = table.Column<string>(type: "text", nullable: false),
                    hold_capture_created_payment_id = table.Column<long>(type: "bigint", nullable: true),
                    payment_system_widget_generation_id = table.Column<long>(type: "bigint", nullable: true),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_holds", x => x.hold_id);
                    table.ForeignKey(
                        name: "fk_holds_paid_resource_type_paid_resource_type_id",
                        column: x => x.paid_resource_type_id,
                        principalTable: "paid_resource_type_configurations",
                        principalColumn: "paid_resource_type_configuration_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_holds_payment_methods_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "payment_method_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_holds_payment_system_configurations_payment_system_configur",
                        column: x => x.payment_system_configuration_id,
                        principalTable: "payment_system_configurations",
                        principalColumn: "payment_system_configuration_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    payment_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    paid_resource_id = table.Column<string>(type: "text", nullable: false),
                    payment_system_transaction_id = table.Column<string>(type: "text", nullable: false),
                    additional_data = table.Column<string>(type: "jsonb", nullable: true),
                    operator_id = table.Column<string>(type: "text", nullable: false),
                    is_success = table.Column<bool>(type: "boolean", nullable: false),
                    is_bonus = table.Column<bool>(type: "boolean", nullable: false),
                    payment_method_id = table.Column<long>(type: "bigint", nullable: true),
                    paid_resource_type_id = table.Column<long>(type: "bigint", nullable: true),
                    payment_system_configuration_id = table.Column<long>(type: "bigint", nullable: false),
                    receipt_url = table.Column<string>(type: "text", nullable: true),
                    captured_hold_id = table.Column<long>(type: "bigint", nullable: true),
                    payment_system_widget_id = table.Column<long>(type: "bigint", nullable: true),
                    capture_debtor_id = table.Column<long>(type: "bigint", nullable: true),
                    is_refund = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.payment_id);
                    table.ForeignKey(
                        name: "fk_payments_holds_captured_hold_id",
                        column: x => x.captured_hold_id,
                        principalTable: "holds",
                        principalColumn: "hold_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payments_paid_resource_type_paid_resource_type_id",
                        column: x => x.paid_resource_type_id,
                        principalTable: "paid_resource_type_configurations",
                        principalColumn: "paid_resource_type_configuration_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payments_payment_methods_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "payment_method_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payments_payment_system_configurations_payment_system_confi",
                        column: x => x.payment_system_configuration_id,
                        principalTable: "payment_system_configurations",
                        principalColumn: "payment_system_configuration_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "debtors",
                columns: table => new
                {
                    debtor_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    payment_method_id = table.Column<long>(type: "bigint", nullable: false),
                    paid_resource_type_id = table.Column<long>(type: "bigint", nullable: true),
                    payment_system_configuration_id = table.Column<long>(type: "bigint", nullable: false),
                    operator_id = table.Column<string>(type: "text", nullable: false),
                    paid_resource_id = table.Column<string>(type: "text", nullable: true),
                    new_payment_id = table.Column<long>(type: "bigint", nullable: true),
                    additional_data = table.Column<string>(type: "jsonb", nullable: true),
                    capture_attempt_count = table.Column<int>(type: "integer", nullable: false),
                    last_capture_attempt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    debtor_capture_last_error_message = table.Column<string>(type: "text", nullable: true),
                    is_captured = table.Column<bool>(type: "boolean", nullable: false),
                    need_to_capture = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_debtors", x => x.debtor_id);
                    table.ForeignKey(
                        name: "fk_debtors_paid_resource_type_paid_resource_type_id",
                        column: x => x.paid_resource_type_id,
                        principalTable: "paid_resource_type_configurations",
                        principalColumn: "paid_resource_type_configuration_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_debtors_payment_methods_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "payment_method_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_debtors_payment_system_configurations_payment_system_config",
                        column: x => x.payment_system_configuration_id,
                        principalTable: "payment_system_configurations",
                        principalColumn: "payment_system_configuration_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_debtors_payments_new_payment_id",
                        column: x => x.new_payment_id,
                        principalTable: "payments",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment_system_widget_generations",
                columns: table => new
                {
                    payment_system_widget_generation_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    widget_reason = table.Column<int>(type: "integer", nullable: false),
                    paid_resource_id = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    is_success = table.Column<bool>(type: "boolean", nullable: false),
                    got_response_from_payment_system = table.Column<bool>(type: "boolean", nullable: false),
                    is_disabled = table.Column<bool>(type: "boolean", nullable: false),
                    url = table.Column<string>(type: "text", nullable: true),
                    token = table.Column<string>(type: "text", nullable: true),
                    payment_system_configuration_id = table.Column<long>(type: "bigint", nullable: false),
                    paid_resource_type_id = table.Column<long>(type: "bigint", nullable: false),
                    hold_id = table.Column<long>(type: "bigint", nullable: true),
                    payment_id = table.Column<long>(type: "bigint", nullable: true),
                    operator_id = table.Column<string>(type: "text", nullable: false),
                    additional_data = table.Column<string>(type: "jsonb", nullable: true),
                    payment_system_message = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_system_widget_generations", x => x.payment_system_widget_generation_id);
                    table.ForeignKey(
                        name: "fk_payment_system_widget_generations_holds_hold_id",
                        column: x => x.hold_id,
                        principalTable: "holds",
                        principalColumn: "hold_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_payment_system_widget_generations_paid_resource_type_config",
                        column: x => x.paid_resource_type_id,
                        principalTable: "paid_resource_type_configurations",
                        principalColumn: "paid_resource_type_configuration_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payment_system_widget_generations_payment_system_configurat",
                        column: x => x.payment_system_configuration_id,
                        principalTable: "payment_system_configurations",
                        principalColumn: "payment_system_configuration_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payment_system_widget_generations_payments_payment_id",
                        column: x => x.payment_id,
                        principalTable: "payments",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "receipts",
                columns: table => new
                {
                    receipt_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    payment_system_transaction_id = table.Column<string>(type: "text", nullable: true),
                    paid_resource_id = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    payment_system_configuration_id = table.Column<long>(type: "bigint", nullable: true),
                    payment_method_id = table.Column<long>(type: "bigint", nullable: true),
                    payment_id = table.Column<long>(type: "bigint", nullable: true),
                    hold_id = table.Column<long>(type: "bigint", nullable: true),
                    paid_resource_type_id = table.Column<long>(type: "bigint", nullable: true),
                    is_receipt_for_hold = table.Column<bool>(type: "boolean", nullable: false),
                    is_receipt_for_payment = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_receipts", x => x.receipt_id);
                    table.ForeignKey(
                        name: "fk_receipts_holds_hold_id",
                        column: x => x.hold_id,
                        principalTable: "holds",
                        principalColumn: "hold_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_receipts_paid_resource_type_paid_resource_type_id",
                        column: x => x.paid_resource_type_id,
                        principalTable: "paid_resource_type_configurations",
                        principalColumn: "paid_resource_type_configuration_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_receipts_payment_methods_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "payment_method_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_receipts_payment_system_configurations_payment_system_confi",
                        column: x => x.payment_system_configuration_id,
                        principalTable: "payment_system_configurations",
                        principalColumn: "payment_system_configuration_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_receipts_payments_payment_id",
                        column: x => x.payment_id,
                        principalTable: "payments",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_debtors_new_payment_id",
                table: "debtors",
                column: "new_payment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_debtors_paid_resource_type_id",
                table: "debtors",
                column: "paid_resource_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_debtors_payment_method_id",
                table: "debtors",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_debtors_payment_system_configuration_id",
                table: "debtors",
                column: "payment_system_configuration_id");

            migrationBuilder.CreateIndex(
                name: "ix_holds_paid_resource_type_id",
                table: "holds",
                column: "paid_resource_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_holds_payment_method_id",
                table: "holds",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_holds_payment_system_configuration_id",
                table: "holds",
                column: "payment_system_configuration_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_system_widget_generations_hold_id",
                table: "payment_system_widget_generations",
                column: "hold_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_payment_system_widget_generations_paid_resource_type_id",
                table: "payment_system_widget_generations",
                column: "paid_resource_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_system_widget_generations_payment_id",
                table: "payment_system_widget_generations",
                column: "payment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_payment_system_widget_generations_payment_system_configurat",
                table: "payment_system_widget_generations",
                column: "payment_system_configuration_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_captured_hold_id",
                table: "payments",
                column: "captured_hold_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_payments_paid_resource_type_id",
                table: "payments",
                column: "paid_resource_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_payment_method_id",
                table: "payments",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_payments_payment_system_configuration_id",
                table: "payments",
                column: "payment_system_configuration_id");

            migrationBuilder.CreateIndex(
                name: "ix_receipts_hold_id",
                table: "receipts",
                column: "hold_id");

            migrationBuilder.CreateIndex(
                name: "ix_receipts_paid_resource_type_id",
                table: "receipts",
                column: "paid_resource_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_receipts_payment_id",
                table: "receipts",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "ix_receipts_payment_method_id",
                table: "receipts",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "ix_receipts_payment_system_configuration_id",
                table: "receipts",
                column: "payment_system_configuration_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "debtors");

            migrationBuilder.DropTable(
                name: "payment_system_widget_generations");

            migrationBuilder.DropTable(
                name: "receipts");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "holds");

            migrationBuilder.DropTable(
                name: "paid_resource_type_configurations");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "payment_system_configurations");
        }
    }
}
