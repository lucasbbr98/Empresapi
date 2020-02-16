using System;
using System.Xml.Linq;

namespace Models.Xml
{
    using Constants;
    using Base;

    public class Scale : XmlModel<Scale>
    {
        private string SourceType { get; set; }
        public virtual int CurrencyScale { get; set; }
        public virtual int QuantityScale { get; set; }

        public override bool IsScale() => true;
        public override string Extension() => string.Empty;
        public override string DocumentRoot() => string.Empty;
        public override string ElementXPath() => string.Empty;
        public override string Filename() => string.Empty;

        public override string ScaleFilename() => $"{this.SourceType}";

        public Scale() { }
        

        private Scale(int cs, int sc)
        {
            cs = translateScale(cs);
            sc = translateScale(sc);

            if (cs <= 0 || sc <= 0)
                throw new NotImplementedException($"Scale not implemented: {cs}, {sc}");

            this.CurrencyScale = cs;
            this.QuantityScale = sc;
        }

        private int translateScale(int s)
        {
            if (s == 1)
                return 1;

            if (s == 2)
                return 1000;

            return 0;
        }

        public override Scale FromElement(XElement e)
        {
            return new Scale(
                int.Parse(e.Element("CodigoEscalaMoeda").Value),
                int.Parse(e.Element("CodigoEscalaQuantidade").Value)
                );
        }
    }
}
