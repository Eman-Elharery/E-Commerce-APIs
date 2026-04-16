using CompanySystem.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lab11
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(
            ICategoryManager categoryManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _categoryManager = categoryManager;
            _webHostEnvironment = webHostEnvironment;
        }

        /*------------------------------------------------*/

        [HttpGet]
        [Authorize(Policy = AuthPolicies.AdminOrUser)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryManager.GetCategoriesAsync();

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpGet("{id}")]
        [Authorize(Policy = AuthPolicies.AdminOrUser)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoryManager.GetCategoryByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpPost]
        [Authorize(Policy = AuthPolicies.AdminOnly)]
        public async Task<IActionResult> Create(CategoryCreateDTO dto)
        {
            var result = await _categoryManager.CreateCategoryAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpPut("{id}")]
        [Authorize(Policy = AuthPolicies.AdminOnly)]
        public async Task<IActionResult> Update(int id, CategoryEditDTO dto)
        {
            var result = await _categoryManager.UpdateCategoryAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpDelete("{id}")]
        [Authorize(Policy = AuthPolicies.AdminOnly)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryManager.DeleteCategoryAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpPost("{id}/image")]
        [Authorize(Policy = AuthPolicies.AdminOnly)]
        public async Task<IActionResult> UploadImage(int id, [FromForm] ImageUploadDto dto)
        {
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;

            var result = await _categoryManager.UploadCategoryImageAsync(id, dto, basePath, schema, host);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}