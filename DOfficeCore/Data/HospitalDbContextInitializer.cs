using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DOfficeCore.Data
{
    class HospitalDbContextInitializer : IDesignTimeDbContextFactory<HospitalDb>
    {
        public HospitalDb CreateDbContext(string[] args)
        {
            const string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hospital.DB;Integrated Security=True";

            return new HospitalDb(
                new DbContextOptionsBuilder<HospitalDb>()
                .UseSqlServer(connection)
                .Options);
        }
    }
}
