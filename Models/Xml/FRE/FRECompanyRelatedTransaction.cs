using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    //https://github.com/msperlin/GetDFPData/blob/master/R/gdfpd_xml_fcts.R HELPERS

    public class FRECompanyRelatedTransaction : XmlModel<FRECompanyRelatedTransaction>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public string RelatedName { get; set; }
        public string RelatedType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public string Balance { get; set; }
        public string InterestDescription { get; set; }
        public string Warranty { get; set; }
        public bool IsLoanOrDebt { get; set; }
        public float InterestRate { get; set; }
        public string CompanyRole { get; set; }
        public string Rescision { get; set; }

        public FRECompanyRelatedTransaction() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyRelatedTranscations;
        public override string ElementXPath() => CVMElement.FRECompanyRelatedTranscations;
        public override string Filename() => CVMFile.FRECompanyRelatedTranscations;


        public override FRECompanyRelatedTransaction FromElement(XElement e)
        {

            var loanOrDebt = true;
            var ldCode = e.Element("IndTnsaEDvda").Value;
            if (ldCode.ToLower() == "false")
                loanOrDebt = false;

            var val = e.Element("ValMontEnvldoNeg").Value.Trim().Replace("R", "").Replace("$", "");
            if (string.IsNullOrEmpty(val) || val == "0" || val == "0.0" || val == "0,0")
                val = "0.00";
            if(val.Substring(val.Length - 3) == ".00")
            {
                val = val.Substring(0, val.Length - 1) + "hck";
                val = val.Replace(".", "").Replace(",", "").Replace("hck", ".00");
            }
            else if (val.Contains(","))
            {
                val = val.Replace(".", "").Replace(",", ".");
            }          


            var ir = e.Element("FatTaxaJuro").Value.Trim().Replace("R", "").Replace("$", "");



            return new FRECompanyRelatedTransaction {
                RelatedName = e.Element("NomePateRelc").Value,
                RelatedType = e.Element("DescRelcPateRelcEmss").Value,
                TransactionDate = DateTime.Parse(e.Element("DataTnsa").Value),
                Description = e.Element("DescObjtCotr").Value,
                Value = float.Parse(val),
                Balance = e.Element("ValSaldExis").Value,
                InterestDescription = e.Element("ValMontItresePateRelc").Value,
                Warranty = e.Element("DescGarnSeguRelc").Value,
                Rescision = e.Element("DescCondResc").Value,
                IsLoanOrDebt = loanOrDebt,
                InterestRate = float.Parse(ir),
                CompanyRole = e.Element("DescRelcPateRelcEmss").Value,
            };
        }

    }
}
