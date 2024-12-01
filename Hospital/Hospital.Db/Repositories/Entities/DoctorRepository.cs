using EnsureThat;
using Hospital.Db.Repositories.Base;
using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Db.Repositories.Entities;

public class DoctorRepository(HospitalContext hospitalContext) : BaseRepository(hospitalContext)
{
    public Task<Doctor?> GetAsync(Guid id)
    {
        return HospitalContext.Doctors
            .Include(doctor => doctor.Polyclinics.Where(polyclinic => polyclinic.DoctorId == id))
            .Include(doctor => doctor.Specializations.Where(specialization => specialization.DoctorId == id))
            .FirstOrDefaultAsync(doctor => doctor.Id == id);
    }

    public Task<List<Doctor>> GetAllAsync()
    {
        return HospitalContext.Doctors
            .Include(doctor => doctor.Polyclinics)
            .Include(doctor => doctor.Specializations)
            .ToListAsync();
    }

    public Task AddAsync(
        string fullName,
        string contactNumber,
        byte[]? photo,
        string shortDescription,
        string fullDescription)
    {
        var doctor = new Doctor(
            fullName, 
            contactNumber,
            shortDescription,
            fullDescription, 
            photo);

        HospitalContext.Doctors.Add(doctor);
        return HospitalContext.SaveChangesAsync();
    }

    public Task RemoveAsync(Doctor doctor)
    {
        EnsureArg.IsNotNull(doctor, nameof(doctor));

        HospitalContext.Doctors.Remove(doctor);
        return HospitalContext.SaveChangesAsync();
    }

    public Task UpdateAsync(
        Doctor doctor,
        string? fullName = default,
        string? contactNumber = default,
        byte[]? photo = default,
        string? shortDescription = default,
        string? fullDescription = default)
    {
        EnsureArg.IsNotNull(doctor, nameof(doctor));

        doctor.FullName = fullName ?? doctor.FullName;
        doctor.ContactNumber = contactNumber ?? doctor.ContactNumber;
        doctor.Photo = photo ?? doctor.Photo;
        doctor.ShortDescription = shortDescription ?? doctor.ShortDescription;
        doctor.FullDescription = fullDescription ?? doctor.FullDescription;

        return HospitalContext.SaveChangesAsync();
    }

    public Task AddDoctorSpecialization(
        Doctor doctor,
        Specialization specialization,
        double experience)
    {
        EnsureArg.IsNotNull(doctor, nameof(doctor));
        EnsureArg.IsNotNull(specialization, nameof(specialization));

        var doctorSpecialization = new DoctorSpecialization
        {
            DoctorId = doctor.Id,
            SpecializationId = specialization.Id,
            Experience = experience
        };

        HospitalContext.DoctorsSpecializations.Add(doctorSpecialization);
        return HospitalContext.SaveChangesAsync();
    }

    public Task RemoveDoctorSpecialization(Guid id)
    {
        var doctorSpecialization =
            HospitalContext.DoctorsSpecializations.FirstOrDefault(specialization => specialization.Id == id);

        HospitalContext.DoctorsSpecializations.Remove(doctorSpecialization ?? throw new Exception("Указанной специальности не существует у доктора"));
        return HospitalContext.SaveChangesAsync();
    }

    public Task AddDoctorPolyclinic(
        Doctor doctor,
        Polyclinic polyclinic,
        double cost)
    {
        EnsureArg.IsNotNull(doctor, nameof(doctor));
        EnsureArg.IsNotNull(polyclinic, nameof(polyclinic));

        var doctorPolyclinic = new PolyclinicDoctor
        {
            DoctorId = doctor.Id,
            PolyclinicId = polyclinic.Id,
            Cost = cost
        };

        HospitalContext.PolyclinicsDoctors.Add(doctorPolyclinic);
        return HospitalContext.SaveChangesAsync();
    }

    public Task RemoveDoctorPolyclinic(Guid id)
    {
        var doctorPolyclinic =
            HospitalContext.PolyclinicsDoctors.FirstOrDefault(polyclinic => polyclinic.Id == id);

        HospitalContext.PolyclinicsDoctors.Remove(doctorPolyclinic ?? throw new Exception("Указанной поликлиники не существует у доктора"));
        return HospitalContext.SaveChangesAsync();
    }
}