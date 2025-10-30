using SpendingAnalysis.Core.Models;

namespace SpendingAnalysis.API.Contracts
{
    public record CategoryResponse(Guid Id, string Name, OperationdTypeEnum OperationType);
}
