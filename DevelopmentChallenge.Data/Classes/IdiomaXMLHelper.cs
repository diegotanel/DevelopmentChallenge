using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Xml;
using System.IO;

namespace DevelopmentChallenge.Data.Classes
{
    public class IdiomaXMLHelper : IIdiomaHelper
    {
        private XmlDocument xmlDoc;

        public IdiomaXMLHelper(string idioma)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(string.Format("{0}.xml", idioma));
            xmlDoc = xml;
            //adicionar código defensivo en caso que el idioma no este definido
        }

        public string ListaVacia { get { return this.buscarValorXML("ListaVacia"); } }

        public string Encabezado { get { return this.buscarValorXML("Encabezado"); } }

        public string Area { get { return this.buscarValorXML("Area"); } }

        public string Perimetro { get { return this.buscarValorXML("Perimetro"); } }

        public string Formas(string nombre)
        {
            return this.buscarValorXML(nombre);
        }

        private string buscarValorXML(string nombre)
        {
            XmlNode node = xmlDoc.SelectSingleNode(string.Format("/etiquetas/etiqueta[@key='{0}']", nombre));
            return node.Attributes["value"].Value;
            //adicionar código defensivo en caso que la llave no este definida
        }
    }
}
