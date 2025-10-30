using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingAnalysis.DataAccess.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int OperationTypeId { get; set; }   // 🔸 внешний ключ

        public Guid UserId { get; set; }// FK

        public UserEntity User { get; set; }// навигационное свойство

        public OperationTypeEntity OperationType { get; set; } = null!; // навигация
    }

}
