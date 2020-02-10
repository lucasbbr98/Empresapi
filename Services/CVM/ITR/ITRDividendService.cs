using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.ITR;

namespace Services.CVM.ITR
{
    public class ITRDividendService : BaseService<ITRDividend>, IITRDividendService
    {
        public ITRDividendService(IConfiguration config, ILogger<ITRDividend> logger) : base(config, logger) => DbName = "itrdividends";

    }
}
