using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyShareBuybackService : BaseService<FRECompanyShareBuyback>, IFRECompanyShareBuybackService
    {
        public FRECompanyShareBuybackService(IConfiguration config, ILogger<FRECompanyShareBuyback> logger) : base(config, logger) => DbName = "frecompanysharebuybacks";

    }
}
