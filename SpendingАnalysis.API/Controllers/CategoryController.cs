using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpendingAnalysis.API.Contracts;
using SpendingAnalysis.Core.Abstractions;
using SpendingAnalysis.Core.Models;
using SpendingАnalysis.Contracts;

namespace SpendingAnalysis.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private ICategoriesService _categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<List<CategoryResponse>>> GetCategories()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);
            var categories = await _categoriesService.GetCategories(userId);
            var responce = categories
                .Select(x => new CategoryResponse(x.Id, x.Name, x.OperationType))
                .ToList();
            return Ok(responce);
        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult<Guid>> AddSpendings([FromBody] CategoryRequest dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);
            var id = await _categoriesService.
                AddCategory(Category.Create(Guid.NewGuid(), dto.Name, dto.OperationType, userId).Category);

            return Ok(id);
        }

        [HttpDelete]
        public async Task<ActionResult<Guid[]>> DeleteSpending([FromQuery] Guid[] ids)
        {
            var SpId = await _categoriesService.
                DeleteCategory(ids);

            return Ok(SpId);
        }

        [HttpGet("GetOperationTypes")]
        public async Task<ActionResult<List<OperationTypeResponse>>> GetOperationTypes()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);
            var categories = await _categoriesService.GetOperationdTypes();
            var responce = categories
                .Select(x => new OperationTypeResponse(x.Id, x.Name))
                .ToList();
            return Ok(responce);
        }
    }
}
