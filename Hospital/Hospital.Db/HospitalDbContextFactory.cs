using EnsureThat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hospital.Db;

internal sealed class HospitalDbContextFactory : IDesignTimeDbContextFactory<HospitalContext>
{
    public HospitalContext CreateDbContext(string[] args)
    {
        EnsureArg.HasItems(args, nameof(args));

        var optionsBuilder = new DbContextOptionsBuilder<HospitalContext>();
        optionsBuilder.UseSqlServer(args[0],
            builder => builder.MigrationsAssembly(typeof(HospitalDbContextFactory).Assembly.GetName().Name));

        return new HospitalContext(optionsBuilder.Options);
    }
}