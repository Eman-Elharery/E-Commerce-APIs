using global::CompanySystem.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
namespace lab11
{
    
        [ApiController]
        [Route("api/[controller]")]
        [Authorize]
    public class CategoriesController : ControllerBase
        {
            private readonly ICategoryManager _categoryManager;

            public CategoriesController(ICategoryManager categoryManager)
            {
                _categoryManager = categoryManager;
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
        }
    }

