using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public class TrianguloEquilatero : IFigura
    {
        private decimal lado;
        public TrianguloEquilatero(decimal lado)
        {
            this.lado = lado;
        }
        public string Nombre { get => "Triangulo_Equilatero"; }
        public string NombrePlural { get => "Triangulos_Equilateros"; }
        public decimal CalcularArea()
        {
            return ((decimal)Math.Sqrt(3) / 4) * lado * lado;
        }
        public decimal CalcularPerimetro()
        {
            return lado * 3;
        }
    }
}
