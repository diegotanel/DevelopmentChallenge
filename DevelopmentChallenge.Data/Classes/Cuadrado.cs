using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public class Cuadrado : IFormaGeometrica
    {
        public string Nombre { get => "Cuadrado"; }
        public string NombrePlural { get => "Cuadrados"; }

        public decimal CalcularArea(decimal lado)
        {    
            return lado * lado;
        }
        public decimal CalcularPerimetro(decimal lado)
        {
            return lado * 4;
        }
    }
}
