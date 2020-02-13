using System;
using System.Xml.Linq;

namespace Models.Xml.FCA
{
    using Base;

    public enum CompanySecurityType
    {
        // BEING USED!
        OrdinaryShare = 1,
        PreferenceShare = 2,
        Bond = 3
    }
    /// <summary>
    /// Data from CVM XML is not reliable. It comes with wrong Ticker names and even invalid ones, such as '5.5' as a Ticker
    /// What I've actually done was to web scrap b3 website to get updated share ticket information
    /// Example: http://bvmf.bmfbovespa.com.br/cias-listadas/empresas-listadas/ResumoEmpresaPrincipal.aspx?codigoCvm=9954&idioma=pt-br
    /// </summary>
    public class FCACompanySecurity : XmlModel<FCACompanySecurity>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public CompanySecurityType CompanySecurityType { get; set; }

        public FCACompanySecurity() { }

        public override string DocumentRoot() => "ArrayOfValorMobiliario";
        public override string ElementXPath() => "ValorMobiliario";


        public FCACompanySecurity(string n, int t)
        {
            this.Name = n;
            this.CompanySecurityType = (CompanySecurityType)t;
        }

        public override FCACompanySecurity FromElement(XElement e)
        {
            // DO NOT USE THIS CLASS, READ SUMMARY

            //var xmlValue = e.Element("ValorMobiliarioNegociado").Element("DescricaoOpcaoDominio").Value;
            //int? securityType = null;
            //if (xmlValue == "Ações Ordinárias")
            //    securityType = 1;
            //else if (xmlValue == "Ações Preferenciais")
            //    securityType = 2;
            //else if (xmlValue == "Debêntures")
            //    return null;
            //else if (xmlValue == "Debêntures Conversíveis")
            //    return null;
            //else if (xmlValue == "Bônus de Subscrição")
            //    return null;
            //else if (xmlValue == "Units")
            //    return null;
            //else if (xmlValue == "Certificados de Depósito de Valores Mobiliários")
            //    return null;
            //else if (xmlValue == "Certificados de Recebíveis Imobiliários")
            //    return null;
            //else if (xmlValue == "Nota Comercial")
            //    return null;
            //else
            //    throw new NotImplementedException();

            //var name = e.Element("CodigoNegociacao");
            //if (name == null)
            //    return null;

            //return new FCACompanySecurity(
            //    name.Value,
            //    (int)securityType
            //);
            throw new NotImplementedException();
        }

        public override string Extension()
        {
            throw new NotImplementedException();
        }

        public override string Filename()
        {
            throw new NotImplementedException();
        }
    }
}
