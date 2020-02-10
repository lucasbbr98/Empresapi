using System;
using System.Xml.Linq;

namespace Models.Xml.FCA
{
    using Base;

    public enum CompanySecurityType
    {
        OrdinaryShare = 1,
        PreferenceShare = 2,
        Bond = 3
    }

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
            var xmlValue = e.Element("ValorMobiliarioNegociado").Element("DescricaoOpcaoDominio").Value;
            int? securityType = null;
            if (xmlValue == "Ações Ordinárias")
                securityType = 1;
            else if (xmlValue == "Ações Preferenciais")
                securityType = 2;
            else if (xmlValue == "Debêntures")
                return null;
            else if (xmlValue == "Debêntures Conversíveis")
                return null;
            else if (xmlValue == "Bônus de Subscrição")
                return null;
            else if (xmlValue == "Units")
                return null;
            else if (xmlValue == "Certificados de Depósito de Valores Mobiliários")
                return null;
            else if (xmlValue == "Certificados de Recebíveis Imobiliários")
                return null;
            else if (xmlValue == "Nota Comercial")
                return null;
            else
                throw new NotImplementedException();

            var name = e.Element("CodigoNegociacao");
            if (name == null)
                return null;

            return new FCACompanySecurity(
                name.Value,
                (int)securityType
            );
        }
    }
}
