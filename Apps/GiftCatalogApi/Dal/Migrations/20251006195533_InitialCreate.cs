using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DenominationDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DenominationDal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Slug = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorDal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftBatchDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DenominationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftBatchDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftBatchDal_DenominationDal_DenominationId",
                        column: x => x.DenominationId,
                        principalTable: "DenominationDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GiftBatchDal_VendorDal_VendorId",
                        column: x => x.VendorId,
                        principalTable: "VendorDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GiftCardDal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CodeHash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    MaskedCode = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActivatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BatchId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCardDal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiftCardDal_GiftBatchDal_BatchId",
                        column: x => x.BatchId,
                        principalTable: "GiftBatchDal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DenominationDal_Amount_Currency",
                table: "DenominationDal",
                columns: new[] { "Amount", "Currency" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiftBatchDal_DenominationId",
                table: "GiftBatchDal",
                column: "DenominationId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftBatchDal_VendorId",
                table: "GiftBatchDal",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCardDal_BatchId",
                table: "GiftCardDal",
                column: "BatchId");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCardDal_CodeHash",
                table: "GiftCardDal",
                column: "CodeHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VendorDal_Slug",
                table: "VendorDal",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiftCardDal");

            migrationBuilder.DropTable(
                name: "GiftBatchDal");

            migrationBuilder.DropTable(
                name: "DenominationDal");

            migrationBuilder.DropTable(
                name: "VendorDal");
        }
    }
}
