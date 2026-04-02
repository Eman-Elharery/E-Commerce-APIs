using CompanySystem.BLL;
using CompanySystem.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace lab11
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser>   _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings                    _jwtSettings;

        public AuthController(
            UserManager<ApplicationUser>   userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtSettings>          jwtSettings)
        {
            _userManager   = userManager;
            _signInManager = signInManager;
            _jwtSettings   = jwtSettings.Value;
        }

     
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                FirstName = dto.FirstName,
                LastName  = dto.LastName,
                UserName  = dto.Email.Split('@')[0],
                Email     = dto.Email,
            };

            var createResult = await _userManager.CreateAsync(user, dto.Password);
            if (!createResult.Succeeded)
                return BadRequest(createResult.Errors);

            var role = dto.Role is "Admin" or "User" ? dto.Role : "User";
            var addRoleResult = await _userManager.AddToRoleAsync(user, role);
            if (!addRoleResult.Succeeded)
                return BadRequest(addRoleResult.Errors);

            return Ok("Successfully registered user");
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
          
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                return Unauthorized("Invalid Email or Password");

            var passwordOk = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!passwordOk)
                return Unauthorized("Invalid Email or Password");

            
            if (await _userManager.IsLockedOutAsync(user))
                return Unauthorized("Account is temporarily locked. Please try again later.");

           
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name,           user.UserName!),
                new(ClaimTypes.Email,          user.Email!),
                new("firstName",               user.FirstName),
                new("lastName",                user.LastName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var r in roles)
                claims.Add(new Claim(ClaimTypes.Role, r));

            var tokenDto = GenerateToken(claims);
            return Ok(tokenDto);
        }

        private TokenDto GenerateToken(List<Claim> claims)
        {
            var keyBytes = Convert.FromBase64String(_jwtSettings.SecretKey);
            var key      = new SymmetricSecurityKey(keyBytes);
            var creds    = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry   = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);

            var jwt = new JwtSecurityToken(
                issuer:             _jwtSettings.Issuer,
                audience:           _jwtSettings.Audience,
                claims:             claims,
                expires:            expiry,
                signingCredentials: creds
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new TokenDto(token, _jwtSettings.DurationInMinutes);
        }
    }
}
