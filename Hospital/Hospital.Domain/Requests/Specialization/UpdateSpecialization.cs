namespace Hospital.Domain.Requests.Specialization;

public record UpdateSpecialization(
    Guid Id,
    string Name);