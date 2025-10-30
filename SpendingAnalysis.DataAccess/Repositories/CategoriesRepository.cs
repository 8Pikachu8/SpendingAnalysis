
using Microsoft.EntityFrameworkCore;
using SpendingAnalysis.Core.Abstractions;
using SpendingAnalysis.Core.Models;
using SpendingAnalysis.DataAccess.Consts;
using SpendingAnalysis.DataAccess.Entities;

namespace SpendingAnalysis.DataAccess.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private SpendinAnalysisDbContext _context;

        public CategoriesRepository(SpendinAnalysisDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Get(Guid userId)
        {
            var categoryEntities = await _context.Categories
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .ToListAsync();

            var category = categoryEntities
                .Select(x => Category.Create(x.Id, x.Name, (OperationdTypeEnum)x.OperationTypeId, userId).Category)
                .ToList();

            return category;
        }

        public async Task<Guid> Add(Category category)
        {
            var entity = new CategoryEntity
            {
                Id = category.Id,
                Name = category.Name,
                OperationTypeId = (int)category.OperationType,
                UserId = category.UserId
            };
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid[]> Delete(Guid[] ids)
        {
            var affectedRows = await _context.Categories.Where(x => ids.Contains(x.Id)).ExecuteDeleteAsync();
            return ids;
        }

        public async Task<List<OperationType>> GetOperationTypes()
        {
            var operationTypeEntity = await _context.OperationTypes
                .AsNoTracking()
                .ToListAsync();

            var operationTypes = operationTypeEntity.Select(x => OperationType.Create((OperationdTypeEnum)x.Id, x.Name).Category).ToList();

            return operationTypes;
        }

        public async Task InsertDefaultCategories(Guid userId)
        {
            var categoriesDefault = CommonCosnts.CategoryEntities;

            foreach (var category in categoriesDefault) 
            {
                category.UserId = userId;
            }

            await _context.Categories.AddRangeAsync(categoriesDefault);
            await _context.SaveChangesAsync();
        }
    }
}
