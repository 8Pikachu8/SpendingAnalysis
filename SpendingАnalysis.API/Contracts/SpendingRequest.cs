namespace SpendingAnalysis.API.Contracts
{
    public record SpendingRequest(string Description, decimal Amount, DateTime Date, Guid CategoryId);
}
