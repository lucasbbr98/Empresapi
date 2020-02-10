using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Models;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        public CompanyService(IConfiguration config, ILogger<Company> logger) : base(config, logger) => DbName = "companies";

    }
}
