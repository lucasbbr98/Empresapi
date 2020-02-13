using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.DFP;

namespace Services.CVM.DFP
{
    public class DFPFinancialReportService : BaseService<DFPFinancialReport>, IDFPFinancialReportService
    {
        public DFPFinancialReportService(IConfiguration config, ILogger<DFPFinancialReport> logger) : base(config, logger) => DbName = "dfpfinancialreports";

    }
}
