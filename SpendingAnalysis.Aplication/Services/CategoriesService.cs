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

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<List<Category>> GetCategories(Guid userId)
        {
            return await _categoriesRepository.Get(userId);
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
