using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingAnalysis.DataAccess.Entities
{
    public class OperationEntity
    {
        public Guid Id { get; set; }

        public required string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        // Новый внешний ключ
        public Guid UserId { get; set; }// FK

        public UserEntity User { get; set; }// навигационное свойство

        public Guid CategoryId { get; set; }

        public CategoryEntity Category { get; set; } = null!;
    }
}
