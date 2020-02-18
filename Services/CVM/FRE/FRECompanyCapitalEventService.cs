using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyCapitalEventService : BaseService<FRECompanyCapitalEvent>, IFRECompanyCapitalEventService
    {
        public FRECompanyCapitalEventService(IConfiguration config, ILogger<FRECompanyCapitalEvent> logger) : base(config, logger) => DbName = "frecompanycapitalevents";

    }
}
