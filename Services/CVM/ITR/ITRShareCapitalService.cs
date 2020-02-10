using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.ITR;

namespace Services.CVM.ITR
{
    public class ITRShareCapitalService : BaseService<ITRShareCapital>, IITRShareCapitalService
    {
        public ITRShareCapitalService(IConfiguration config, ILogger<ITRShareCapital> logger) : base(config, logger) => DbName = "itrsharecapitals";

    }
}
