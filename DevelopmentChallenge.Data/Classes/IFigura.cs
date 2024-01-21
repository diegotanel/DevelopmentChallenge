using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public interface IFigura
    {
        string Nombre { get; }
        string NombrePlural { get; }
        decimal CalcularArea();
        decimal CalcularPerimetro();
    }
}
