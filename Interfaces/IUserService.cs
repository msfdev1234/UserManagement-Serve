using UserManagement_Serv.Models;

namespace UserManagement_Serv.Interfaces
{
    public interface IUserService
    {
        public Task<User> VerifyUserCredentials(User userObject);
        public Task<object> GetAllUsers();
        public Task<object> AddNewUser(User userObject);

    }
}
