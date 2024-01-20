using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public class TrianguloEquilatero : IFormaGeometrica
    {
        public decimal CalcularArea(decimal lado)
        {
            return ((decimal)Math.Sqrt(3) / 4) * lado * lado;
        }
        public string Nombre { get => "Triangulo_Equilatero"; }
        public string NombrePlural { get => "Triangulos_Equilateros"; }

        public decimal CalcularPerimetro(decimal lado)
        {
            return lado * 3;
        }
    }
}
