using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanySubordinateRelationship : XmlModel<FRECompanySubordinateRelationship>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }

        public string AdminName { get; set; }
        public string AdminIdentity { get; set; }
        public string AdminPersonType { get; set; }
        public string AdminRole { get; set; }
        
        public string SubordinateName { get; set; }
        public string SubordinateCnpj { get; set; }
        public string SubordinateType { get; set; }
        public string SubordinateRole { get; set; }

        public int RelationshipCode { get; set; }
        public string RelationshipDescription { get; set; }
        public int SubordinationCode { get; set; }
        public string SubordinationDescription { get; set; }
        public string Observation { get; set; }
        public string ActionType { get; set; }

        public FRECompanySubordinateRelationship() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanySubordinateRelationships;
        public override string ElementXPath() => CVMElement.FRECompanySubordinateRelationships;
        public override string Filename() => CVMFile.FRECompanySubordinateRelationships;


        public override FRECompanySubordinateRelationship FromElement(XElement e)
        {


            var AdminName = e.Element("AdministradorCadastroPessoa").Element("NomePessoa").Value;
            var AdminIdentity = e.Element("AdministradorCadastroPessoa").Element("IdentificacaoPessoa").Value.Trim();
            var AdminPersonType = e.Element("AdministradorCadastroPessoa").Element("TipoPessoa").Value;
            var AdminRole = e.Element("DescricaoCargoFuncaoAdministrador").Value;
            var SubordinateName = e.Element("PessoaSubordinadaAdministradorCadastroPessoa").Element("NomePessoa").Value;
            var SubordinateCnpj = e.Element("PessoaSubordinadaAdministradorCadastroPessoa").Element("IdentificacaoPessoa").Value;
            var SubordinateType = e.Element("PessoaSubordinadaAdministradorCadastroPessoa").Element("TipoPessoa").Value;
            var SubordinateRole = e.Element("DescricaoCargoFuncaoPessoaSubordinada").Value;
            var RelationshipCode = int.Parse(e.Element("CodigoTipoRelacao").Value);
            var RelationshipDescription = e.Element("DescricaoTipoRelacao").Value;
            var SubordinationCode = int.Parse(e.Element("CodigoSubordinacao").Value);
            var SubordinationDescription = e.Element("DescricaoSubordinacao").Value;
            var Observation = e.Element("DescricaoObservacao").Value;
            var ActionType = e.Element("TipoAcaoRealizada").Value;

            return new FRECompanySubordinateRelationship 
            {
                 AdminName = e.Element("AdministradorCadastroPessoa").Element("NomePessoa").Value,
                 AdminIdentity = e.Element("AdministradorCadastroPessoa").Element("IdentificacaoPessoa").Value.Trim(),
                 AdminPersonType = e.Element("AdministradorCadastroPessoa").Element("TipoPessoa").Value,
                 AdminRole = e.Element("DescricaoCargoFuncaoAdministrador").Value,
                 SubordinateName = e.Element("PessoaSubordinadaAdministradorCadastroPessoa").Element("NomePessoa").Value,
                 SubordinateCnpj = e.Element("PessoaSubordinadaAdministradorCadastroPessoa").Element("IdentificacaoPessoa").Value,
                 SubordinateType = e.Element("PessoaSubordinadaAdministradorCadastroPessoa").Element("TipoPessoa").Value,
                 SubordinateRole = e.Element("DescricaoCargoFuncaoPessoaSubordinada").Value,
                 RelationshipCode = int.Parse(e.Element("CodigoTipoRelacao").Value),
                 RelationshipDescription = e.Element("DescricaoTipoRelacao").Value,
                 SubordinationCode = int.Parse(e.Element("CodigoSubordinacao").Value),
                 SubordinationDescription = e.Element("DescricaoSubordinacao").Value,
                 Observation = e.Element("DescricaoObservacao").Value,
                 ActionType = e.Element("TipoAcaoRealizada").Value,
            };
        }

    }
}
