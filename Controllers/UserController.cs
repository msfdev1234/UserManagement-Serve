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
using UserManagement_Serv.Models;

namespace UserManagement_Serv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper _mapper;
        public UserController(AppDbContext aappDbContext, IMapper mapper)
        {
            this.appDbContext = aappDbContext;
            this._mapper = mapper;
        }

        
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObject)
        {
            if(userObject == null)
                return BadRequest();

            var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == userObject.Email && x.Password == userObject.Password);

            if(user == null)
                return NotFound(new {Message = "User Not Found"});

            userObject.Token = createJwtToken(userObject);

            return Ok( new { Message = "Logged in Succesfull", Token = userObject.Token, Name = user.Name } );
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObject)
        {
            if(userObject==null) 
                return BadRequest();

            if(await IsUserExists(userObject.Email))
                return BadRequest(new {Message = "User already exists"});

            await appDbContext.Users.AddAsync(userObject);
            await appDbContext.SaveChangesAsync();

            userObject.Token = createJwtToken(userObject);

            return Ok(new 
            {
                Token = userObject.Token,
                Message = "Registered successfully" + userObject.Name
            });



        }


        [Authorize]
        [HttpGet("users")]
        public async Task<ActionResult<User>> getusers()
        {
            var usersDto = new UserDto();
            var users = await appDbContext.Users.ToListAsync();

            return Ok(users.Select(user => _mapper.Map<UserDto>(user)));

            //return Ok(users);
        }

        private async Task<bool> IsUserExists(String email)
        {
            return await appDbContext.Users.AnyAsync(x => x.Email == email);
        }

        private String createJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("mazharsecretkeymohdmazharpatel@gmail.commohdmazharpatel3l@gmail.com");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = credentials

            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
         }

    }
}
