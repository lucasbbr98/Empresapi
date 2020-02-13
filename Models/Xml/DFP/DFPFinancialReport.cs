using System;
using System.Xml.Linq;

namespace Models.Xml.DFP
{
    using Base;
    using Constants;
    using ITR;

    public class DFPFinancialReport : XmlModel<DFPFinancialReport>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public int ReportType { get; set; }
        public string AccountNumber { get; set; }
        public string AccountDescription { get; set; }
        public float Value { get; set; }
        public DateTime ReferenceDate { get; set; }
        public bool Consolidated { get; set; }
        public int Year { get; set; }

        public DFPFinancialReport() { }

        public override string Extension() => CVMFileExtension.DFP;
        public override string DocumentRoot() => CVMDocumentRoot.ITRFinancialReports;
        public override string ElementXPath() => CVMElement.ITRFinancialReports;
        public override string Filename() => CVMFile.ITRFinancialReports;

        public DFPFinancialReport(ITRFinancialReport r)
        {
            this.ReportType = r.ReportType;
            this.AccountNumber = r.AccountNumber;
            this.AccountDescription = r.AccountDescription;
            this.Value = r.Value;
            this.Consolidated = r.Consolidated;
        }

        public override DFPFinancialReport FromElement(XElement e)
        {
            var model = new ITRFinancialReport().FromElement(e);
            if (model == null)
                return null;

            return new DFPFinancialReport(model);   
        }

    }
}
