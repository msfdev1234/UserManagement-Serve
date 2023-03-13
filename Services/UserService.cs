using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManagement_Serv.Context;
using UserManagement_Serv.Dto;
using UserManagement_Serv.Interfaces;
using UserManagement_Serv.Models;

namespace UserManagement_Serv.Services
{
    public class UserService : IUserService
    {

        private readonly AppDbContext appDbContext;
        private readonly IMapper _mapper;

        public UserService(AppDbContext aappDbContext, IMapper mapper)
        {
            this.appDbContext = aappDbContext;
            this._mapper = mapper;
        }

        public async Task<User> VerifyUserCredentials(User userObject)
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == userObject.Email && x.Password == userObject.Password);
            return user;
           
        }

        public async Task<object> GetAllUsers()
        {
            var usersDto = new UserDto();
            var users = await appDbContext.Users.ToListAsync();

            return (users.Select(user => _mapper.Map<UserDto>(user)));

        }

        public async Task<object> AddNewUser(User userObject)
        {

            if (await IsUserExists(userObject.Email))
                return null;

            await appDbContext.Users.AddAsync(userObject);
            await appDbContext.SaveChangesAsync();

            return (new { Message = "User Registered successfully" });
        }

        private async Task<bool> IsUserExists(String email)
        {
            return await appDbContext.Users.AnyAsync(x => x.Email == email);
        }
    }
}
