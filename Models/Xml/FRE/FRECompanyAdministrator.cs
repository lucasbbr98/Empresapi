using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyAdministrator : XmlModel<FRECompanyAdministrator>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }
        public int Age { get; set; }
        public string JobTitle { get; set; }
        public string OtherJob { get; set; }
        public string CurriculumVitae { get; set; }
        public string Name { get; set; }
        public string Identity { get; set; }
        public string PersonType { get; set; }
        public int AdministrationCode { get; set; }
        public int RoleCode { get; set; }
        public string RoleDescription { get; set; }
        public DateTime ElectedOn { get; set; }
        public DateTime StartedOn { get; set; }
        public string TermDescription { get; set; }
        public bool ElectedByController { get; set; }
        public bool HasBeenSentenced { get; set; }
        public int? ConsecutiveTerms { get; set; }

        public FRECompanyAdministrator() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyAdministrators;
        public override string ElementXPath() => CVMElement.FRECompanyAdministrators;
        public override string Filename() => CVMFile.FRECompanyAdministrators;


        public override FRECompanyAdministrator FromElement(XElement e)
        {
            var electedByController = false;
            if (e.Element("CodEleitoPeloControlador") != null && int.Parse(e.Element("CodEleitoPeloControlador").Value.Trim()) == 1)
                electedByController = true;

            var sentenced = false;
            var elListS = e.Element("lstCondenacao");
            if(elListS != null)
            {
                var ss = elListS.Elements("Condenacao");
                foreach (var kk in ss)
                    if (kk.Element("TipoCondenacao").Value != "4")
                        sentenced = true;
            }

            int? consq = null;
            var elCon = e.Element("QteMandatosConsecutivos");
            if(elCon != null )
                consq = int.Parse(e.Element("QteMandatosConsecutivos").Value);

            var Age = int.Parse(e.Element("Idade").Value);
            var JobTitle = e.Element("DescricaoProfissao").Value;
            var OtherJob = e.Element("DescricaoOutroCargoFuncaoExercida").Value;
            var CurriculumVitae = e.Element("DescricaoCv").Value;
            var Name = e.Element("PessoaMembro").Element("NomePessoa").Value;
            var Identity = e.Element("PessoaMembro").Element("IdentificacaoPessoa").Value.Trim();
            var PersonType = e.Element("PessoaMembro").Element("TipoPessoa").Value;
            var AdministrationCode = int.Parse(e.Element("CodTipoOrgaoAdministracao").Value);
            var RoleCode = int.Parse(e.Element("CodTipoOrgaoFuncaoExercida").Value);
            var RoleDescription = e.Element("DescricaoCargoFuncaoExercida").Value;
            var ElectedOn = DateTime.Parse(e.Element("DataEleicao").Value);
            var StartedOn = DateTime.Parse(e.Element("DataPosse").Value);
            var TermDescription = e.Element("PrazoMandato").Value;


            return new FRECompanyAdministrator
            {
                Age = int.Parse(e.Element("Idade").Value),
                ConsecutiveTerms = consq,
                JobTitle = e.Element("DescricaoProfissao").Value,
                OtherJob = e.Element("DescricaoOutroCargoFuncaoExercida").Value,
                CurriculumVitae = e.Element("DescricaoCv").Value,
                Name = e.Element("PessoaMembro").Element("NomePessoa").Value,
                Identity = e.Element("PessoaMembro").Element("IdentificacaoPessoa").Value.Trim(),
                PersonType = e.Element("PessoaMembro").Element("TipoPessoa").Value,
                AdministrationCode = int.Parse(e.Element("CodTipoOrgaoAdministracao").Value),
                RoleCode = int.Parse(e.Element("CodTipoOrgaoFuncaoExercida").Value),
                RoleDescription = e.Element("DescricaoCargoFuncaoExercida").Value,
                ElectedOn = DateTime.Parse(e.Element("DataEleicao").Value),
                StartedOn = DateTime.Parse(e.Element("DataPosse").Value),
                TermDescription = e.Element("PrazoMandato").Value,
                ElectedByController = electedByController,
                HasBeenSentenced = sentenced

            };
        }

    }
}
