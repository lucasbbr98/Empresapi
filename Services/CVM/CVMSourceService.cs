using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Models;
using Microsoft.Extensions.Logging;

namespace Services.CVM
{
    public class CVMSourceService : BaseService<CVMSource>, ICVMSourceService
    {
        public CVMSourceService(IConfiguration config, ILogger<CVMSource> logger) : base(config, logger) => DbName = "cvmsources";

    }
}
