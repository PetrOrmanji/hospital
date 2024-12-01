namespace Hospital.Domain.Requests.Town;

public record UpdateTown(
    Guid Id,
    string Name);