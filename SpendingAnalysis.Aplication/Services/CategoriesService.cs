using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendingAnalysis.Core.Abstractions;
using SpendingAnalysis.Core.Models;

namespace SpendingAnalysis.Aplication.Services
{
    public class CategoriesService : ICategoriesService
    {
        private ICategoriesRepository _categoriesRepository;
        private readonly ICacheService _cache;

        public CategoriesService(ICategoriesRepository categoriesRepository, ICacheService cache)
        {
            _categoriesRepository = categoriesRepository;
            _cache = cache;
        }

        private string GetUserCategoriesCacheKey(Guid userId) => $"user:{userId}:categories";

        public async Task<List<Category>> GetCategories(Guid userId)
        {
            // Сначала пробуем получить из кэша
            var cached = await _cache.GetAsync<List<Category>>(GetUserCategoriesCacheKey(userId));
            if (cached != null) return cached;


            var categories = await _categoriesRepository.Get(userId);

            // Сохраняем в кэш на 10 минут
            await _cache.SetAsync(GetUserCategoriesCacheKey(userId), categories, TimeSpan.FromMinutes(10));

            return categories;

        }

        public async Task<Guid> AddCategory(Category category)
        {
            return await _categoriesRepository.Add(category);
        }

        public async Task<Guid[]> DeleteCategory(Guid[] categoryIds)
        {
            return await _categoriesRepository.Delete(categoryIds);
        }

        public async Task<List<OperationType>> GetOperationdTypes()
        {
            return await _categoriesRepository.GetOperationTypes();
        }

        public async Task InsertDefaultCategories(Guid userId)
        {
            await _categoriesRepository.InsertDefaultCategories(userId);
        }
    }
}
