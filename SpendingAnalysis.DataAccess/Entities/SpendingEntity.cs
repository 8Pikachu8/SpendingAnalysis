using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingAnalysis.DataAccess.Entities
{
    public class SpendingEntity
    {
        public Guid Id { get; set; }

        public required string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
