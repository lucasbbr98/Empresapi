using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyAuditor : XmlModel<FRECompanyAuditor>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }

        public string ServiceDescription { get; set; }
        public DateTime TermStart { get; set; }
        public DateTime? TermEnd { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string CompanyType { get; set; }
        public string Discordance { get; set; }
        public string Substitution { get; set; }
        public string Fees { get; set; }



        public FRECompanyAuditor() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyAuditors;
        public override string ElementXPath() => CVMElement.FRECompanyAuditors;
        public override string Filename() => CVMFile.FRECompanyAuditors;

        public FRECompanyAuditor(string sd, DateTime ts, DateTime? te, string n, string c, string ct, string d, string s, string f)
        {
            this.ServiceDescription = sd;
            this.TermStart = ts;
            this.TermEnd = te;
            this.Name = n;
            this.Cnpj = c;
            this.CompanyType = ct;
            this.Discordance = d;
            this.Substitution = s;
            this.Fees = f;
        }

        public override FRECompanyAuditor FromElement(XElement e)
        {
            var razao = string.Empty;
            var elRazao = e.Element("RazaoApresentadaAuditorDiscordancia");
            if (elRazao != null)
                razao = elRazao.Value;

            var jus = string.Empty;
            var elSub = e.Element("JustificativaSubstituicao");
            if (elSub != null)
                jus = elSub.Value;

            var hon = string.Empty;
            var elHon = e.Element("HonorariosServicosPresta");
            if (elHon != null)
                hon = elHon.Value;

            return new FRECompanyAuditor(
             e.Element("DescricaoServicoContratado").Value,
             DateTime.Parse(e.Element("DataInicioContratacaoAuditorServico").Value),
             DateTime.Parse(e.Element("DataFimContratacaoAuditorServico").Value),
             e.Element("PessoaAuditor").Element("NomePessoa").Value,
             e.Element("PessoaAuditor").Element("IdentificacaoPessoa").Value,
             e.Element("PessoaAuditor").Element("TipoPessoa").Value,
             razao,
             jus,
             hon
         );
        }

    }
}
