using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpendingAnalysis.API.Contracts;
using SpendingAnalysis.Core.Abstractions;
using SpendingAnalysis.Core.Models;
using SpendingАnalysis.Contracts;

namespace SpendingАnalysis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);
            var spendings = await _spendingAnalysisService.GetSpendingsByUserId(userId);
            var responce = spendings
                .Select(x => new SpendingsDto(x.Id, x.Description, x.Amount, x.Date.ToString("dd MMMM, ddd"), x.CategoryId))
                .ToList();
            return Ok(responce);
        }

        [HttpPost("AddSpendings")]
        public async Task<ActionResult<Guid>> AddSpendings([FromBody]SpendingRequest dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);
            var id = await _spendingAnalysisService.
                AddSpending(Spending.Create(Guid.NewGuid(), dto.Description, dto.Amount, dto.Date.ToUniversalTime(), userId, dto.CategoryId).Spending);
            
            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateSpending(Guid id, SpendingRequest spendingRequest)
        {
            var SpId = await _spendingAnalysisService.
                UpdateSpending(id, spendingRequest.Description, spendingRequest.Amount, spendingRequest.Date);

            return Ok(SpId);
        }

        [HttpDelete]
        public async Task<ActionResult<Guid[]>> DeleteSpending([FromQuery] Guid[] ids)
        {
            var SpId = await _spendingAnalysisService.
                DeleteSpending(ids);

            return Ok(SpId);
        }
    }
}
