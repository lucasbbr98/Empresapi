using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyDebtService : BaseService<FRECompanyDebt>, IFRECompanyDebtService
    {
        public FRECompanyDebtService(IConfiguration config, ILogger<FRECompanyDebt> logger) : base(config, logger) => DbName = "frecompanydebts";

    }
}
