using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyCapitalDistribution : XmlModel<FRECompanyCapitalDistribution>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public long IndividualShareholders { get; set; }
        public long LegalShareholders { get; set; }
        public long InstitutionalShareholders { get; set; }
        public DateTime LastMeeting { get; set; }
        public long OrdinaryShares { get; set; }
        public float OrdinarySharesPercentage { get; set; }
        public long PreferenceShares { get; set; }
        public float PreferenceSharesPercentage { get; set; }


        public FRECompanyCapitalDistribution() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyCapitalDistributions;
        public override string ElementXPath() => CVMElement.FRECompanyCapitalDistributions;
        public override string Filename() => CVMFile.FRECompanyCapitalDistributions;


        public override FRECompanyCapitalDistribution FromElement(XElement e)
        {
            return new FRECompanyCapitalDistribution 
            {
                IndividualShareholders = long.Parse(e.Element("QuantidadeAcionistasPessoaFisica").Value),
                LegalShareholders = long.Parse(e.Element("QuantidadeAcionistasPessoaJuridica").Value),
                InstitutionalShareholders = long.Parse(e.Element("QuantidadeInvestidoresInstitucionais").Value),
                LastMeeting = DateTime.Parse(e.Element("UltimaAssembleia").Value),
                OrdinaryShares = long.Parse(e.Element("QuantidadeAcoesOrdinarias").Value),
                OrdinarySharesPercentage = float.Parse(e.Element("PercentualAcoesOrdinarias").Value),
                PreferenceShares = long.Parse(e.Element("QuantidadeAcoesPreferenciais").Value),
                PreferenceSharesPercentage = float.Parse(e.Element("PercentualAcoesPreferenciais").Value),
            };
        }

    }
}
