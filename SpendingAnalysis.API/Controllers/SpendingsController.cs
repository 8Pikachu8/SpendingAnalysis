using Microsoft.AspNetCore.Mvc;
using SpendingAnalysis.Core.Abstractions;
using SpendingАnalysis.Contracts;

namespace SpendingАnalysis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpendingsController : ControllerBase
    {
        private ISpendingAnalysisService _spendingAnalysisService;

        public SpendingsController(ISpendingAnalysisService spendingAnalysisService)
        {
            _spendingAnalysisService = spendingAnalysisService;
        }

        [HttpGet("GetAllSpendings")]
        public async Task<ActionResult<List<SpendingsDto>>> GetAllSpendings()
        {
            var spendings = await _spendingAnalysisService.GetAllSpendings();
            var responce = spendings
                .Select(x => new SpendingsDto(x.Id,x.Description,x.Amount,x.Date))
                .ToList();
            return Ok(responce);
        }
    }
}
