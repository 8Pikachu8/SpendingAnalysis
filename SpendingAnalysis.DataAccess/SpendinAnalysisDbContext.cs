using Microsoft.EntityFrameworkCore;
using SpendingAnalysis.DataAccess.Entities;

namespace SpendingAnalysis.DataAccess
{
    public class SpendinAnalysisDbContext : DbContext
    {
        public SpendinAnalysisDbContext(DbContextOptions<SpendinAnalysisDbContext> options) : base(options)
        {

        }

        public DbSet<OperationEntity> Operations { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<OperationTypeEntity> OperationTypes { get; set; }

        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
