using Microsoft.EntityFrameworkCore;
using SpendingAnalysis.Core.Abstractions;
using SpendingAnalysis.Core.Models;
using SpendingAnalysis.DataAccess.Entities;

namespace SpendingAnalysis.DataAccess.Repositories
{
    public class SpendingRepository : ISpendingRepository
    {
        private SpendinAnalysisDbContext _context;

        public SpendingRepository(SpendinAnalysisDbContext context)
        {
            _context = context;
        }

        public async Task<List<Spending>> Get()
        {
            var spendingEntities = await _context.Operations
                .AsNoTracking()
                .ToListAsync();

            var spendings = spendingEntities
                .Select(x => Spending.Create(x.Id, x.Description, x.Amount, x.Date.ToLocalTime(), new Guid(), x.CategoryId).Spending)
                .ToList();

            return spendings;
        }

        public async Task<List<Spending>> GetByUserId(Guid userId)
        {
            var spendingEntities = await _context.Operations
                .AsNoTracking()
                .ToListAsync();

            var spendings = spendingEntities.Where(x => x.UserId == userId)
                .Select(x => Spending.Create(x.Id, x.Description, x.Amount, x.Date.ToLocalTime(), x.UserId, x.CategoryId).Spending)
                .ToList();

            return spendings;
        }

        public async Task<Guid> Add(Spending spending)
        {
            var entity = new OperationEntity
            {
                Id = spending.Id,
                Description = spending.Description,
                Amount = spending.Amount,
                Date = spending.Date,
                UserId = spending.UserId,
                CategoryId = spending.CategoryId,
            };
            await _context.Operations.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> Update(Guid id, string description, decimal amount, DateTime date)
        {
            await _context.Operations
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Description, description)
                    .SetProperty(p => p.Amount, amount)
                    .SetProperty(p => p.Date, date));

            return id;
        }

        public async Task<Guid[]> Delete(Guid[] ids)
        {
            var affectedRows = await _context.Operations.Where(x => ids.Contains(x.Id)).ExecuteDeleteAsync();
            return ids;
        }
    }
}
