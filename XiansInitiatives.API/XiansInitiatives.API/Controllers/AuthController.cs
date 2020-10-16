using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using XiansInitiatives.Shared.Models;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Constants;

namespace XiansInitiatives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singInManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthController(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        RoleManager<IdentityRole> roleManager,
                                        IMapper mapper,
                                        IConfiguration config)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _roleManger = roleManager;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/Auth/Register
        public async Task<Object> RegisterNewUser(UserForRegisterDto userForRegisterDto)
        {
            var userToCreate = _mapper.Map<ApplicationUser>(userForRegisterDto);
            try
            {
                var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

                if (!await _roleManger.RoleExistsAsync(UserRoles.AdminRole))
                {
                    _roleManger.CreateAsync(new IdentityRole(UserRoles.AdminRole)).GetAwaiter().GetResult();
                    _roleManger.CreateAsync(new IdentityRole(UserRoles.EvaluatorRole)).GetAwaiter().GetResult();
                    _roleManger.CreateAsync(new IdentityRole(UserRoles.LeadRole)).GetAwaiter().GetResult();
                    _roleManger.CreateAsync(new IdentityRole(UserRoles.UserRole)).GetAwaiter().GetResult();
                }

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userToCreate, UserRoles.UserRole);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("login")]

        //POST : /api/Auth/Login
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userForLoginDto.Username);

                if (user == null)
                {
                    return BadRequest("User not exists");
                }
                var result = await _singInManager
                    .CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

                if (!result.Succeeded)
                {
                    return BadRequest("Password is incorrect");
                }

                var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.Username.ToUpper());

                if (result.Succeeded)
                {
                    return Ok(new
                    {
                        token = GenerateJwtToken(appUser).Result,
                        user
                    });
                }
            }

            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}