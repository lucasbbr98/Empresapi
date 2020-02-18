using Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Xml.FRE;

namespace Services.CVM.FRE
{
    public class FRECompanySubordinateRelationshipService : BaseService<FRECompanySubordinateRelationship>, IFRECompanySubordinateRelationshipService
    {
        public FRECompanySubordinateRelationshipService(IConfiguration config, ILogger<FRECompanySubordinateRelationship> logger) : base(config, logger) => DbName = "frecompanysubordinaterelationships";

    }
}
