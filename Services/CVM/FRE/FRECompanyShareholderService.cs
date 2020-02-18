using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyShareholderService : BaseService<FRECompanyShareholder>, IFRECompanyShareholderService
    {
        public FRECompanyShareholderService(IConfiguration config, ILogger<FRECompanyShareholder> logger) : base(config, logger) => DbName = "frecompanyshareholders";

    }
}
