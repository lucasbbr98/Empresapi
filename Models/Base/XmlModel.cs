using Models.Attributes;
using System.Xml.Linq;

namespace Models.Base
{
    public abstract class XmlModel<T>: Model
    {
        // To override use [QueryIgnore(Ignore = IgnoreType.None)]
        [QueryIgnore(Ignore = IgnoreType.All)]
        public virtual int Currency { get; set; }
        [QueryIgnore(Ignore = IgnoreType.All)]
        public virtual int CurrencyScale { get; set; }
        [QueryIgnore(Ignore = IgnoreType.All)]
        public virtual int ShareScale { get; set; }

        public virtual string ScaleFilename() => string.Empty;
        public abstract string Extension();
        public abstract string Filename();
        public abstract string DocumentRoot();
        public abstract string ElementXPath();
        public abstract T FromElement(XElement e);
    }
}
