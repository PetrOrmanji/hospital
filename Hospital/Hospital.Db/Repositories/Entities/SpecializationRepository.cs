using EnsureThat;
using Hospital.Db.Repositories.Base;
using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Db.Repositories.Entities;

public class SpecializationRepository(HospitalContext hospitalContext) : BaseRepository(hospitalContext)
{
    public Task<Specialization?> GetAsync(Guid id)
    {
        return HospitalContext.Specializations
            .FirstOrDefaultAsync(specialization => specialization.Id == id);
    }

    public Task<List<Specialization>> GetAllAsync()
    {
        return HospitalContext.Specializations.ToListAsync();
    }

    public Task AddAsync(string name)
    {
        EnsureArg.IsNotEmptyOrWhiteSpace(name, nameof(name));

        HospitalContext.Specializations.Add(new Specialization(name));
        return HospitalContext.SaveChangesAsync();
    }

    public Task RemoveAsync(Specialization specialization)
    {
        EnsureArg.IsNotNull(specialization, nameof(specialization));

        HospitalContext.Specializations.Remove(specialization);
        return HospitalContext.SaveChangesAsync();
    }

    public Task UpdateAsync(
        Specialization specialization,
        string name)
    {
        EnsureArg.IsNotNull(specialization, nameof(specialization));

        specialization.Name = name;
        return HospitalContext.SaveChangesAsync();
    }
}