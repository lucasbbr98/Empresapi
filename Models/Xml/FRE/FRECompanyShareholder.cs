using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyShareholder : XmlModel<FRECompanyShareholder>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public int InternalId { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string PersonType { get; set; }
        public string Nationality { get; set; }
        public string State { get; set; }
        public float OrdinaryShares { get; set; }
        public float OrdinarySharesPercentage { get; set; }
        public float PreferenceShares { get; set; }
        public float PreferenceSharesPercentage { get; set; }
        public int ControllerCode { get; set; }
        public int StockholderCode { get; set; }


        public FRECompanyShareholder() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyShareholders;
        public override string ElementXPath() => CVMElement.FRECompanyShareholders;
        public override string Filename() => CVMFile.FRECompanyShareholders;

        public FRECompanyShareholder(int _i, string c, string n, string t, string pt,string na, string st, float o, float os, float p, float ps, int cc, int sc)
        {
            this.InternalId = _i;
            this.Cnpj = c;
            this.Name = n;
            this.Type = t;
            this.PersonType = pt;
            this.Nationality = na;
            this.State = st;
            this.OrdinaryShares = o;
            this.OrdinarySharesPercentage = os;
            this.PreferenceShares = p;
            this.PreferenceSharesPercentage = ps;
            this.ControllerCode = cc;
            this.StockholderCode = sc;
        }


        public override FRECompanyShareholder FromElement(XElement e)
        {
            var elType = e.Element("TipoRegistro");
            if (elType == null)
                throw new NotImplementedException("Could not find TipoRegistro Element");

            string type = elType.Value;
            var cnpj = string.Empty;
            var nationality = string.Empty;
            var state = string.Empty;
            string name;
            string personType;
            if (type.ToLower() == "Outros".ToLower())
            {
                name = "Outros";
                personType = "Nenhuma";
            }
            else
            {
                name = e.Element("Pessoa").Element("NomePessoa").Value;
                cnpj = e.Element("Pessoa").Element("IdentificacaoPessoa").Value;
                nationality = e.Element("Nacionalidade").Value;
                var elState = e.Element("Estado");
                if(elState != null)
                    state = e.Element("Estado").Element("NomeEstado").Value;

                personType = e.Element("Pessoa").Element("TipoPessoa").Value;
            }
            return new FRECompanyShareholder(
                int.Parse(e.Element("NumeroIdentificacaoAcionista").Value),
                cnpj,
                name,
                type,
                personType,
                nationality,
                state,
                float.Parse(e.Element("QuantidadeAcoesOrdinarias").Value),
                float.Parse(e.Element("PercentualAcoesOrdinarias").Value),
                float.Parse(e.Element("QuantidadeAcoesPreferenciais").Value),
                float.Parse(e.Element("PercentualAcoesPreferenciais").Value),
                int.Parse(e.Element("AcionistaControlador").Value),
                int.Parse(e.Element("ParticipanteAcionista").Value)
            );
        }

    }
}
