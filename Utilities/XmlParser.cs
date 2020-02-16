using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;

namespace Utilities
{
    using Constants;
    using Models.Base;
    using Models.Xml;

    public class XmlParser<T> where T: XmlModel<T>, new()
    {
        public XmlParser() { }

        public static IEnumerable<T> ParseElements(Stream content)
        {    
            var model = new T();

            if (string.IsNullOrEmpty(model.Filename()) || string.IsNullOrEmpty(model.Extension()) || string.IsNullOrEmpty(model.DocumentRoot()) || string.IsNullOrEmpty(model.ElementXPath()))
                throw new ArgumentNullException($"Null required model {model.ToString()}");

            var xml = CVMUnzipper.OpenFile(content, model.Extension(), model.Filename());
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException();

            xml = new string(xml.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray());  // Treats invalids xml chars
            xml = xml.Replace("&#x1F;", "");    // Why does CVM let weird line separators?
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

        public static Scale GetScale(Stream content, string extension, string scaleFile = CVMFile.DefaultScaleFile)
        {
            extension = extension.Replace(".", "").ToLower();
            var xml = CVMUnzipper.OpenFile(content, extension, scaleFile);
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException();

            xml = new string(xml.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray());  // Treats invalids xml chars
            xml = xml.Replace("&#x1F;", "");    // Why does CVM let weird line separators?
            XDocument _doc = XDocument.Parse(xml);
            XElement el = _doc.XPathSelectElement("Documento");
            return new Scale().FromElement(el);
        }
        
    }
}
