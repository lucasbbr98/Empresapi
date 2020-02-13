using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FCA;

namespace Services.CVM.FCA
{
    public class FCACompanyIssuerService : BaseService<FCACompanyIssuer>, IFCACompanyIssuerService
    {
        public FCACompanyIssuerService(IConfiguration config, ILogger<FCACompanyIssuer> logger) : base(config, logger) => DbName = "fcacompanyissuers";

    }
}
