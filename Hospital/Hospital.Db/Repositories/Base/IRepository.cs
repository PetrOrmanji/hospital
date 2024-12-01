namespace Hospital.Db.Repositories.Base;

public interface IRepository
{
    HospitalContext HospitalContext { get; }
}