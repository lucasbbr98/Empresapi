using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyRelatedTransactionService : BaseService<FRECompanyRelatedTransaction>, IFRECompanyRelatedTransactionService
    {
        public FRECompanyRelatedTransactionService(IConfiguration config, ILogger<FRECompanyRelatedTransaction> logger) : base(config, logger) => DbName = "frecompanyrelatedtransactions";

    }
}
