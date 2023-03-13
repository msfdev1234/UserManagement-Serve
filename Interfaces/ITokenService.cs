using UserManagement_Serv.Dto;
using UserManagement_Serv.Models;

namespace UserManagement_Serv.Interfaces
{
    public interface ITokenService
    {
        public String GenerateTokens(User userObject);

    }
}
