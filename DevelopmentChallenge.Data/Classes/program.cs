using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DevelopmentChallenge.Data.Classes
{
    public static class program
    {
        static public void Main(String[] args)
        {
            Dictionary<int, string> map = new Dictionary<int, string>();
            map.Add(FormaGeometrica.Castellano, "Castellano");
            map.Add(FormaGeometrica.Ingles, "Ingles");
            map.Add(FormaGeometrica.Italiano, "Italiano");
            string valor = map.FirstOrDefault(pair => pair.Key == FormaGeometrica.Castellano).Value;
            string valor2 = map.Where(p => p.Key == FormaGeometrica.Castellano).ToString();
            if ("Castellano" == valor)
            {
                
            }

                Console.WriteLine("Main Method");
        }
    }

 
}
