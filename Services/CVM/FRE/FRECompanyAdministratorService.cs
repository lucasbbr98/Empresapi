using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyAdministratorService : BaseService<FRECompanyAdministrator>, IFRECompanyAdministratorService
    {
        public FRECompanyAdministratorService(IConfiguration config, ILogger<FRECompanyAdministrator> logger) : base(config, logger) => DbName = "frecompanyadministrators";

    }
}
