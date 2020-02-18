using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyCapitalReduction : XmlModel<FRECompanyCapitalReduction>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public DateTime DateOfApproval { get; set; }
        public DateTime DateOfReduction { get; set; }
        public float TotalReductionValue { get; set; }
        public long OrdinarySharesReduction { get; set; }
        public long PreferenceSharesReduction { get; set; }
        public float PercentagePreviousCapital { get; set; }
        public float ValuePerShare { get; set; }
        public string Description { get; set; }
        public string TermDescription { get; set; }

        public FRECompanyCapitalReduction() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyCapitalReductions;
        public override string ElementXPath() => CVMElement.FRECompanyCapitalReductions;
        public override string Filename() => CVMFile.FRECompanyCapitalReductions;


        public override FRECompanyCapitalReduction FromElement(XElement e)
        {
            var DateOfApproval = DateTime.Parse(e.Element("DataDeliberacao").Value);
            var DateOfReduction = DateTime.Parse(e.Element("DataReducaoCapital").Value);
            var TotalReductionValue = float.Parse(e.Element("ValorTotalReducaoCapital").Value);
            var OrdinarySharesReduction = long.Parse(e.Element("QuantidadeAcaoOrdinaria").Value);
            var PreferenceSharesReduction = long.Parse(e.Element("QuantidadeAcaoPreferencial").Value);
            var PercentagePreviousCapital = float.Parse(e.Element("PercentualSubscricaoRelacaoCapitalAnterior").Value);
            var ValuePerShare = float.Parse(e.Element("ValorRestituidoPorAcao").Value);
            var Description = e.Element("DescricaoFormaRestituicao").Value;
            var TermDescription = e.Element("RazaoParaReducao").Value;

            return new FRECompanyCapitalReduction 
            {
                DateOfApproval = DateTime.Parse(e.Element("DataDeliberacao").Value),
                DateOfReduction = DateTime.Parse(e.Element("DataReducaoCapital").Value),
                TotalReductionValue = float.Parse(e.Element("ValorTotalReducaoCapital").Value),
                OrdinarySharesReduction = long.Parse(e.Element("QuantidadeAcaoOrdinaria").Value),
                PreferenceSharesReduction = long.Parse(e.Element("QuantidadeAcaoPreferencial").Value),
                PercentagePreviousCapital = float.Parse(e.Element("PercentualSubscricaoRelacaoCapitalAnterior").Value),
                ValuePerShare = float.Parse(e.Element("ValorRestituidoPorAcao").Value),
                Description = e.Element("DescricaoFormaRestituicao").Value,
                TermDescription = e.Element("RazaoParaReducao").Value
            };
        }

    }
}
