using System;
using System.Xml.Linq;

namespace Models.Xml.ITR
{
    using Base;

    public class ITRFinancialReport : XmlModel<ITRFinancialReport>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public int ReportType { get; set; }
        public string AccountNumber { get; set; }
        public string AccountDescription { get; set; }
        public float Value { get; set; }
        public DateTime ReferenceDate { get; set; }
        public bool Consolidated { get; set; }

        public ITRFinancialReport() { }

        public override string DocumentRoot() => "ArrayOfInfoFinaDFin";
        public override string ElementXPath() => "InfoFinaDFin";


        public ITRFinancialReport(int r, string an, string ad, float v, bool co)
        {
            this.ReportType = r;
            this.AccountNumber = an;
            this.AccountDescription = ad;
            this.Value = v;
            this.Consolidated = co;
        }

        public override ITRFinancialReport FromElement(XElement e)
        {
            bool? consolidated = null;
            var infoType = int.Parse(e.Element("PlanoConta").Element("VersaoPlanoConta").Element("CodigoTipoInformacaoFinanceira").Value);

            if (infoType == 1)
                consolidated = false;
            else if (infoType == 2)
                consolidated = true;

            if (consolidated == null)
                throw new ArgumentNullException();

            var reportType = int.Parse(e.Element("PlanoConta").Element("NumeroConta").Value.Substring(0 ,1).Replace(".", ""));
            float? value = null;
            if (reportType == 1)
                value = float.Parse(e.Element("ValorConta2").Value);
            else if (reportType == 2)
                value = float.Parse(e.Element("ValorConta2").Value);
            else if (reportType == 3)
                value = float.Parse(e.Element("ValorConta2").Value);
            else if (reportType == 4)
                value = float.Parse(e.Element("ValorConta2").Value);
            else if (reportType == 5)
                return null;
            else if (reportType == 6)
                value = float.Parse(e.Element("ValorConta4").Value);
            else if (reportType == 7)
                value = float.Parse(e.Element("ValorConta4").Value);

            if (value == null)
                throw new ArgumentNullException();

            return new ITRFinancialReport(
                reportType,
                e.Element("PlanoConta").Element("NumeroConta").Value,
                e.Element("DescricaoConta1").Value,
                (float)value,
                (bool)consolidated
            );
        }
    }
}
