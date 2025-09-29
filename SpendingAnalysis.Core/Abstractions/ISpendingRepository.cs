using SpendingAnalysis.Core.Models;

namespace SpendingAnalysis.Core.Abstractions
{
    public interface ISpendingRepository
    {
        Task<Guid> Add(Spending spending);
        Task<Guid> Delete(Guid id);
        Task<List<Spending>> Get();
        Task<Guid> Update(Guid id, string description, decimal amount, DateTime date);
    }
}