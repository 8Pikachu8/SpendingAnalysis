using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingAnalysis.Core.Models
{
    public class GroupingSpending
    {
        public DateTime DateSpending { get; set; }

        public decimal SpendingSum { get; set; }

        public List<Spending> Spendings { get; set; }
    }
}
