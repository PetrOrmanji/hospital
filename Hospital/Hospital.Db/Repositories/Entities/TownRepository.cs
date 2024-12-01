using EnsureThat;
using Hospital.Db.Repositories.Base;
using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Db.Repositories.Entities;

public class TownRepository(HospitalContext hospitalContext) : BaseRepository(hospitalContext)
{
    public Task<Town?> GetAsync(Guid id)
    {
        return HospitalContext.Towns
            .Include(town => town.Polyclinics)
            .FirstOrDefaultAsync(town => town.Id == id);
    }

    public Task<List<Town>> GetAllAsync()
    {
        return HospitalContext.Towns
            .Include(town => town.Polyclinics)
            .OrderBy(town => town.Name)
            .ToListAsync();
    }

    public Task AddAsync(string name)
    {
        EnsureArg.IsNotEmptyOrWhiteSpace(name, nameof(name));

        HospitalContext.Towns.Add(new Town(name));
        return HospitalContext.SaveChangesAsync();
    }

    public Task UpdateAsync(
        Town town,
        string name)
    {
        EnsureArg.IsNotNull(town, nameof(town));
        EnsureArg.IsNotNullOrWhiteSpace(name, nameof(name));

        town.Name = name;

        return HospitalContext.SaveChangesAsync();
    }

    public Task RemoveAsync(Town town)
    {
        EnsureArg.IsNotNull(town, nameof(town));

        HospitalContext.Towns.Remove(town);
        return HospitalContext.SaveChangesAsync();
    }
}