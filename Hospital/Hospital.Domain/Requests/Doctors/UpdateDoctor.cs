namespace Hospital.Domain.Requests.Doctors;

public record UpdateDoctor(
    Guid Id,
    string FullName,
    string ContactNumber,
    byte[] Photo,
    string ShortDescription,
    string FullDescription);