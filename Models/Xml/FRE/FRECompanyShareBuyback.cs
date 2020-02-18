using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;
    using System.Collections.Generic;

    public class FRECompanyShareBuyback : XmlModel<FRECompanyShareBuyback>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime ReferenceDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Allowance { get; set; }
        public string Description { get; set; }
        public int SecurityType { get; set; }
        public string ShareTypeDescription { get; set; }
        public long QuantityAcquired { get; set; }
        public float WeightedAveragePrice { get; set; }
        public int UnitScale { get; set; }
        public string UnitScaleDescription { get; set; }
        public float PercentageOutstandingShares { get; set; }
        public long QuantityForecastedShares { get; set; }
        public float PercentageForecastedShares { get; set; }


        public FRECompanyShareBuyback() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyShareBuyback;
        public override string ElementXPath() => CVMElement.FRECompanyShareBuyback;
        public override string Filename() => CVMFile.FRECompanyShareBuyback;
        public override bool WillReturnList() => true;


        public override List<FRECompanyShareBuyback> ListFromElement(XElement e)
        {
            List<FRECompanyShareBuyback> list = new List<FRECompanyShareBuyback>();

            var approval = DateTime.Parse(e.Element("DataDeliberacao").Value);
            var start = DateTime.Parse(e.Element("DataInicialPeriodoRecompraAprovado").Value);
            var end = DateTime.Parse(e.Element("DataFinalPeriodoRecompraAprovado").Value);
            var allowance = float.Parse(e.Element("ReservaLucrosParaRecompraAprovado").Value);
            var desc = e.Element("CaracteristicasImportantesAprovado").Value;

            var listOfEl = e.Element("HistoricosPlanosRecompraClasseEspecieAcao").Elements("HistoricoPlanoRecompraClasseEspecieAcao");
            foreach(var el in listOfEl)
            {
                var shareType = int.Parse(el.Element("CodigoEspecieAcaoAdquirida").Value);
                var shareTypeDesc = el.Element("DescricaoEspecieAcaoAdquirida").Value;
                var quantityAcquisition = long.Parse(el.Element("QuantidadeAcoesAdquirida").Value);
                var avg = float.Parse(el.Element("PrecoMedioPonderadoAdquirida").Value);
                var unit = int.Parse(el.Element("CodigoEscalaCotacao").Value);
                var unitDesc = el.Element("DescricaoEscalaCotacao").Value;
                var percOutstanding = float.Parse(el.Element("PercentualAcoesCirculacaoAdquirida").Value);
                var qtdForecasted = long.Parse(el.Element("QuantidadeAcoesPrevista").Value);
                var percForecasted = float.Parse(el.Element("PercentualAcoesCirculacaoPrevista").Value);

                list.Add(new FRECompanyShareBuyback { 
                    ApprovalDate = approval,
                    StartDate = start,
                    EndDate = end,
                    Allowance = allowance,
                    Description = desc,
                    SecurityType = shareType,
                    ShareTypeDescription = shareTypeDesc,
                    QuantityAcquired = quantityAcquisition,
                    WeightedAveragePrice = avg,
                    UnitScale = unit,
                    UnitScaleDescription = unitDesc,
                    PercentageOutstandingShares = percOutstanding,
                    QuantityForecastedShares = qtdForecasted,
                    PercentageForecastedShares = percForecasted
                });;
            }


            return list;
        }

        public override FRECompanyShareBuyback FromElement(XElement e)
        {
            throw new NotImplementedException("This should not run in FRECompanyShareBuyback");
        }

    }
}
