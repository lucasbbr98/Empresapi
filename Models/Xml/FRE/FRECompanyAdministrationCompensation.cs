using System;
using System.Xml.Linq;
using System.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;
    using System.Collections.Generic;

    public class FRECompanyAdministrationCompensation : XmlModel<FRECompanyAdministrationCompensation>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }

        public int AdministrationCode { get; set; }
        public string AdministrationType { get; set; }
        public float TotalCompensation { get; set; }
        public float FixedSalary { get; set; }
        public float FixedBenefits { get; set; }
        public float FixedCommittees { get; set; }
        public float FixedOther { get; set; }
        public string FixedOtherDescription { get; set; }
        public float VariableBonus { get; set; }
        public float VariableProfitShare { get; set; }
        public float VariableCommission { get; set; }
        public float VariableMeetings { get; set; }
        public float VariableOther { get; set; }
        public string VariableOtherDescription { get; set; }
        public float AfterJob { get; set; }
        public float BasedOnShares { get; set; }
        public float BasedOnFires { get; set; }
        public string Observation { get; set; }
        public float QuantityOfMembers { get; set; }

        public FRECompanyAdministrationCompensation() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyAdministrationCompensations;
        public override string ElementXPath() => CVMElement.FRECompanyAdministrationCompensations;
        public override string Filename() => CVMFile.FRECompanyAdministrationCompensations;
        public override bool WillReturnList() => true;


        public override FRECompanyAdministrationCompensation FromElement(XElement e)
        {
            var ac = int.Parse(e.Element("CodigoOrgaoAdministrador").Value);
            var at = string.Empty;
            switch (ac)
            {
                case 0:
                    at = "Outro";
                    break;

                case 1:
                    at = "Conselho de Administração";
                    break;

                case 2:
                    at = "Diretoria Estatuária";
                    break;

                case 3:
                    at = "Conselho Fiscal";
                    break;

                default:
                    throw new NotImplementedException($"Could not find a name for AdministrationCode {at}");
            }

            var TotalCompensation = float.Parse(e.Element("ValorTotalRemuneracao").Value);
            var FixedSalary = float.Parse(e.Element("ValorFixoSalario").Value);
            var FixedBenefits = float.Parse(e.Element("ValorFixoBeneficios").Value);
            var FixedCommittees = float.Parse(e.Element("ValorFixoParticipacoesComites").Value);
            var FixedOther = float.Parse(e.Element("ValorFixoOutros").Value);
            var FixedOtherDescription = e.Element("DescricaoOutrasRemuneracoesFixas").Value;
            var VariableBonus = float.Parse(e.Element("ValorVariavelBonus").Value);
            var VariableProfitShare = float.Parse(e.Element("ValorVariavelParticipacaoResultados").Value);
            var VariableCommission = float.Parse(e.Element("ValorVariavelComissoes").Value);
            var VariableMeetings = float.Parse(e.Element("ValorVariavelParticipacoesReunioes").Value);
            var VariableOther = float.Parse(e.Element("ValorVariavelOutros").Value);
            var VariableOtherDescription = e.Element("DescricaoOutrasRemuneracoesVariaveis").Value;
            var AfterJob = float.Parse(e.Element("ValorBeneficiosPosEmprego").Value);
            var BasedOnShares = float.Parse(e.Element("ValorBeneficiosBaseadaAcoes").Value);
            var BasedOnFires = float.Parse(e.Element("ValorBeneficiosCessacaoCargo").Value);
            var Observation = e.Element("Observacao").Value;
            var QuantityOfMembers = float.Parse(e.Element("QuantidadeMembros").Value);

            return new FRECompanyAdministrationCompensation 
            {
                 AdministrationCode = ac,
                 AdministrationType = at,
                 TotalCompensation = float.Parse(e.Element("ValorTotalRemuneracao").Value),
                 FixedSalary = float.Parse(e.Element("ValorFixoSalario").Value),
                 FixedBenefits = float.Parse(e.Element("ValorFixoBeneficios").Value),
                 FixedCommittees = float.Parse(e.Element("ValorFixoParticipacoesComites").Value),
                 FixedOther = float.Parse(e.Element("ValorFixoOutros").Value),
                 FixedOtherDescription = e.Element("DescricaoOutrasRemuneracoesFixas").Value,
                 VariableBonus = float.Parse(e.Element("ValorVariavelBonus").Value),
                 VariableMeetings = float.Parse(e.Element("ValorVariavelParticipacoesReunioes").Value),
                 VariableProfitShare = float.Parse(e.Element("ValorVariavelParticipacaoResultados").Value),
                 VariableCommission = float.Parse(e.Element("ValorVariavelComissoes").Value),
                 VariableOther = float.Parse(e.Element("ValorVariavelOutros").Value),
                 VariableOtherDescription = e.Element("DescricaoOutrasRemuneracoesVariaveis").Value,
                 AfterJob = float.Parse(e.Element("ValorBeneficiosPosEmprego").Value),
                 BasedOnShares = float.Parse(e.Element("ValorBeneficiosBaseadaAcoes").Value),
                 BasedOnFires = float.Parse(e.Element("ValorBeneficiosCessacaoCargo").Value),
                 Observation = e.Element("Observacao").Value,
                 QuantityOfMembers = float.Parse(e.Element("QuantidadeMembros").Value)
        };
        }

        public override List<FRECompanyAdministrationCompensation> ListFromElement(XElement e)
        {
            List<FRECompanyAdministrationCompensation> list = new List<FRECompanyAdministrationCompensation>();
            var elList = e.Element("RemuneracaoReconhecidaOrgao").Elements("RemuneracaoReconhecidaOrgao");
            if (elList == null || !elList.Any())
                return list;

            list.Add(this.FromElement(elList.First()));

            return list;
        }

    }
}
