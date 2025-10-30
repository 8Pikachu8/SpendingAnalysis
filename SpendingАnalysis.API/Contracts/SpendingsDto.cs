namespace SpendingАnalysis.Contracts
{
    public record SpendingsDto(Guid Id, string Description, decimal Amount, string Date, Guid CategoryId);
}
