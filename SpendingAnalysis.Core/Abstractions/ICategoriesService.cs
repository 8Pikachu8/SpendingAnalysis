using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendingAnalysis.Core.Models;

namespace SpendingAnalysis.Core.Abstractions
{
    public interface ICategoriesService
    {
        Task<List<Category>> GetCategories(Guid userId);

        Task<Guid> AddCategory(Category category);

        Task<Guid[]> DeleteCategory(Guid[] categoryIds);

        Task<List<OperationType>> GetOperationdTypes();

        Task InsertDefaultCategories(Guid userId);
    }
}
