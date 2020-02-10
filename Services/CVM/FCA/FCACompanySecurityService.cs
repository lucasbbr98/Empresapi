using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FCA;

namespace Services.CVM.FCA
{
    public class FCACompanySecurityService : BaseService<FCACompanySecurity>, IFCACompanySecurityService
    {
        public FCACompanySecurityService(IConfiguration config, ILogger<FCACompanySecurity> logger) : base(config, logger) => DbName = "companysecurities";

    }
}
