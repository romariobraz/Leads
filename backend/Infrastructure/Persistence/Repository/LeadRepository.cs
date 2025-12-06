using LeadQualifier.Application.Interfaces;
using LeadQualifier.Domain.Entities;
using System;
namespace LeadQualifier.Infrastructure.Persistence.Repository;


public class LeadRepository : ILeadRepository
{
    private readonly AppDbContext _db;


    public LeadRepository(AppDbContext db)
    {
        _db = db;
    }


    public async Task AddAsync(Lead lead)
    {
        await _db.Leads.AddAsync(lead);
    }


    public async Task DeleteAsync(Lead lead)
    {
        _db.Leads.Remove(lead);
        await Task.CompletedTask;
    }


    public async Task<IEnumerable<Lead>> GetAllAsync()
    {
        return await _db.Leads
        .AsNoTracking()
        .OrderByDescending(x => x.CreatedAt)
        .ToListAsync();
    }


    public async Task<Lead?> GetByIdAsync(Guid id)
    {
        return await _db.Leads.FindAsync(id);
    }


    public async Task UpdateAsync(Lead lead)
    {
        _db.Leads.Update(lead);
        await Task.CompletedTask;
    }


    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}