using EnsureThat;
using Hospital.Db.Repositories.Base;
using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Db.Repositories.Entities;

public class UserRepository(HospitalContext hospitalContext) : BaseRepository(hospitalContext)
{
    public Task<User?> GetAsync(Guid id)
    {
        return HospitalContext.Users
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public Task<User?> GetAsync(string login)
    {
        return HospitalContext.Users
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Login == login);
    }

    public Task<List<User>> GetAllAsync()
    {
        return HospitalContext.Users
            .Include(user => user.Roles)
            .ToListAsync();
    }

    public Task AddAsync(
        string fullName,
        string login,
        string passwordHash)
    {
        EnsureArg.IsNotEmptyOrWhiteSpace(fullName, nameof(fullName));
        EnsureArg.IsNotEmptyOrWhiteSpace(login, nameof(login));
        EnsureArg.IsNotEmptyOrWhiteSpace(passwordHash, nameof(passwordHash));

        HospitalContext.Users.Add(
            new User(
                fullName,
                login,
                passwordHash));

        return HospitalContext.SaveChangesAsync();
    }

    public Task RemoveAsync(User user)
    {
        EnsureArg.IsNotNull(user, nameof(user));

        HospitalContext.Users.Remove(user);
        return HospitalContext.SaveChangesAsync();
    }

    public Task UpdateAsync(
        User user,
        string? fullName = default,
        string? newPasswordHash = default)
    {
        user.PasswordHash = newPasswordHash ?? user.PasswordHash;
        user.FullName = fullName ?? user.FullName;

        return HospitalContext.SaveChangesAsync();
    }

    public Task AddUserRole(
        User user,
        Role role)
    {
        EnsureArg.IsNotNull(user, nameof(user));
        EnsureArg.IsNotNull(role, nameof(role));

        var userRole = new UserRole
        {
            UserId = user.Id,
            RoleId = role.Id
        };

        HospitalContext.UserRoles.Add(userRole);
        return HospitalContext.SaveChangesAsync();
    }

    public Task RemoveUserRole(Guid id)
    {
        var userRole =
            HospitalContext.UserRoles.FirstOrDefault(role => role.Id == id);

        HospitalContext.UserRoles.Remove(userRole ?? throw new Exception("Указанной роли не существует у пользователя"));
        return HospitalContext.SaveChangesAsync();
    }
}