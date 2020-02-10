using Models.Base;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Utilities
{
    public class XmlParser<T> where T: XmlModel<T>, new()
    {

        public XmlParser() { }

        public IEnumerable<T> ParseElements(string xml)
        {
            xml = new string(xml.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray());  // Treats invalids xml chars
            xml = xml.Replace("&#x1F;", "");    // Why does CVM let weird line separators?
            var model = new T();
            var root = model.DocumentRoot().Replace("/", "");
            var xpath = model.ElementXPath();
            var list = new List<T>();
            XDocument _doc = XDocument.Parse(xml);
            List<XElement> elements = _doc.XPathSelectElements($"{root}/{xpath}").ToList();

            if (!elements.Any())
                return list;

            foreach (var e in elements)
                list.Add(model.FromElement(e));
            
            return list.Where(x => x != null).ToList();
        }
    }
}
