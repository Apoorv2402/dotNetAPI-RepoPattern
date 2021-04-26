using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudyMash.API.DTOS;
using StudyMash.API.Interfaces;
using StudyMash.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudyMash.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configs;

        public UserController(IUnitOfWork uow,IConfiguration configs)
        {
            _uow = uow;
            _configs = configs;
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(UserRequestDTO reqDto)
        {
            var user = await _uow.UserRepo.Authenticate(reqDto.Username, reqDto.Password);

            if(user == null)
            {
                return Unauthorized();
            }
            else
            {
                var responseDto = new UserResponseDTO()
                {
                    Username = user.Username,
                    Token = CreateJWT(user)
                };
                return Ok(responseDto);
            }

            
        }


        // JWt creator
        private string CreateJWT(User user)
        { 
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configs.GetSection("AppSettings:Key").Value));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())

            };

            var signingCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        
        }


    }
}
