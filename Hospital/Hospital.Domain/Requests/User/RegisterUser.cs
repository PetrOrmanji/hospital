namespace Hospital.Domain.Requests.User;

public record RegisterUser(
    string Login,
    string Password,
    string FullName);