namespace Hospital.Domain.Requests.User;

public record LoginUser(
    string Login, 
    string Password);