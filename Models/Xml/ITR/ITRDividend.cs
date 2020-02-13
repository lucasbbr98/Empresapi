using System;
using System.Xml.Linq;


namespace Models.Xml.ITR
{
    using Constants;
    using Base;

    public class ITRDividend : XmlModel<ITRDividend>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public int PaymentIdNumber { get; set; }
        public string EventAbbreviation { get; set; }
        public string Event { get; set; }
        public DateTime Approval { get; set; }
        public string ShareTypeAbbreviation { get; set; }
        public string ShareType { get; set; }
        public string DividendType { get; set; }
        public DateTime PaymentDate { get; set; }
        public float DividendPerShare { get; set; }

        public ITRDividend() { }

        public override string Extension() => CVMFileExtension.ITR;
        public override string DocumentRoot() => CVMDocumentRoot.ITRDividends;
        public override string ElementXPath() => CVMElement.ITRDividends;
        public override string Filename() => CVMFile.ITRDividends;

        public ITRDividend(int p, string ea, string e, DateTime a, string sta, string st, string dt, DateTime pd, float dps)
        {
            this.PaymentIdNumber = p;
            this.EventAbbreviation = ea;
            this.Event = e;
            this.Approval = a;
            this.ShareTypeAbbreviation = sta;
            this.ShareType = st;
            this.DividendType = dt;
            this.PaymentDate = pd;
            this.DividendPerShare = dps;
        }

        public override ITRDividend FromElement(XElement e)
        {
            return new ITRDividend(
                int.Parse(e.Element("NumeroIdentificacaoPagamento").Value),
                e.Element("EventoOrigemProvento").Element("SiglaOpcaoDominio").Value,
                    e.Element("EventoOrigemProvento").Element("DescricaoOpcaoDominio").Value,
                DateTime.Parse(e.Element("DataAprovacaoProvento").Value),
                e.Element("CodigoEspecieAcao").Element("SiglaOpcaoDominio").Value,
                e.Element("CodigoEspecieAcao").Element("DescricaoOpcaoDominio").Value,
                e.Element("TipoProvento").Element("DescricaoOpcaoDominio").Value,
                DateTime.Parse(e.Element("DataInicioPagamento").Value),
                float.Parse(e.Element("ValorProventoPorAcao").Value)
            );       
        }
    }
}
