namespace Hospital.Domain.Entities;

public class DoctorSpecialization : EntityBase
{
    public Guid DoctorId { get; set; }
    public Guid SpecializationId { get; set; }
    public double Experience { get; set; }
}