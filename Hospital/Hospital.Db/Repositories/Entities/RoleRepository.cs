using EnsureThat;
using Hospital.Db.Repositories.Base;
using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Db.Repositories.Entities;

public class RoleRepository(HospitalContext hospitalContext) : BaseRepository(hospitalContext)
{
    public Task<Role?> GetAsync(Guid id)
    {
        return HospitalContext.Roles
            .FirstOrDefaultAsync(role => role.Id == id);
    }

    public Task<List<Role>> GetAllAsync()
    {
        return HospitalContext.Roles
            .ToListAsync();
    }

    public Task AddAsync(string name)
    {
        EnsureArg.IsNotEmptyOrWhiteSpace(name, nameof(name));

        HospitalContext.Roles.Add(new Role(name));
        return HospitalContext.SaveChangesAsync();
    }

    public Task RemoveAsync(Role role)
    {
        EnsureArg.IsNotNull(role, nameof(role));

        HospitalContext.Roles.Remove(role);
        return HospitalContext.SaveChangesAsync();
    }

    public Task UpdateAsync(
        Role role,
        string name)
    {
        EnsureArg.IsNotNull(role, nameof(role));

        role.Name = name;
        return HospitalContext.SaveChangesAsync();
    }
}