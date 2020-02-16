using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyIntangibleService : BaseService<FRECompanyIntangible>, IFRECompanyIntangibleService
    {
        public FRECompanyIntangibleService(IConfiguration config, ILogger<FRECompanyIntangible> logger) : base(config, logger) => DbName = "frecompanyintangibles";

    }
}
