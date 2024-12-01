using EnsureThat;

namespace Hospital.Domain.Entities;

public class Role : EntityBase
{
    public string Name { get; set; }

    public ICollection<UserRole> Users { get; } = new HashSet<UserRole>();

    public Role(string name)
    {
        EnsureArg.IsNotEmptyOrWhiteSpace(name, nameof(name));

        Name = name;
    }
}