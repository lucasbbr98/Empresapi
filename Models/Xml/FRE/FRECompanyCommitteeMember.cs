using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyCommitteeMember : XmlModel<FRECompanyCommitteeMember>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }

        public int Age { get; set; }
        public string JobTitle { get; set; }
        public string OtherJob { get; set; }
        public string Name { get; set; }
        public string Identity { get; set; }
        public string PersonType { get; set; }
        public int CommitteeCode { get; set; }
        public int RoleCode { get; set; }
        public string RoleDescription { get; set; }
        public DateTime ElectedOn { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime? Birthdate { get; set; }
        public string TermDescription { get; set; }
        public bool HasBeenSentenced { get; set; }
        public int? ConsecutiveTerms { get; set; }
        public float PercentageMeetings { get; set; }


        public FRECompanyCommitteeMember() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyCommitteeMembers;
        public override string ElementXPath() => CVMElement.FRECompanyCommitteeMembers;
        public override string Filename() => CVMFile.FRECompanyCommitteeMembers;


        public override FRECompanyCommitteeMember FromElement(XElement e)
        {
            var sentenced = false;
            var elListS = e.Element("lstCondenacao");
            if (elListS != null)
            {
                var ss = elListS.Elements("Condenacao");
                foreach (var kk in ss)
                    if (kk.Element("TipoCondenacao").Value != "4")
                        sentenced = true;
            }

            int? consq = null;
            var elCon = e.Element("QteMandatosConsecutivos");
            if (elCon != null)
                consq = int.Parse(e.Element("QteMandatosConsecutivos").Value);

            DateTime? birth = null;
            var elBirth = e.Element("DataNascimento");
            if (elBirth != null)
                birth = DateTime.Parse(elBirth.Value);

            float pm = 0;
            var elPm = e.Element("PercParticipacaoReunioes");
            if (elPm != null)
                pm = float.Parse(elPm.Value);

            var Age = int.Parse(e.Element("Idade").Value);
            var JobTitle = e.Element("DescricaoProfissao").Value;
            var OtherJob = e.Element("DescricaoOutroCargo").Value;
            var Name = e.Element("PessoaMembro").Element("NomePessoa").Value;
            var Identity = e.Element("PessoaMembro").Element("IdentificacaoPessoa").Value.Trim();
            var PersonType = e.Element("PessoaMembro").Element("TipoPessoa").Value;
            var CommitteeCode = int.Parse(e.Element("CodTipoComite").Value);
            var RoleCode = int.Parse(e.Element("CodTipoCargo").Value);
            var RoleDescription = e.Element("DescricaoCargo").Value;
            var ElectedOn = DateTime.Parse(e.Element("DataEleicao").Value);
            var StartedOn = DateTime.Parse(e.Element("DataPosse").Value);
            var TermDescription = e.Element("PrazoMandato").Value;


            return new FRECompanyCommitteeMember
            {
                Age = int.Parse(e.Element("Idade").Value),
                ConsecutiveTerms = consq,
                JobTitle = e.Element("DescricaoProfissao").Value,
                OtherJob = e.Element("DescricaoOutroCargo").Value,
                PercentageMeetings = pm,
                Name = e.Element("PessoaMembro").Element("NomePessoa").Value,
                Identity = e.Element("PessoaMembro").Element("IdentificacaoPessoa").Value.Trim(),
                PersonType = e.Element("PessoaMembro").Element("TipoPessoa").Value,
                CommitteeCode = int.Parse(e.Element("CodTipoComite").Value),
                RoleCode = int.Parse(e.Element("CodTipoCargo").Value),
                RoleDescription = e.Element("DescricaoCargo").Value,
                ElectedOn = DateTime.Parse(e.Element("DataEleicao").Value),
                StartedOn = DateTime.Parse(e.Element("DataPosse").Value),
                TermDescription = e.Element("PrazoMandato").Value,
                Birthdate = birth,
                HasBeenSentenced = sentenced

            };
        }

    }
}
