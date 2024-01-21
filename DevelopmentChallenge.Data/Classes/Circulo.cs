using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public class Circulo : IFigura
    {
        private decimal lado;
        public Circulo(decimal lado)
        {
            this.lado = lado;
        }

        public string Nombre { get => "Circulo"; }
        public string NombrePlural { get => "Circulos"; }

        public decimal CalcularArea()
        {
            return (decimal)Math.PI * (lado / 2) * (lado / 2);
        }

        public decimal CalcularPerimetro()
        {
            return (decimal)Math.PI * lado;
        }
    }
}
