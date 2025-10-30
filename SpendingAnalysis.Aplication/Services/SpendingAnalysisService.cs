using SpendingAnalysis.Core.Abstractions;
using SpendingAnalysis.Core.Models;
using System.Collections.Generic;

namespace SpendingAnalysis.Aplication.Services
{
    public class SpendingAnalysisService : ISpendingAnalysisService
    {
        private ISpendingRepository _spendingRepository;
        private readonly ICacheService _cache;
        private ICategoriesService _categoriesService;

        public SpendingAnalysisService(ISpendingRepository spendingRepository, ICacheService cache, ICategoriesService categoriesService)
        {
            _spendingRepository = spendingRepository;
            _cache = cache;
            _categoriesService = categoriesService;
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

        public async Task<List<GroupingSpending>> GetGroupedSpendings(Guid userId)
        {
            var categories = (await _categoriesService.GetCategories(userId))
                .ToDictionary(c => c.Id);

            var allSpendings = await _spendingRepository.GetByUserId(userId);

            var groupedSpendings = allSpendings
                .GroupBy(s => s.Date)
                .Select(g => new GroupingSpending
                {
                    DateSpending = g.Key,
                    Spendings = g.ToList(),
                    SpendingSum = g
                        .Where(s => categories.TryGetValue(s.CategoryId, out var cat) &&
                                    cat.OperationType == OperationdTypeEnum.Expense)
                        .Sum(s => s.Amount)
                })
                .OrderBy(g => g.DateSpending)
                .ToList();

            return groupedSpendings;
        }


    }
}
