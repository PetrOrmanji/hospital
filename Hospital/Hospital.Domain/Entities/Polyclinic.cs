using EnsureThat;

namespace Hospital.Domain.Entities;

public class Polyclinic : EntityBase
{
    public string Name { get; set; }
    public Guid TownId { get; set; } 
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    public byte[]? Photo { get; set; }

    public Town? Town { get; set; }
    public ICollection<PolyclinicDoctor> Doctors { get; } = new HashSet<PolyclinicDoctor>();

    public Polyclinic(
        string name, 
        Guid townId,
        string address,
        string contactNumber,
        byte[]? photo)
    {
        EnsureArg.IsNotEmptyOrWhiteSpace(name, nameof(name));
        EnsureArg.IsNotEmptyOrWhiteSpace(address, nameof(address));
        EnsureArg.IsNotEmptyOrWhiteSpace(contactNumber, nameof(contactNumber));

        Name = name;
        TownId = townId;
        Address = address;
        ContactNumber = contactNumber;
        Photo = photo;
    }
}