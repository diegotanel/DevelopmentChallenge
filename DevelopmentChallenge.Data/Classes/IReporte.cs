using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public interface IReporte
    {
        string ListaVacia { get; }
        string Encabezado { get; }
        string Area { get; }
        string Perimetro { get; }
        string Formas(string nombre);
    }
}
