﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevelopmentChallenge.Data.Classes
{
    public interface IFormaGeometrica
    {
        string Nombre { get; }
        string NombrePlural { get; }
        decimal CalcularArea(decimal lado);
        decimal CalcularPerimetro(decimal lado);
    }
}
