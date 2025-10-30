using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendingAnalysis.Core.Models;

namespace SpendingAnalysis.Core.Abstractions
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> Get(Guid userId);

        Task<Guid> Add(Category category);

        Task<Guid[]> Delete(Guid[] ids);

        Task<List<OperationType>> GetOperationTypes();

        Task InsertDefaultCategories(Guid userId);
    }
}
