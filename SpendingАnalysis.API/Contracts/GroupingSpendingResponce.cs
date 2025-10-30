using SpendingAnalysis.Core.Models;
using SpendingАnalysis.Contracts;

namespace SpendingAnalysis.API.Contracts
{
    public class GroupingSpendingResponce
    {
        public GroupingSpendingResponce(string dateSpending, int spendingSum, List<Spending> spendings)
        {
            Date = dateSpending;
            Summ = spendingSum;
            Spendings = spendings.Select(x => new SpendingsDto(x.Id, x.Description, x.Amount, x.Date.ToString("dd MMMM, ddd"), x.CategoryId)).ToList();
        }

        public string Date { get; set; }

        public int Summ {  get; set; }

        public List<SpendingsDto> Spendings { get; set; }
    }
}
