using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement_Serv.Context;
using UserManagement_Serv.Dto;
using UserManagement_Serv.Interfaces;
using UserManagement_Serv.Models;
using UserManagement_Serv.Services;

namespace UserManagement_Serv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(AppDbContext aappDbContext, IMapper mapper, IUserService userService, ITokenService tokenService)
        {
            this.appDbContext = aappDbContext;
            this._mapper = mapper;
            _userService = userService;
            _tokenService = tokenService;

        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] User userObject)
        {
            if (userObject == null)
                return BadRequest();

            var response = await _userService.VerifyUserCredentials(userObject);

            if (response == null)
                return NotFound(new { Message = "User Not Found" });

            var token = _tokenService.GenerateTokens(userObject);

            return Ok(new ResponseModel("Logged in successfully", token, response.Name));

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObject)
        {
            if (userObject == null)
                return BadRequest(new { Message = "Please provide accurate inputs...!" });

            var response = await _userService.AddNewUser(userObject);

            if (response == null)
                return BadRequest(new { Message = "User already exists" });

            var token = _tokenService.GenerateTokens(userObject);

            return Ok(new
            {
                Token = token,
                Message = "Registered successfully" + userObject.Name
            });

        }


        [Authorize]
        [HttpGet("users")]
        public async Task<ActionResult<User>> getusers()
        {
            var response = await _userService.GetAllUsers();

            return Ok(response);

        }
    }
}
