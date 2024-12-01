namespace Hospital.Domain.Requests.Doctors;

public record AddDoctorPolyclinic(
    Guid DoctorId,
    Guid PolyclinicId,
    double Cost);