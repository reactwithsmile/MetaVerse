using MetaSphere.Models;

namespace MetaSphere.Repositories;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllAsync(string? search = null, string? tag = null);
    Task<IEnumerable<Item>> GetFeaturedAsync();
    Task<Item?> GetByIdAsync(int id);
    Task AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(int id);
}
