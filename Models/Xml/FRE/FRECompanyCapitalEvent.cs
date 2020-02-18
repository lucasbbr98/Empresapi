using System;
using System.Xml.Linq;

namespace Models.Xml.FRE
{
    using Base;
    using Constants;

    public class FRECompanyCapitalEvent : XmlModel<FRECompanyCapitalEvent>
    {
        public int SourceId { get; set; }
        public int CompanyId { get; set; }
        public DateTime ReferenceDate { get; set; }

        public int EventId { get; set; }
        public string EventType { get; set; }
        public DateTime ApprovalDate { get; set; }
        public int EventCode { get; set; }
        public long OrdinarySharesBeforeApproval { get; set; }
        public long PreferenceSharesBeforeApproval { get; set; }
        public long OrdinarySharesAfterApproval { get; set; }
        public long PreferenceSharesAfterApproval { get; set; }


        public FRECompanyCapitalEvent() { }

        public override string Extension() => CVMFileExtension.FRE;
        public override string DocumentRoot() => CVMDocumentRoot.FRECompanyCapitalEvents;
        public override string ElementXPath() => CVMElement.FRECompanyCapitalEvents;
        public override string Filename() => CVMFile.FRECompanyCapitalEvents;


        public override FRECompanyCapitalEvent FromElement(XElement e)
        {
            var EventId = int.Parse(e.Element("NumeroIdentificacaoEventoSocietario").Value);
            var EventType = e.Element("DetalheDominio").Element("DescricaoOpcaoDominio").Value;
            var ApprovalDate = DateTime.Parse(e.Element("DataAprovacao").Value);
            var EventCode = int.Parse(e.Element("CodigoTipoEvento").Value);
            var OrdinarySharesBeforeApproval = long.Parse(e.Element("QuantidadeAcaoOrdinariaAntesAprovacao").Value);
            var PreferenceSharesBeforeApproval = long.Parse(e.Element("QuantidadeAcaoPreferencialAntesAprovacao").Value);
            var OrdinarySharesAfterApproval = long.Parse(e.Element("QuantidadeAcaoOrdinariaDepoisAprovacao").Value);
            var PreferenceSharesAfterApproval = long.Parse(e.Element("QuantidadeAcaoPreferencialDepoisAprovacao").Value);


            return new FRECompanyCapitalEvent 
            {
                EventId = int.Parse(e.Element("NumeroIdentificacaoEventoSocietario").Value),
                EventType = e.Element("DetalheDominio").Element("DescricaoOpcaoDominio").Value,
                ApprovalDate = DateTime.Parse(e.Element("DataAprovacao").Value),
                EventCode = int.Parse(e.Element("CodigoTipoEvento").Value),
                OrdinarySharesBeforeApproval = long.Parse(e.Element("QuantidadeAcaoOrdinariaAntesAprovacao").Value),
                PreferenceSharesBeforeApproval = long.Parse(e.Element("QuantidadeAcaoPreferencialAntesAprovacao").Value),
                OrdinarySharesAfterApproval = long.Parse(e.Element("QuantidadeAcaoOrdinariaDepoisAprovacao").Value),
                PreferenceSharesAfterApproval = long.Parse(e.Element("QuantidadeAcaoPreferencialDepoisAprovacao").Value)
            };
        }

    }
}
