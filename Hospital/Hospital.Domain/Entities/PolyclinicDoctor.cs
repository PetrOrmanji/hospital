namespace Hospital.Domain.Entities;

public class PolyclinicDoctor : EntityBase
{
    public Guid PolyclinicId { get; set; }
    public Guid DoctorId { get; set; }
    public double Cost { get; set; }
}