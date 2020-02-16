using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyAuditorService : BaseService<FRECompanyAuditor>, IFRECompanyAuditorService
    {
        public FRECompanyAuditorService(IConfiguration config, ILogger<FRECompanyAuditor> logger) : base(config, logger) => DbName = "frecompanyauditors";

    }
}
