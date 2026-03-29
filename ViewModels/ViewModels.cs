using MetaSphere.Models;
using System.ComponentModel.DataAnnotations;

namespace MetaSphere.ViewModels;

public class LeadViewModel
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    public string? Phone { get; set; }

    [Required]
    public string Message { get; set; } = string.Empty;

    public int? ItemId { get; set; }
}

public class ExploreViewModel
{
    public IEnumerable<Item> Items { get; set; } = [];
    public string? Search { get; set; }
    public string? Tag { get; set; }
    public IEnumerable<string> Tags { get; set; } = [];
}

public class ItemDetailViewModel
{
    public Item Item { get; set; } = null!;
    public LeadViewModel Lead { get; set; } = new();
}

public class ItemFormViewModel
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
}
