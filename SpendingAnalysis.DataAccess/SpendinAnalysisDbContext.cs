using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpendingAnalysis.DataAccess.Entities;

namespace SpendingAnalysis.DataAccess
{
    public class SpendinAnalysisDbContext : DbContext
    {
        public SpendinAnalysisDbContext(DbContextOptions<SpendinAnalysisDbContext> options) : base(options)
        {

        }

        public DbSet<SpendingEntity> Spendings { get; set; }
    }
}
