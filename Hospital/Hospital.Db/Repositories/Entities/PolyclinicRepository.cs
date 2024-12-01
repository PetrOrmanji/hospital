using EnsureThat;
using Hospital.Db.Repositories.Base;
using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Db.Repositories.Entities;

public class PolyclinicRepository(HospitalContext hospitalContext) : BaseRepository(hospitalContext)
{
    public Task<Polyclinic?> GetAsync(Guid id)
    {
        return HospitalContext.Polyclinics
            .Include(polyclinic => polyclinic.Town)
            .FirstOrDefaultAsync(polyclinic => polyclinic.Id == id);
    }

    public Task<List<Polyclinic>> GetAllAsync()
    {
        return HospitalContext.Polyclinics
            .Include(polyclinic => polyclinic.Town)
            .OrderBy(polyclinic => polyclinic.Name)
            .ToListAsync();
    }

    public async Task AddAsync(Polyclinic polyclinic)
    {
        HospitalContext.Polyclinics.Add(polyclinic);
        await HospitalContext.SaveChangesAsync();
    }

    public Task UpdateAsync(
        Polyclinic polyclinic,
        string? name = default,
        Guid? townId = default,
        string? address = default,
        string? contactNumber = default,
        byte[]? photo = default)
    {
        EnsureArg.IsNotNull(polyclinic, nameof(polyclinic));

        polyclinic.Name = name ?? polyclinic.Name;
        polyclinic.TownId = townId ?? polyclinic.TownId;
        polyclinic.Address = address ?? polyclinic.Address;
        polyclinic.ContactNumber = contactNumber ?? polyclinic.ContactNumber;
        polyclinic.Photo = photo ?? polyclinic.Photo;

        return HospitalContext.SaveChangesAsync();
    }

    public Task RemoveAsync(Polyclinic polyclinic)
    {
        EnsureArg.IsNotNull(polyclinic, nameof(polyclinic));

        HospitalContext.Polyclinics.Remove(polyclinic);
        return HospitalContext.SaveChangesAsync();
    }
}