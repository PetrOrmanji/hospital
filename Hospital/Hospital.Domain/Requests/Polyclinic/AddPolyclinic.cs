namespace Hospital.Domain.Requests.Polyclinic;

public record AddPolyclinic(
    string Name,
    Guid TownId,
    string Address,
    string ContactNumber,
    byte[]? Photo);