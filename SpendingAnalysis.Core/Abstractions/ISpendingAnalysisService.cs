using SpendingAnalysis.Core.Models;

namespace SpendingAnalysis.Core.Abstractions
{
    public interface ISpendingAnalysisService
    {
        Task<Guid> AddSpending(Spending spending);
        Task<Guid> DeleteSpending(Guid id);
        Task<List<Spending>> GetAllSpendings();
        Task<Guid> UpdateSpending(Guid id, string description, decimal amount, DateTime date);
    }
}