using MetaSphere.Models;
using Microsoft.EntityFrameworkCore;

namespace MetaSphere.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Lead> Leads => Set<Lead>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>().HasData(
            new Item { Id = 1, Title = "Neon Genesis #001", Description = "A rare digital artifact from the genesis collection. Glowing neon patterns embedded in a quantum matrix.", Tag = "Digital Asset", Rarity = "Legendary", ImageUrl = "/images/nft1.png", IsFeatured = true },
            new Item { Id = 2, Title = "Virtual Land Alpha", Description = "Prime virtual real estate in the MetaSphere core district. Unlimited build potential.", Tag = "Virtual Land", Rarity = "Rare", ImageUrl = "/images/nft2.png", IsFeatured = true },
            new Item { Id = 3, Title = "Cyber Avatar X", Description = "Next-gen avatar with full customization. Unlock exclusive metaverse zones.", Tag = "Avatar", Rarity = "Epic", ImageUrl = "/images/nft3.png", IsFeatured = true },
            new Item { Id = 4, Title = "Quantum Portal", Description = "Teleportation node connecting multiple metaverse realms. Limited edition.", Tag = "Utility", Rarity = "Limited", ImageUrl = "/images/nft4.png", IsFeatured = false },
            new Item { Id = 5, Title = "Dark Matter Suit", Description = "Wearable dark matter armor with stealth capabilities and energy shields.", Tag = "Wearable", Rarity = "Rare", ImageUrl = "/images/nft5.png", IsFeatured = false },
            new Item { Id = 6, Title = "Hologram Studio", Description = "Create and broadcast holographic content across the MetaSphere network.", Tag = "Digital Asset", Rarity = "Common", ImageUrl = "/images/nft6.png", IsFeatured = false }
        );
    }
}
