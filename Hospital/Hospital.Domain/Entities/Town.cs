using EnsureThat;

namespace Hospital.Domain.Entities;

public class Town : EntityBase
{
    public string Name { get; set; }

    public ICollection<Polyclinic> Polyclinics = new HashSet<Polyclinic>();

    public Town(string name)
    {
        EnsureArg.IsNotEmptyOrWhiteSpace(name, nameof(name));

        Name = name;
    }
}