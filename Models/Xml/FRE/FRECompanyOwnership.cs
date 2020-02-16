using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;
    using System.Globalization;

    public class FRECompanyOwnership : XmlModel<FRECompanyOwnership>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Activity { get; set; }
        public int SocietyType { get; set; }
        public string SocietyTypeName { get; set; }
        public int? CvmCode { get; set; }
        public string AcquisitionReason { get; set; }
        public float PercentageOfShares { get; set; }
        public float Value { get; set; }
        public DateTime ValueDate { get; set; }
        public float MarketValue { get; set; }
        public DateTime MarketValueDate { get; set; }

        public FRECompanyOwnership() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyOwnerships;
        public override string ElementXPath() => CVMElement.FRECompanyOwnerships;
        public override string Filename() => CVMFile.FRECompanyOwnerships;

        public FRECompanyOwnership(string c, string n, string t, string co, string st, string ci, string ac, int so, int? cv, string ar, float ps, float v, DateTime vd, float mv, DateTime mvd)
        {
            //TODO FORMAT CNPJ
            this.Cnpj = c;
            this.Name = n;
            this.Type = t;
            this.Country = co;
            this.State = st;
            this.City = ci;
            this.Activity = ac;
            this.SocietyType = so;
            this.CvmCode = cv;
            this.AcquisitionReason = ar;
            this.PercentageOfShares = ps;
            this.Value = v;
            this.ValueDate = vd;
            this.MarketValue = mv;
            this.MarketValueDate = mvd;
            if (this.SocietyType == 0)
                this.SocietyTypeName = "Indefinida";
            else if (this.SocietyType == 1)
                this.SocietyTypeName = "Controlada";
            else if (this.SocietyType == 2)
                this.SocietyTypeName = "Coligada";
            else
                throw new NotImplementedException($"SocietyType: {this.SocietyType.ToString()}");

        }

        public override FRECompanyOwnership FromElement(XElement e)
        {
            try
            {
                var hasCvmRecord = int.Parse(e.Element("PossuiRegistroCVM").Value);
                int? cvmCode = null;
                if (hasCvmRecord == 1)
                {
                    var hasCvmCode = e.Element("CodigoCVM").Value;
                    if (!string.IsNullOrEmpty(hasCvmCode))
                        cvmCode = int.Parse(hasCvmCode);
                }

                var x = e.Element("PessoaSoemParticipante").Element("IdentificacaoPessoa").Value;
                var c = e.Element("PessoaSoemParticipante").Element("NomePessoa").Value;
                var a = e.Element("PessoaSoemParticipante").Element("TipoPessoa").Value;
                var xq = e.Element("Pais").Element("NomePais").Value;
                var xw = e.Element("Uf").Element("NomeEstado").Value;
                var xr = e.Element("Municipio").Element("NomeMunicipio").Value;
                var xs = e.Element("AtividadesDesenvolvidas").Value;
                var xzz = int.Parse(e.Element("CodigoTipoSociedade").Value);
                var xxx = cvmCode;
                var xqq = e.Element("RazaoAquisicaoManutencao").Value;
                var ignore = e.Element("PercentualParticipacaoEmissor").Value;
                var xww = float.Parse(e.Element("PercentualParticipacaoEmissor").Value, CultureInfo.InvariantCulture);
                var xll = float.Parse(e.Element("ValorContabil").Value);
                var xff = DateTime.Parse(e.Element("DataValorContabil").Value);
                var xbb = float.Parse(e.Element("ValorMercado").Value);
                var xp = DateTime.Parse(e.Element("DataValorMercado").Value);

                return new FRECompanyOwnership(
                     e.Element("PessoaSoemParticipante").Element("IdentificacaoPessoa").Value,
                     e.Element("PessoaSoemParticipante").Element("NomePessoa").Value,
                     e.Element("PessoaSoemParticipante").Element("TipoPessoa").Value,
                     e.Element("Pais").Element("NomePais").Value,
                     e.Element("Uf").Element("NomeEstado").Value,
                     e.Element("Municipio").Element("NomeMunicipio").Value,
                     e.Element("AtividadesDesenvolvidas").Value,
                     int.Parse(e.Element("CodigoTipoSociedade").Value),
                     cvmCode,
                     e.Element("RazaoAquisicaoManutencao").Value,
                     float.Parse(e.Element("PercentualParticipacaoEmissor").Value),
                     float.Parse(e.Element("ValorContabil").Value),
                     DateTime.Parse(e.Element("DataValorContabil").Value),
                     float.Parse(e.Element("ValorMercado").Value),
                     DateTime.Parse(e.Element("DataValorMercado").Value)
             );
            }
            catch(Exception er)
            {
                return null;
            }
            
        }

    }
}
