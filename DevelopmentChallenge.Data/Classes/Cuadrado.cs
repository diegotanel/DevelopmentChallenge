using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public class Cuadrado : IFigura
    {
        private decimal lado;
        public Cuadrado(decimal lado)
        {
            this.lado = lado;
        }
        public string Nombre { get => "Cuadrado"; }
        public string NombrePlural { get => "Cuadrados"; }

        public decimal CalcularArea()
        {
            return lado * lado;
        }
        public decimal CalcularPerimetro()
        {
            return lado * 4;
        }
    }
}
