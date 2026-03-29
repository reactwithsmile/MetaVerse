using System.ComponentModel.DataAnnotations;

namespace MetaSphere.Models;

public class Lead
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Phone, MaxLength(20)]
    public string? Phone { get; set; }

    [Required]
    public string Message { get; set; } = string.Empty;

    public int? ItemId { get; set; }
    public Item? Item { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
}
