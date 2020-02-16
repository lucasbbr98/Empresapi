using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyOwnershipService : BaseService<FRECompanyOwnership>, IFRECompanyOwnershipService
    {
        public FRECompanyOwnershipService(IConfiguration config, ILogger<FRECompanyOwnership> logger) : base(config, logger) => DbName = "frecompanyownerships";

    }
}
