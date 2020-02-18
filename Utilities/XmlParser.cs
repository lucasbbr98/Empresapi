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
    using Models;
    using Models.Base;
    using Models.Xml;

    public class XmlParser<T> where T: XmlModel<T>, new()
    {
        public XmlParser() { }

        public static IEnumerable<T> ParseElements(Stream content)
        {    
            var model = new T();

            if (string.IsNullOrEmpty(model.Filename()) || string.IsNullOrEmpty(model.Extension()) || string.IsNullOrEmpty(model.DocumentRoot()))
                throw new ArgumentNullException($"Null required model {model.ToString()}");

            var xml = CVMUnzipper.OpenFile(content, model.Extension(), model.Filename());
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException();

            xml = new string(xml.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray());  // Treats invalids xml chars
            xml = xml.Replace("&#x1F;", "");    // Why does CVM let weird line separators?
            xml = xml.Replace("&#x1;", "");
            xml = xml.Replace("&#x2;", "");
            var root = model.DocumentRoot().Replace("/", "");
            var xpath = model.ElementXPath();
            var list = new List<T>();
            XDocument _doc = XDocument.Parse(xml);
            List<XElement> elements;

            if (string.IsNullOrEmpty(xpath))
                elements = _doc.XPathSelectElements($"{root}").ToList();
            else
                elements = _doc.XPathSelectElements($"{root}/{xpath}").ToList();

            if (!elements.Any())
                return list;

            foreach (var e in elements)
            {
                if (model.WillReturnList())
                {
                    var tmpList = model.ListFromElement(e);
                    foreach (var i in tmpList)
                        list.Add(i);
                }
                else
                {
                    list.Add(model.FromElement(e));
                }
            }
            
      
            return list.Where(x => x != null).ToList();
        }

        public static IEnumerable<T> ParseTreasuryAction(Stream content, CVMSource source)
        {
            var model = new T();

            if (string.IsNullOrEmpty(model.Filename()) || string.IsNullOrEmpty(model.Extension()) || string.IsNullOrEmpty(model.DocumentRoot()))
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
            List<XElement> elements;

            if(source.ReferenceDate.Year < 2017)
            {
                xpath = xpath.Replace("2016", "");
                root = root.Replace("2016", "");
            }

            if (string.IsNullOrEmpty(xpath))
                elements = _doc.XPathSelectElements($"{root}").ToList();
            else
                elements = _doc.XPathSelectElements($"{root}/{xpath}").ToList();

            if (!elements.Any())
                return list;

            foreach (var e in elements)
            {
                if (model.WillReturnList())
                {
                    var tmpList = model.ListFromElement(e);
                    foreach (var i in tmpList)
                        list.Add(i);
                }
                else
                {
                    list.Add(model.FromElement(e));
                }
            }


            return list.Where(x => x != null).ToList();
        }

        public static bool Debug(Stream content, CVMSource s)
        {
            var xml = CVMUnzipper.OpenFile(content, "FRE", "ReducaoCapitalEmissor.xml");
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException();

            xml = new string(xml.Where(ch => XmlConvert.IsXmlChar(ch)).ToArray());  // Treats invalids xml chars
            xml = xml.Replace("&#x1F;", "");    // Why does CVM let weird line separators?
            var root = "ArrayOfHistoricoReducaoCapitalEmissor";
            var xpath = "HistoricoReducaoCapitalEmissor";
            var list = new List<T>();
            XDocument _doc = XDocument.Parse(xml);
            List<XElement> elements;
            var k = 1;

            if (string.IsNullOrEmpty(xpath))
                elements = _doc.XPathSelectElements($"{root}").ToList();
            else
                elements = _doc.XPathSelectElements($"{root}/{xpath}").ToList();

            if (elements.Any())
                return true;

            return false;
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
