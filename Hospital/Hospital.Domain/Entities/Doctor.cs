using EnsureThat;

namespace Hospital.Domain.Entities;

public class Doctor : EntityBase
{
    public string FullName { get; set; }
    public string ContactNumber { get; set; }
    public byte[]? Photo { get; set; }
    public string ShortDescription { get; set; }
    public string FullDescription { get; set; }

    public ICollection<PolyclinicDoctor> Polyclinics { get; } = new HashSet<PolyclinicDoctor>();
    public ICollection<DoctorSpecialization> Specializations { get; } = new HashSet<DoctorSpecialization>();

    public Doctor(
        string fullName,
        string contactNumber,
        string shortDescription,
        string fullDescription,
        byte[]? photo)
    {
        EnsureArg.IsNotEmptyOrWhiteSpace(fullName, nameof(fullName));
        EnsureArg.IsNotEmptyOrWhiteSpace(contactNumber, nameof(contactNumber));
        EnsureArg.IsNotEmptyOrWhiteSpace(shortDescription, nameof(shortDescription));
        EnsureArg.IsNotEmptyOrWhiteSpace(fullDescription, nameof(fullDescription));

        FullName = fullName;
        ContactNumber = contactNumber;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        Photo = photo;
    }
}