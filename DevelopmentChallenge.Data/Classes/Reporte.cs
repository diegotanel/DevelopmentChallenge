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
    public class Reporte
    {
        private XmlDocument xmlDoc;

        public Reporte(string idioma) 
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(string.Format("{0}.xml", idioma));
            xmlDoc = xml;
            //adicionar código defensivo en caso que el idioma no este definido
        }

        public string Formas(string nombre)
        {
            XmlNode node = xmlDoc.SelectSingleNode(string.Format("/etiquetas/etiqueta[@key='{0}']", nombre));
            return node.Attributes["value"].Value;
            //adicionar código defensivo en caso que no exista el valor asignado
        }
    }
}
