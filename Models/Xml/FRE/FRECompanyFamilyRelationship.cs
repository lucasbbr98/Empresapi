using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyFamilyRelationship : XmlModel<FRECompanyFamilyRelationship>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }

        public string AdminName { get; set; }
        public string AdminPersonType { get; set; }
        public string AdminIdentity { get; set; }
        public string AdminCompany { get; set; }
        public string AdminCompanyCnpj { get; set; }
        public string AdminCompanyType { get; set; }
        public string AdminRole { get; set; }


        public string RelatedName { get; set; }
        public string RelatedPersonType { get; set; }
        public string RelatedIdentity { get; set; }
        public string RelatedCompany { get; set; }
        public string RelatedCompanyCnpj { get; set; }
        public string RelatedCompanyType { get; set; }
        public string RelatedRole { get; set; }

        public int KinshipId { get; set; }
        public string Kinship { get; set; }
        public string Observations { get; set; }


        public FRECompanyFamilyRelationship() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyFamilyRelationships;
        public override string ElementXPath() => CVMElement.FRECompanyFamilyRelationships;
        public override string Filename() => CVMFile.FRECompanyFamilyRelationships;


        public override FRECompanyFamilyRelationship FromElement(XElement e)
        {

            var AdminName = e.Element("PessoaAdministrador").Element("NomePessoa").Value;
            var AdminPersonType = e.Element("PessoaAdministrador").Element("TipoPessoa").Value;
            var AdminIdentity = e.Element("PessoaAdministrador").Element("IdentificacaoPessoa").Value;
            var AdminCompany = e.Element("PessoaAdministrador").Element("NomePessoa").Value;
            var AdminCnpj = e.Element("PessoaAdministrador").Element("IdentificacaoPessoa").Value;
            var AdminCompanyType = e.Element("PessoaAdministrador").Element("TipoPessoa").Value;
            var AdminRole = e.Element("FuncaoAdministrador").Value;

            var RelatedName = e.Element("PessoaRelacaoConjugal").Element("NomePessoa").Value;
            var RelatedPersonType = e.Element("PessoaRelacaoConjugal").Element("TipoPessoa").Value;
            var RelatedIdentity = e.Element("PessoaRelacaoConjugal").Element("IdentificacaoPessoa").Value;
            var RelatedCompany = e.Element("PessoaRelacaoConjugal").Element("NomePessoa").Value;
            var RelatedCnpj = e.Element("PessoaRelacaoConjugal").Element("IdentificacaoPessoa").Value;
            var RelatedCompanyType = e.Element("PessoaRelacaoConjugal").Element("TipoPessoa").Value;
            var RelatedRole = e.Element("FuncaoRelacaoConjugal").Value;

            var KinshipId = int.Parse(e.Element("RelacaoParentesco").Value);
            var Kinship = e.Element("DescRelacaoParentesco").Value;
            var Observations = e.Element("Observacoes").Value;


            return new FRECompanyFamilyRelationship
            {
                AdminName = e.Element("PessoaAdministrador").Element("NomePessoa").Value,
                AdminPersonType = e.Element("PessoaAdministrador").Element("TipoPessoa").Value,
                AdminIdentity = e.Element("PessoaAdministrador").Element("IdentificacaoPessoa").Value,
                AdminCompany = e.Element("PessoaAdministrador").Element("NomePessoa").Value,
                AdminCompanyCnpj = e.Element("PessoaAdministrador").Element("IdentificacaoPessoa").Value,
                AdminCompanyType = e.Element("PessoaAdministrador").Element("TipoPessoa").Value,
                AdminRole = e.Element("FuncaoAdministrador").Value,

                RelatedName = e.Element("PessoaRelacaoConjugal").Element("NomePessoa").Value,
                RelatedPersonType = e.Element("PessoaRelacaoConjugal").Element("TipoPessoa").Value,
                RelatedIdentity = e.Element("PessoaRelacaoConjugal").Element("IdentificacaoPessoa").Value,
                RelatedCompany = e.Element("PessoaRelacaoConjugal").Element("NomePessoa").Value,
                RelatedCompanyCnpj = e.Element("PessoaRelacaoConjugal").Element("IdentificacaoPessoa").Value,
                RelatedCompanyType = e.Element("PessoaRelacaoConjugal").Element("TipoPessoa").Value,
                RelatedRole = e.Element("FuncaoRelacaoConjugal").Value,

                KinshipId = int.Parse(e.Element("RelacaoParentesco").Value),
                Kinship = e.Element("DescRelacaoParentesco").Value,
                Observations = e.Element("Observacoes").Value
            };
        }

    }
}
