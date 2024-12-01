using Hospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Db;

public class HospitalContext(DbContextOptions<HospitalContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Town> Towns => Set<Town>();
    public DbSet<Polyclinic> Polyclinics => Set<Polyclinic>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<DoctorSpecialization> DoctorsSpecializations => Set<DoctorSpecialization>();
    public DbSet<PolyclinicDoctor> PolyclinicsDoctors => Set<PolyclinicDoctor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //User
        modelBuilder
            .Entity<User>()
            .HasKey(user => user.Id);

        modelBuilder
            .Entity<User>()
            .HasIndex(user => user.Login)
            .IsUnique();

        modelBuilder
            .Entity<User>()
            .HasMany(user => user.Roles);

        //Role
        modelBuilder 
            .Entity<Role>()
            .HasKey(role => role.Id);

        modelBuilder
            .Entity<Role>()
            .HasIndex(role => role.Name)
            .IsUnique();

        modelBuilder
            .Entity<Role>()
            .HasMany(role => role.Users);

        //UserRole
        modelBuilder
            .Entity<UserRole>()
            .HasKey(userRole => userRole.Id);

        //Town
        modelBuilder
            .Entity<Town>()
            .HasKey(town => town.Id);

        modelBuilder
            .Entity<Town>()
            .HasIndex(town => town.Name)
            .IsUnique();

        modelBuilder
            .Entity<Town>()
            .HasMany(town => town.Polyclinics)
            .WithOne(polyclinic => polyclinic.Town)
            .HasForeignKey(polyclinic => polyclinic.TownId)
            .IsRequired();

        //Polyclinic
        modelBuilder
            .Entity<Polyclinic>()
            .HasKey(polyclinic => polyclinic.Id);

        modelBuilder
            .Entity<Polyclinic>()
            .HasIndex(polyclinic => polyclinic.Name);

        modelBuilder
            .Entity<Polyclinic>()
            .HasOne(polyclinic => polyclinic.Town)
            .WithMany(town => town.Polyclinics)
            .HasForeignKey(polyclinic => polyclinic.TownId)
            .IsRequired();

        modelBuilder
            .Entity<Polyclinic>()
            .HasMany(polyclinic => polyclinic.Doctors);

        //Doctor
        modelBuilder
            .Entity<Doctor>()
            .HasKey(doctor => doctor.Id);

        modelBuilder
            .Entity<Doctor>()
            .HasIndex(doctor => doctor.FullName);

        modelBuilder
            .Entity<Doctor>()
            .HasMany(doctor => doctor.Polyclinics);

        modelBuilder
            .Entity<Doctor>()
            .HasMany(doctor => doctor.Specializations);

        //Specialization
        modelBuilder
            .Entity<Specialization>()
            .HasKey(specialization => specialization.Id);

        modelBuilder
            .Entity<Specialization>()
            .HasIndex(specialization => specialization.Name)
            .IsUnique();

        modelBuilder
            .Entity<Specialization>()
            .HasMany(specialization => specialization.Doctors);

        //DoctorSpecialization
        modelBuilder
            .Entity<DoctorSpecialization>()
            .HasKey(doctorSpecialization => doctorSpecialization.Id);
        
        //PolyclinicDoctor
        modelBuilder
            .Entity<PolyclinicDoctor>()
            .HasKey(polyclinicDoctor => polyclinicDoctor.Id);
    }
}