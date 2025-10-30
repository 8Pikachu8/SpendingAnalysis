using SpendingAnalysis.Core.Models;

namespace SpendingAnalysis.API.Contracts
{
    public record CategoryRequest(string Name, OperationdTypeEnum OperationType);
}
