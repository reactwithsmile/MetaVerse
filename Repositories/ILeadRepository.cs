using MetaSphere.Models;

namespace MetaSphere.Repositories;

public interface ILeadRepository
{
    Task<IEnumerable<Lead>> GetAllAsync();
    Task AddAsync(Lead lead);
    Task DeleteAsync(int id);
}
