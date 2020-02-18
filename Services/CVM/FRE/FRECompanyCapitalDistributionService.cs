using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyCapitalDistributionService : BaseService<FRECompanyCapitalDistribution>, IFRECompanyCapitalDistributionService
    {
        public FRECompanyCapitalDistributionService(IConfiguration config, ILogger<FRECompanyCapitalDistribution> logger) : base(config, logger) => DbName = "frecompanycapitaldistributions";

    }
}
