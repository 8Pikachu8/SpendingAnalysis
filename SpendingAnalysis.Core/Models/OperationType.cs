using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingAnalysis.Core.Models
{
    public class OperationType
    {
        private OperationType(OperationdTypeEnum id, string name)
        {
            Id = id;
            Name = name;
        }

        public OperationdTypeEnum Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public static (OperationType Category, string Error) Create(OperationdTypeEnum id, string name)
        {
            var errror = string.Empty;
            if (string.IsNullOrWhiteSpace(name))
            {
                errror = "Name cannot be empty.";
            }

            var operationType = new OperationType(id, name);
            return (operationType, errror);
        }
    }
}
