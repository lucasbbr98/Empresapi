using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyDebt : XmlModel<FRECompanyDebt>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public string Type { get; set; }
        public string Warranty { get; set; }
        public float UpToYear { get; set; }
        public float OneToThreeYears { get; set; }
        public float ThreeToFiveYears { get; set; }
        public float FiveYearsOrMore { get; set; }


        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyDebts;
        public override string ElementXPath() => CVMElement.FRECompanyDebts;
        public override string Filename() => CVMFile.FRECompanyDebts;

        public FRECompanyDebt() { }

        public FRECompanyDebt(string t, string w, float u, float o, float tr, float f) 
        {
            this.Type = t;
            this.Warranty = w;
            this.UpToYear = u;
            this.OneToThreeYears = o;
            this.ThreeToFiveYears = tr;
            this.FiveYearsOrMore = f;
        }

        public override FRECompanyDebt FromElement(XElement e)
        {
            var type = string.Empty;
            var elType = e.Element("CodigoTipoDivida")?.Element("DescricaoOpcaoDominio");
            if (elType != null)
                type = elType.Value;

            var warranty = string.Empty;
            var elWarranty = e.Element("CodigoTipoGarantia")?.Element("DescricaoOpcaoDominio");
            if (elWarranty != null)
                warranty = elWarranty.Value;

            return new FRECompanyDebt(
                type,
                warranty,
                float.Parse(e.Element("ValorDividaInferiorAUmAno").Value),
                float.Parse(e.Element("ValorDividaUmATresAnos").Value),
                float.Parse(e.Element("ValorDividaTresACincoAnos").Value),
                float.Parse(e.Element("ValorDividaSuperiorACincoAnos").Value)
            );
        }

    }
}
