using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanyFamilyRelationshipService : BaseService<FRECompanyFamilyRelationship>, IFRECompanyFamilyRelationshipService
    {
        public FRECompanyFamilyRelationshipService(IConfiguration config, ILogger<FRECompanyFamilyRelationship> logger) : base(config, logger) => DbName = "frecompanyfamilyrelationships";

    }
}
