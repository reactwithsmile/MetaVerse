using MetaSphere.Data;
using MetaSphere.Models;
using Microsoft.EntityFrameworkCore;

namespace MetaSphere.Repositories;

public class LeadRepository(AppDbContext db) : ILeadRepository
{
    public async Task<IEnumerable<Lead>> GetAllAsync() =>
        await db.Leads.Include(l => l.Item).OrderByDescending(l => l.SubmittedAt).ToListAsync();

    public async Task AddAsync(Lead lead) { db.Leads.Add(lead); await db.SaveChangesAsync(); }

    public async Task DeleteAsync(int id)
    {
        var lead = await db.Leads.FindAsync(id);
        if (lead != null) { db.Leads.Remove(lead); await db.SaveChangesAsync(); }
    }
}
