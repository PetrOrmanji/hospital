namespace Hospital.Domain.Models;

public record DoctorModel(
    Guid Id,
    string FullName,
    string ContactNumber,
    byte[] Photo,
    string ShortDescription,
    string FullDescription,
    List<PolyclinicModel> Polyclinics,
    List<SpecializationModel> Specializations);