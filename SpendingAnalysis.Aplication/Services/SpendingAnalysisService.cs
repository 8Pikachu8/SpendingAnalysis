using SpendingAnalysis.Core.Abstractions;
using SpendingAnalysis.Core.Models;

namespace SpendingAnalysis.Aplication.Services
{
    public class SpendingAnalysisService : ISpendingAnalysisService
    {
        private ISpendingRepository _spendingRepository;

        public SpendingAnalysisService(ISpendingRepository spendingRepository)
        {
            _spendingRepository = spendingRepository;
        }

        public async Task<List<Spending>> GetSpendingsByUserId(Guid userId)
        {
            return await _spendingRepository.GetByUserId(userId);
        }

        public async Task<Guid> AddSpending(Spending spending)
        {
            return await _spendingRepository.Add(spending);
        }

        public async Task<Guid[]> DeleteSpending(Guid[] ids)
        {
            return await _spendingRepository.Delete(ids);
        }

        public async Task<Guid> UpdateSpending(Guid id, string description, decimal amount, DateTime date)
        {
            return await _spendingRepository.Update(id, description, amount, date);
        }

    }
}
