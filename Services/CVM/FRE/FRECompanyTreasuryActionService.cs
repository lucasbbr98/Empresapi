using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyTreasuryActionService : BaseService<FRECompanyTreasuryAction>, IFRECompanyTreasuryActionService
    {
        public FRECompanyTreasuryActionService(IConfiguration config, ILogger<FRECompanyTreasuryAction> logger) : base(config, logger) => DbName = "frecompanytreasuryactions";

    }
}
