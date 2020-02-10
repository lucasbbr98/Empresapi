using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Models;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IConfiguration config, ILogger<User> logger) : base(config, logger) => DbName = "users";

    }
}
