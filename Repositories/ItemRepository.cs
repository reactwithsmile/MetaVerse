using MetaSphere.Data;
using MetaSphere.Models;
using Microsoft.EntityFrameworkCore;

namespace MetaSphere.Repositories;

public class ItemRepository(AppDbContext db) : IItemRepository
{
    public async Task<IEnumerable<Item>> GetAllAsync(string? search = null, string? tag = null)
    {
        var query = db.Items.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(i => i.Title.Contains(search) || i.Description.Contains(search));
        if (!string.IsNullOrWhiteSpace(tag))
            query = query.Where(i => i.Tag == tag);
        return await query.OrderByDescending(i => i.CreatedAt).ToListAsync();
    }

    public async Task<IEnumerable<Item>> GetFeaturedAsync() =>
        await db.Items.Where(i => i.IsFeatured).Take(3).ToListAsync();

    public async Task<Item?> GetByIdAsync(int id) => await db.Items.FindAsync(id);

    public async Task AddAsync(Item item) { db.Items.Add(item); await db.SaveChangesAsync(); }

    public async Task UpdateAsync(Item item) { db.Items.Update(item); await db.SaveChangesAsync(); }

    public async Task DeleteAsync(int id)
    {
        var item = await db.Items.FindAsync(id);
        if (item != null) { db.Items.Remove(item); await db.SaveChangesAsync(); }
    }
}
