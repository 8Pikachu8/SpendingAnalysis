using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendingAnalysis.Core.Models
{
    public class Spending
    {
        private Spending(Guid id, string description, decimal amount, DateTime date)
        {
            Id = id;
            Description = description;
            Amount = amount;
            Date = date;
        }

        public Guid Id { get; }

        public string Description { get; }

        public decimal Amount { get; }

        public DateTime Date { get; }

        public static (Spending Spending, string Error) Create(Guid guid, string description, decimal amount, DateTime date)
        {
            var errror = string.Empty;
            if (string.IsNullOrWhiteSpace(description))
            {
                errror = "Description cannot be empty.";
            }
            if (amount <= 0)
            {
                errror = "Amount must be greater than zero.";
            }
            if (date > DateTime.Now)
            {
                errror = "Date cannot be in the future.";
            }

            var spending = new Spending(guid, description, amount, date);
            return (spending, errror);
        }
    }
}
