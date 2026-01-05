using AddressBook.Domain.Entities;
using AddressBook.Presentation.DTOs.AccountDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AddressBook.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IConfiguration config;
        UserManager<ApplicationUser> userManager;
        IMapper _map;
        public AccountController(UserManager<ApplicationUser> userManager, IMapper _map, IConfiguration config)
        {
            this.userManager = userManager;
            this._map = _map;
            this.config = config;

        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDTO userFromRequest) {
            if (ModelState.IsValid)
            {
                ApplicationUser newUser = _map.Map<ApplicationUser>(userFromRequest);
                IdentityResult result= await userManager.CreateAsync(newUser, userFromRequest.Password);
                if (result.Succeeded)
                {
                    return Created();
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("Password",item.Description);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO userFromRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userFromDB = await userManager.FindByNameAsync(userFromRequest.UserName);
            if (userFromDB == null)
                return Unauthorized("UserName or Password is invalid");

            bool found = await userManager.CheckPasswordAsync(userFromDB, userFromRequest.Password);
            if (!found)
                return Unauthorized("UserName or Password is invalid");

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, userFromDB.Id),
        new Claim(ClaimTypes.Name, userFromDB.UserName)
    };

            var roles = await userManager.GetRolesAsync(userFromDB);
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["JWT:SecritKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["JWT:IssuerIP"],
                audience: config["JWT:AudienceIP"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = DateTime.UtcNow.AddHours(1)
            });
        }



    }
}
