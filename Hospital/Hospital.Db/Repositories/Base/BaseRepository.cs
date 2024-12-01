using EnsureThat;

namespace Hospital.Db.Repositories.Base;

public abstract class BaseRepository : IRepository
{
    public HospitalContext HospitalContext { get; }

    protected BaseRepository(HospitalContext hospitalContext)
    {
        EnsureArg.IsNotNull(hospitalContext, nameof(hospitalContext));

        HospitalContext = hospitalContext;
    }
}