using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyAdministrationCompensationService : BaseService<FRECompanyAdministrationCompensation>, IFRECompanyAdministrationCompensationService
    {
        public FRECompanyAdministrationCompensationService(IConfiguration config, ILogger<FRECompanyAdministrationCompensation> logger) : base(config, logger) => DbName = "frecompanyadministrationcompensations";

    }
}
