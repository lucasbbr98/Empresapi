using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyFixedAssetService : BaseService<FRECompanyFixedAsset>, IFRECompanyFixedAssetService
    {
        public FRECompanyFixedAssetService(IConfiguration config, ILogger<FRECompanyFixedAsset> logger) : base(config, logger) => DbName = "frecompanyfixedassets";

    }
}
