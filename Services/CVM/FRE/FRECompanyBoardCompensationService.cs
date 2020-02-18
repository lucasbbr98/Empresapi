using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyBoardCompensationService : BaseService<FRECompanyBoardCompensation>, IFRECompanyBoardCompensationService
    {
        public FRECompanyBoardCompensationService(IConfiguration config, ILogger<FRECompanyBoardCompensation> logger) : base(config, logger) => DbName = "frecompanyboardcompensations";

    }
}
