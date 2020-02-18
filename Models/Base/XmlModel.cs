using System.Collections.Generic;
using System.Xml.Linq;

namespace Models.Base
{
    public abstract class XmlModel<T>: Model
    {
        public virtual bool IsScale() => false;
        public virtual bool WillReturnList() => false;
        public virtual string ScaleFilename() => string.Empty;
        public abstract string Extension();
        public abstract string Filename();
        public abstract string DocumentRoot();
        public abstract string ElementXPath();
        public abstract T FromElement(XElement e);
        public virtual List<T> ListFromElement(XElement e) => new List<T>();
    }
}
