using EnsureThat;

namespace Hospital.Domain.Entities;

public class Specialization : EntityBase
{
    public string Name { get; set; }

    public ICollection<DoctorSpecialization> Doctors { get; } = new HashSet<DoctorSpecialization>();

    public Specialization(string name)
    {
        EnsureArg.IsNotEmptyOrWhiteSpace(name, nameof(name));

        Name = name;
    }
}