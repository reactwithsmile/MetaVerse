using System.ComponentModel.DataAnnotations;

namespace MetaSphere.Models;

public class Item
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Tag { get; set; } = "Digital Asset";

    [MaxLength(50)]
    public string Rarity { get; set; } = "Common";

    public string ImageUrl { get; set; } = "/images/default-nft.png";

    public bool IsFeatured { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
