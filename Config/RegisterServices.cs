using UserManagement_Serv.Interfaces;
using UserManagement_Serv.Services;

namespace UserManagement_Serv.Config
{
    public class RegisterServices
    {
        public static IServiceCollection _services;
        public static IConfiguration _configuration;

        public static void RegisterComponents(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;

            //Register all the services here
            _services.AddTransient<IUserService, UserService>();
            _services.AddTransient<ITokenService, TokenService>();
        }
    }
}
