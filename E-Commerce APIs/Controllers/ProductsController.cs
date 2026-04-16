using CompanySystem.BLL;
using CompanySystem.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lab11
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(
            IProductManager productManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _productManager = productManager;
            _webHostEnvironment = webHostEnvironment;
        }

        /*------------------------------------------------*/

        [HttpGet]
        [Authorize(Policy = AuthPolicies.AdminOrUser)]
        public async Task<IActionResult> GetProducts(
            [FromQuery] ProductFilterParameters? filter,
            [FromQuery] PaginationParameters? pagination)
        {
            var result = await _productManager.GetProductsAsync(
                filter ?? new ProductFilterParameters(),
                pagination ?? new PaginationParameters { PageNumber = 1, PageSize = 50 });

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpGet("{id}")]
        [Authorize(Policy = AuthPolicies.AdminOrUser)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productManager.GetProductByIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpPost]
        [Authorize(Policy = AuthPolicies.AdminOnly)]
        public async Task<IActionResult> Create(ProductCreateDTO dto)
        {
            var result = await _productManager.CreateProductAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpPut("{id}")]
        [Authorize(Policy = AuthPolicies.AdminOnly)]
        public async Task<IActionResult> Update(int id, ProductEditDTO dto)
        {
            var result = await _productManager.UpdateProductAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        /*------------------------------------------------*/

        [HttpDelete("{id}")]
        [Authorize(Policy = AuthPolicies.AdminOnly)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productManager.DeleteProductAsync(id);

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

            var result = await _productManager.UploadProductImageAsync(id, dto, basePath, schema, host);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}