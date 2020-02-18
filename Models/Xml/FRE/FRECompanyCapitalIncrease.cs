using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyCapitalIncrease : XmlModel<FRECompanyCapitalIncrease>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }

        public DateTime ApprovalDate { get; set; }
        public string ApprovalBoard { get; set; }
        public DateTime IssueDate { get; set; }
        public float TotalIssueValue { get; set; }
        public int SubscriptionCode { get; set; }
        public string SubscriptionType { get; set; }
        public long OrdinaryShares { get; set; }
        public long PreferenceShares { get; set; }
        public float SubscriptionPercentageOfLastCapital { get; set; }
        public int ShareScale { get; set; }
        public float IssuePrice { get; set; }
        public string IssuePriceDescription { get; set; }
        public string PaymentDescription { get; set; }


        public FRECompanyCapitalIncrease() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyCapitalIncreases;
        public override string ElementXPath() => CVMElement.FRECompanyCapitalIncreases;
        public override string Filename() => CVMFile.FRECompanyCapitalIncreases;


        public override FRECompanyCapitalIncrease FromElement(XElement e)
        {

            var ApprovalDate = DateTime.Parse(e.Element("DataDeliberacao").Value);
            var ApprovalBoard = e.Element("NomeOrgaoDeliberacaoAcrescimo").Value;
            var IssueDate = DateTime.Parse(e.Element("DataEmissao").Value);
            var TotalIssueValue = float.Parse(e.Element("ValorTotalEmissao").Value);
            var SubscriptionCode = int.Parse(e.Element("CodigoTipoSubscricao").Value);
            var SubscriptionType = e.Element("DetalheDominio").Element("DescricaoOpcaoDominio").Value;
            var OrdinaryShares = long.Parse(e.Element("QuantidadeAcaoOrdinaria").Value);
            var PreferenceShares = long.Parse(e.Element("QuantidadeAcaoPreferencial").Value);
            var SubscriptionPercentageOfLastCapital = float.Parse(e.Element("PercentualSubscricaoRelacaoCapitalAnterior").Value);
            var ShareScale = int.Parse(e.Element("CodigoEscalaCotacao").Value);
            var IssuePrice = float.Parse(e.Element("ValorPrecoEmissao").Value);
            var IssuePriceDescription = e.Element("DescricaoCriterioDefinicaoPrecoEmissao").Value;
            var PaymentDescription = e.Element("FormaIntegralizacao").Value;

            return new FRECompanyCapitalIncrease 
            {
                ApprovalDate = DateTime.Parse(e.Element("DataDeliberacao").Value),
                ApprovalBoard = e.Element("NomeOrgaoDeliberacaoAcrescimo").Value,
                IssueDate = DateTime.Parse(e.Element("DataEmissao").Value),
                TotalIssueValue = float.Parse(e.Element("ValorTotalEmissao").Value),
                SubscriptionCode = int.Parse(e.Element("CodigoTipoSubscricao").Value),
                SubscriptionType = e.Element("DetalheDominio").Element("DescricaoOpcaoDominio").Value,
                OrdinaryShares = long.Parse(e.Element("QuantidadeAcaoOrdinaria").Value),
                PreferenceShares = long.Parse(e.Element("QuantidadeAcaoPreferencial").Value),
                SubscriptionPercentageOfLastCapital = float.Parse(e.Element("PercentualSubscricaoRelacaoCapitalAnterior").Value),
                ShareScale = int.Parse(e.Element("CodigoEscalaCotacao").Value),
                IssuePrice = float.Parse(e.Element("ValorPrecoEmissao").Value),
                IssuePriceDescription = e.Element("DescricaoCriterioDefinicaoPrecoEmissao").Value,
                PaymentDescription = e.Element("FormaIntegralizacao").Value
            };
        }

    }
}
