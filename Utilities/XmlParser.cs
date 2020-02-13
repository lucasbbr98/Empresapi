using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;

namespace Utilities
{
    using Models.Base;

    public class XmlParser<T> where T: XmlModel<T>, new()
    {

        public XmlParser() { }

        public IEnumerable<T> ParseElements(Stream content)
        {    
            var model = new T();
            string xmlScale = null;
            XElement scaleElement = null;
            if (string.IsNullOrEmpty(model.Filename()) || string.IsNullOrEmpty(model.Extension()) || string.IsNullOrEmpty(model.DocumentRoot()) || string.IsNullOrEmpty(model.ElementXPath()))
                throw new ArgumentNullException($"Null required model {model.ToString()}");
            if (!string.IsNullOrEmpty(model.ScaleFilename()))
                xmlScale = CVMUnzipper.OpenScaleFile(content, model.ScaleFilename());

            if (!string.IsNullOrEmpty(xmlScale))
            {
                xmlScale = new string(xmlScale.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray());  // Treats invalids xml chars
                xmlScale = xmlScale.Replace("&#x1F;", "");    // Why does CVM let weird line separators?
                XDocument _sdoc = XDocument.Parse(xmlScale);
                scaleElement = _sdoc.XPathSelectElement($"Documento");
            }

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
            {
                var _tmpModel = model.FromElement(e);
                if (scaleElement != null)
                {
                    //_tmpModel.Currency = int.Parse(scaleElement.Element("CodigoMoeda").Value);
                    //_tmpModel.CurrencyScale = int.Parse(scaleElement.Element("CodigoEscalaMoeda").Value);
                    //_tmpModel.ShareScale = int.Parse(scaleElement.Element("CodigoEscalaQuantidade").Value);
                }
                list.Add(_tmpModel);
            }
            return list.Where(x => x != null).ToList();
        }    
        
    }
}
