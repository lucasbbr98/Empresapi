using System;
using System.Xml.Linq;

namespace Models.Xml.FCA
{
    using Constants;
    using Base;

    public class FCACompanyIssuer : XmlModel<FCACompanyIssuer>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Name { get; set; }
        public string IssuerType { get; set; }
        public string Cnpj { get; set; }
        public int DataVersion { get; set; }

        public FCACompanyIssuer() { }

        public override string Extension() => CVMFileExtension.FCA;
        public override string DocumentRoot() => CVMDocumentRoot.FCACompanyIssuer;
        public override string ElementXPath() => CVMElement.FCACompanyIssuers;
        public override string Filename() => CVMFile.FCACompanyIssuers;

        public FCACompanyIssuer(DateTime s, string n, string i, string c, int d)
        {
            this.StartDate = s;
            this.Name = n.ToUpper();
            this.IssuerType = i;
            this.DataVersion = d;
            this.Cnpj = c;
        }

        public override FCACompanyIssuer FromElement(XElement e)
        {
            var el = e.Element("VersaoDocumentoCadastrado");
            string version = "1";
            if (el != null)
                version = el.Value;

            return new FCACompanyIssuer(
                DateTime.Parse(e.Element("DataInicioPrestacaoServicoEscrituracaoAcao").Value),
                e.Element("Pessoa").Element("NomePessoa").Value,
                e.Element("Pessoa").Element("TipoPessoa").Value,
                e.Element("Pessoa").Element("IdentificacaoPessoa").Value,
                int.Parse(version)
            );
        }
    }
}
