using System;
using System.Xml.Linq;

namespace Models.Xml.ITR
{
    using Base;

    public class ITRShareCapital : XmlModel<ITRShareCapital>
    {
        public int CompanyId { get; set; }
        public int SourceId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public int ShareCapitalNumber { get; set; }
        public float OrdinaryShares { get; set; }
        public float PreferenceShares { get; set; }
        public float OrdinarySharesInTreasury { get; set; }
        public float PreferenceSharesInTreasury { get; set; }

        public ITRShareCapital() { }

        public override string DocumentRoot() => "ArrayOfComposicaoCapitalSocialDemonstracaoFinanceira";
        public override string ElementXPath() => "ComposicaoCapitalSocialDemonstracaoFinanceira";


        public ITRShareCapital(int s, float os, float ps, float ost, float pst)
        {
            this.ShareCapitalNumber = s;
            this.OrdinaryShares = os;
            this.PreferenceShares = ps;
            this.OrdinarySharesInTreasury = ost;
            this.PreferenceSharesInTreasury = pst;
        }

        public override ITRShareCapital FromElement(XElement e)
        {
            return new ITRShareCapital(
                int.Parse(e.Element("NumeroIdentificacaoComposicaoCapitalSocial").Value),
                float.Parse(e.Element("QuantidadeAcaoOrdinariaCapitalIntegralizado").Value),
                float.Parse(e.Element("QuantidadeAcaoPreferencialCapitalIntegralizado").Value),
                float.Parse(e.Element("QuantidadeAcaoOrdinariaTesouraria").Value),
                float.Parse(e.Element("QuantidadeAcaoPreferencialTesouraria").Value)
            );
        }
    }
}
