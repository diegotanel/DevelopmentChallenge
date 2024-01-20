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
            Reporte rep = new Reporte("Ingles");
            string valor = rep.Formas("Circulo");
            Console.WriteLine("Main Method");
        }
    }

 
}
