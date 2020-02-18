using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyCapitalReductionService : BaseService<FRECompanyCapitalReduction>, IFRECompanyCapitalReductionService
    {
        public FRECompanyCapitalReductionService(IConfiguration config, ILogger<FRECompanyCapitalReduction> logger) : base(config, logger) => DbName = "frecompanycapitalreductions";

    }
}
