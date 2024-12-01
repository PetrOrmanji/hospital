using EnsureThat;

namespace Hospital.Domain.Entities;

public class User : EntityBase
{
    public string FullName { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<UserRole> Roles { get; } = new HashSet<UserRole>();

    public User(
        string fullName,
        string login,
        string passwordHash)
    {
        EnsureArg.IsNotNullOrWhiteSpace(fullName, nameof(fullName));
        EnsureArg.IsNotNullOrWhiteSpace(login, nameof(login));
        EnsureArg.IsNotNullOrWhiteSpace(passwordHash, nameof(passwordHash));

        FullName = fullName;
        Login = login;
        PasswordHash = passwordHash;
    }
}