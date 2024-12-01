namespace Hospital.Domain.Requests.Doctors;

public record AddDoctorSpecialization(
    Guid DoctorId,
    Guid SpecializationId,
    double Experience);