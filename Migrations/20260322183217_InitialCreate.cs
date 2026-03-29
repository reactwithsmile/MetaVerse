using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MetaSphere.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Rarity = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    IsFeatured = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leads_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "IsFeatured", "Rarity", "Tag", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 22, 18, 32, 16, 28, DateTimeKind.Utc).AddTicks(6285), "A rare digital artifact from the genesis collection. Glowing neon patterns embedded in a quantum matrix.", "/images/nft1.png", true, "Legendary", "Digital Asset", "Neon Genesis #001" },
                    { 2, new DateTime(2026, 3, 22, 18, 32, 16, 28, DateTimeKind.Utc).AddTicks(6297), "Prime virtual real estate in the MetaSphere core district. Unlimited build potential.", "/images/nft2.png", true, "Rare", "Virtual Land", "Virtual Land Alpha" },
                    { 3, new DateTime(2026, 3, 22, 18, 32, 16, 28, DateTimeKind.Utc).AddTicks(6300), "Next-gen avatar with full customization. Unlock exclusive metaverse zones.", "/images/nft3.png", true, "Epic", "Avatar", "Cyber Avatar X" },
                    { 4, new DateTime(2026, 3, 22, 18, 32, 16, 28, DateTimeKind.Utc).AddTicks(6303), "Teleportation node connecting multiple metaverse realms. Limited edition.", "/images/nft4.png", false, "Limited", "Utility", "Quantum Portal" },
                    { 5, new DateTime(2026, 3, 22, 18, 32, 16, 28, DateTimeKind.Utc).AddTicks(6306), "Wearable dark matter armor with stealth capabilities and energy shields.", "/images/nft5.png", false, "Rare", "Wearable", "Dark Matter Suit" },
                    { 6, new DateTime(2026, 3, 22, 18, 32, 16, 28, DateTimeKind.Utc).AddTicks(6309), "Create and broadcast holographic content across the MetaSphere network.", "/images/nft6.png", false, "Common", "Digital Asset", "Hologram Studio" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leads_ItemId",
                table: "Leads",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
