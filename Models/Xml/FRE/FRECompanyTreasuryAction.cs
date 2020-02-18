using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyTreasuryAction : XmlModel<FRECompanyTreasuryAction>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public int ShareType { get; set; }
        public int SecurityCode { get; set; }
        public long InitialBalance { get; set; }
        public long Acquired { get; set; }
        public float AveragePriceAcquisition { get; set; }
        public long Alienated { get; set; }
        public float AveragePriceAlienation { get; set; }
        public long Cancelled { get; set; }
        public float AveragePriceCancelled { get; set; }
        public long FinalBalance { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }


        public FRECompanyTreasuryAction() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyTreasuryActions;
        public override string ElementXPath() => CVMElement.FRECompanyTreasuryActions;
        public override string Filename() => CVMFile.FRECompanyTreasuryActions;


        public override FRECompanyTreasuryAction FromElement(XElement e)
        {

            var ShareType = int.Parse(e.Element("CodigoEspecieAcao").Value);
            var SecurityCode = int.Parse(e.Element("CodigoValorMobiliario").Value);
            var InitialBalance = long.Parse(e.Element("QuantidadeFisicaSaldoInicial").Value);
            var Acquired = long.Parse(e.Element("QuantidadeFisicaAquisicao").Value);
            var AveragePriceAcquisition = float.Parse(e.Element("ValorPrecoMedioAquisicao").Value);
            var Alienated = long.Parse(e.Element("QuantidadeFisicaAlienacao").Value);
            var AveragePriceAlianation = float.Parse(e.Element("ValorPrecoMedioAlienacao").Value);
            var Cancelled = long.Parse(e.Element("QuantidadeFisicaCancelamento").Value);
            var AveragePriceCancelled = float.Parse(e.Element("ValorPrecoMedioCancelamento").Value);
            var FinalBalance = long.Parse(e.Element("QuantidadeFisicaSaldoFinal").Value);
            var StartDate = DateTime.Parse(e.Element("ExercicioSocial").Element("DataInicioExercicioSocial").Value);
            var EndDate = DateTime.Parse(e.Element("ExercicioSocial").Element("DataFimExercicioSocial").Value);
            var Description = e.Element("ExercicioSocial").Element("ObservacaoDividas").Value;

            return new FRECompanyTreasuryAction 
            { 
                ShareType = int.Parse(e.Element("CodigoEspecieAcao").Value),
                SecurityCode = int.Parse(e.Element("CodigoValorMobiliario").Value),
                InitialBalance = long.Parse(e.Element("QuantidadeFisicaSaldoInicial").Value),
                Acquired = long.Parse(e.Element("QuantidadeFisicaAquisicao").Value),
                AveragePriceAcquisition = float.Parse(e.Element("ValorPrecoMedioAquisicao").Value),
                Alienated = long.Parse(e.Element("QuantidadeFisicaAlienacao").Value),
                AveragePriceAlienation = float.Parse(e.Element("ValorPrecoMedioAlienacao").Value),
                Cancelled = long.Parse(e.Element("QuantidadeFisicaCancelamento").Value),
                AveragePriceCancelled = float.Parse(e.Element("ValorPrecoMedioCancelamento").Value),
                FinalBalance = long.Parse(e.Element("QuantidadeFisicaSaldoFinal").Value),
                StartDate = DateTime.Parse(e.Element("ExercicioSocial").Element("DataInicioExercicioSocial").Value),
                EndDate = DateTime.Parse(e.Element("ExercicioSocial").Element("DataFimExercicioSocial").Value),
                Description = e.Element("ExercicioSocial").Element("ObservacaoDividas").Value,
            };
        }

    }
}
