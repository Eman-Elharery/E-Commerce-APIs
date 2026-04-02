using CompanySystem.BLL;
using CompanySystem.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lab11
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = AuthPolicies.AdminOnly)]   
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleCreateDto dto)
        {
            var role = new ApplicationRole
            {
                Name        = dto.Name,
                Description = dto.Description
            };

            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result);
        }
    }
}
