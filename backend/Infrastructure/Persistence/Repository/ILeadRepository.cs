using LeadQualifier.Domain.Entities;


namespace LeadQualifier.Infrastructure.Persistence.Repository;


public interface ILeadRepositoryInfra
{
    Task<IEnumerable<Lead>> GetAllAsync();
    Task<Lead?> GetByIdAsync(Guid id);
    Task AddAsync(Lead lead);
    Task UpdateAsync(Lead lead);
    Task DeleteAsync(Lead lead);
    Task SaveChangesAsync();
}