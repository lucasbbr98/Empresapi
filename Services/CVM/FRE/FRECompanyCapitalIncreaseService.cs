using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyCapitalIncreaseService : BaseService<FRECompanyCapitalIncrease>, IFRECompanyCapitalIncreaseService
    {
        public FRECompanyCapitalIncreaseService(IConfiguration config, ILogger<FRECompanyCapitalIncrease> logger) : base(config, logger) => DbName = "frecompanycapitalincreases";

    }
}
