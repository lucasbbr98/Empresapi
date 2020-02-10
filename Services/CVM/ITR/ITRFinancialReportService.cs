using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.ITR;

namespace Services.CVM.ITR
{
    public class ITRFinancialReportService : BaseService<ITRFinancialReport>, IITRFinancialReportService
    {
        public ITRFinancialReportService(IConfiguration config, ILogger<ITRFinancialReport> logger) : base(config, logger) => DbName = "itrfinancialreports";

    }
}
