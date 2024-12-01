namespace Hospital.Domain.Models;

public record PolyclinicModel(
    Guid Id,
    string Name,
    string Address,
    string ContactNumber,
    byte[]? Photo,
    Guid TownId,
    string TownName);