using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingAnalysis.DataAccess.Entities
{
    public class OperationTypeEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
    }
}
