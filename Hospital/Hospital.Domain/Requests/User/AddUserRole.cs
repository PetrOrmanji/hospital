namespace Hospital.Domain.Requests.User;

public record AddUserRole(
    Guid UserId,
    Guid RoleId);