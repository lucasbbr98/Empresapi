using System.Xml.Linq;

namespace Models.Base
{
    public abstract class XmlModel<T>: Model
    {
        public abstract string DocumentRoot();
        public abstract string ElementXPath();
        public abstract T FromElement(XElement e);
    }
}
