using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyFixedAsset : XmlModel<FRECompanyFixedAsset>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }

        public FRECompanyFixedAsset() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyFixedAssets;
        public override string ElementXPath() => CVMElement.FRECompanyFixedAssets;
        public override string Filename() => CVMFile.FRECompanyFixedAssets;

        public FRECompanyFixedAsset(string n, string c, string s, int t)
        {
            this.Name = n;
            this.Country = c;
            this.State = s;
            this.Type = t;
            if (this.Type == 0)
                this.TypeName = "Nenhuma";
            else if (this.Type == 1)
                this.TypeName = "Própria";
            else if (this.Type == 2)
                this.TypeName = "Alugada";
            else if (this.Type == 3)
                this.TypeName = "Arrendada";
            else
                throw new NotImplementedException($"Not Implemented TypeName for {this.Type}");
        }

        public override FRECompanyFixedAsset FromElement(XElement e)
        {
            var elCountry = e.Element("PaisBemAtivo");
            string country = string.Empty;
            if (elCountry != null)
                country = elCountry.Element("NomePais").Value;

            var elState = e.Element("EstadoBemAtivo");
            string state = string.Empty;
            if (elState != null)
                state = elState.Element("NomeEstado").Value;

            return new FRECompanyFixedAsset(
             e.Element("DescricaoBemAtivo").Value,
             country,
             state,
             int.Parse(e.Element("CodigoTipoPropriedade").Value)
         );
        }

    }
}
