using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public class Rectangulo : IFigura
    {
        private decimal _base;
        private decimal _altura;
        public Rectangulo(decimal _base, decimal _altura)
        {
            this._base = _base;
            this._altura = _altura;
        }
        public string Nombre { get => "Rectangulo"; }

        public string NombrePlural { get => "Rectangulos"; }

        public decimal CalcularArea()
        {
            return _base * _altura;
        }

        public decimal CalcularPerimetro()
        {
            return 2 * (_base + _altura);
        }
    }
}
