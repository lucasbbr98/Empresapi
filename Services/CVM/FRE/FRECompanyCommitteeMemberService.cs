using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyCommitteeMemberService : BaseService<FRECompanyCommitteeMember>, IFRECompanyCommitteeMemberService
    {
        public FRECompanyCommitteeMemberService(IConfiguration config, ILogger<FRECompanyCommitteeMember> logger) : base(config, logger) => DbName = "frecompanycommitteemembers";

    }
}
