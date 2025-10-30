using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingAnalysis.Core.Models
{
    public class Category
    {

        // Публичный конструктор без параметров для JsonSerializer
        public Category() { }
        private Category(Guid id, string name, OperationdTypeEnum operType, Guid userId)
        {
            Id = id;
            Name = name;
            OperationType = operType;
            UserId = userId;
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public OperationdTypeEnum OperationType { get; set; }   // 🔸 внешний ключ

        public Guid UserId { get; set; }

        public static (Category Category, string Error) Create(Guid id, string name, OperationdTypeEnum operType, Guid userId)
        {
            var errror = string.Empty;
            if (string.IsNullOrWhiteSpace(name))
            {
                errror = "Name cannot be empty.";
            }

            var category = new Category(id, name, operType, userId);
            return (category, errror);
        }
    }

    public enum OperationdTypeEnum
    {
        /// <summary>
        /// Расход
        /// </summary>
        Expense,
        /// <summary>
        /// Доход
        /// </summary>
        Income,
        /// <summary>
        /// Перевод
        /// </summary>
        Transfer,
        /// <summary>
        /// Долг
        /// </summary>
        Debt
    }
}
