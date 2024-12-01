namespace Hospital.Domain.Requests.Doctors;

public record AddDoctor(
    string FullName,
    string ContactNumber,
    byte[] Photo,
    string ShortDescription,
    string FullDescription);