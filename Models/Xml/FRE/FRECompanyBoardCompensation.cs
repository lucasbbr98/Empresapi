using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyBoardCompensation : XmlModel<FRECompanyBoardCompensation>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }

        public int BoardCode { get; set; }
        public string BoardName { get; set; }
        public string BoardDescription { get; set; }
        public float NumberOfMembers { get; set; }
        public float MaxCompensation { get; set; }
        public float AverageCompensation { get; set; }
        public float MinCompensation { get; set; }
        public string Observation { get; set; }


        public FRECompanyBoardCompensation() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyBoardCompensation;
        public override string ElementXPath() => CVMElement.FRECompanyBoardCompensation;
        public override string Filename() => CVMFile.FRECompanyBoardCompensation;


        public override FRECompanyBoardCompensation FromElement(XElement e)
        {
            int bc = int.Parse(e.Element("CodigoOrgaoAdministrador").Value);
            string bn = string.Empty;
            switch (bc)
            {
                case 1:
                    bn = "Conselho de Administração";
                    break;

                case 2:
                    bn = "Diretoria Estatuária";
                    break;

                case 3:
                    bn = "Conselho Fiscal";
                    break;

                default:
                    throw new NotImplementedException($"Could not find a name for BoardCode {bc}");
            }

            var BoardDescription = e.Element("DescricaoOrgaoAdministrador").Value;
            var NumberOfMembers = float.Parse(e.Element("QuantidadeMembros").Value);
            var MaxCompensation = float.Parse(e.Element("ValorMaiorRemuneracao").Value);
            var AverageCompensation = float.Parse(e.Element("ValorMedioRemuneracao").Value);
            var MinCompensation = float.Parse(e.Element("ValorMenorRemuneracao").Value);
            var Observation = e.Element("Observacao").Value;

            return new FRECompanyBoardCompensation 
            {
                BoardCode = bc,
                BoardName = bn,
                BoardDescription = e.Element("DescricaoOrgaoAdministrador").Value,
                NumberOfMembers = float.Parse(e.Element("QuantidadeMembros").Value),
                MaxCompensation = float.Parse(e.Element("ValorMaiorRemuneracao").Value),
                AverageCompensation = float.Parse(e.Element("ValorMedioRemuneracao").Value),
                MinCompensation = float.Parse(e.Element("ValorMenorRemuneracao").Value),
                Observation = e.Element("Observacao").Value
            };
        }

    }
}
