using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public class DatosFormaReporte
    {
        public IFormaGeometrica fg {  get; set; }
        public int cantidadDeElementos { get; set; }
        public decimal totalArea { get; set; }
        public decimal totalPerimetro { get; set; }
    }
}
