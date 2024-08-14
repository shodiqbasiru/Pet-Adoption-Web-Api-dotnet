using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdoption.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_account",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_name = table.Column<string>(type: "NVarchar(50)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_service",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    service_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_service", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_name = table.Column<string>(type: "Varchar(50)", nullable: true),
                    address = table.Column<string>(type: "Varchar(250)", nullable: true),
                    mobile_phone = table.Column<string>(type: "Varchar(14)", nullable: true),
                    email = table.Column<string>(type: "Varchar(50)", nullable: true),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_customer", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_customer_m_account_account_id",
                        column: x => x.account_id,
                        principalTable: "m_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_store",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rating = table.Column<long>(type: "bigint", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_store", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_store_m_account_account_id",
                        column: x => x.account_id,
                        principalTable: "m_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_purchase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trans_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trans_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    service_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_purchase", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_purchase_m_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "m_customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_purchase_m_service_service_id",
                        column: x => x.service_id,
                        principalTable: "m_service",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_name = table.Column<string>(type: "Varchar(100)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    stock = table.Column<long>(type: "bigint", nullable: false),
                    rating = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    store_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_product_m_category_category_id",
                        column: x => x.category_id,
                        principalTable: "m_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_product_m_store_store_id",
                        column: x => x.store_id,
                        principalTable: "m_store",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_review",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rating = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_review", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_review_m_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "m_customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_m_review_m_product_product_id",
                        column: x => x.product_id,
                        principalTable: "m_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "t_purchase_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    purchase_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pet_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    qty = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_purchase_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_purchase_detail_m_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "m_product",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_t_purchase_detail_t_purchase_purchase_id",
                        column: x => x.purchase_id,
                        principalTable: "t_purchase",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_customer_account_id",
                table: "m_customer",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_product_category_id",
                table: "m_product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_product_store_id",
                table: "m_product",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_review_customer_id",
                table: "m_review",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_review_product_id",
                table: "m_review",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_store_account_id",
                table: "m_store",
                column: "account_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_purchase_customer_id",
                table: "t_purchase",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_purchase_service_id",
                table: "t_purchase",
                column: "service_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_purchase_detail_ProductId",
                table: "t_purchase_detail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_t_purchase_detail_purchase_id",
                table: "t_purchase_detail",
                column: "purchase_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_review");

            migrationBuilder.DropTable(
                name: "t_purchase_detail");

            migrationBuilder.DropTable(
                name: "m_product");

            migrationBuilder.DropTable(
                name: "t_purchase");

            migrationBuilder.DropTable(
                name: "m_category");

            migrationBuilder.DropTable(
                name: "m_store");

            migrationBuilder.DropTable(
                name: "m_customer");

            migrationBuilder.DropTable(
                name: "m_service");

            migrationBuilder.DropTable(
                name: "m_account");
        }
    }
}
