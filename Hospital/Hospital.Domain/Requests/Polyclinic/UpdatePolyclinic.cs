namespace Hospital.Domain.Requests.Polyclinic;

public record UpdatePolyclinic(
    Guid Id,
    string? Name,
    Guid? TownId,
    string? Address,
    string? ContactNumber,
    byte[]? Photo);