using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyIntangible : XmlModel<FRECompanyIntangible>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public int InternalId { get; set; }
        public int TypeCode { get; set; }
        public string Asset { get; set; }
        public string ExpiresOn { get; set; }
        public string LossPossibility { get; set; }
        public string LossConsequence { get; set; }

        public FRECompanyIntangible() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyIntangibles;
        public override string ElementXPath() => CVMElement.FRECompanyIntangibles;
        public override string Filename() => CVMFile.FRECompanyIntangibles;

        public FRECompanyIntangible(int _id, int tc, string a, string e, string lp, string lc)
        {
            this.InternalId = _id;
            this.TypeCode = tc;
            this.Asset = a;
            this.ExpiresOn = e;
            this.LossPossibility = lp;
            this.LossConsequence = lc;
        }

        public override FRECompanyIntangible FromElement(XElement e)
        {
            return new FRECompanyIntangible(
             int.Parse(e.Element("Id").Value),
             int.Parse(e.Element("CodigoTipo").Value),
             e.Element("Ativo").Value,
             e.Element("Duracao").Value,
             e.Element("EventosPodemCausarPerdas").Value,
             e.Element("ConsequenciasPerdas").Value
         );
        }

    }
}
