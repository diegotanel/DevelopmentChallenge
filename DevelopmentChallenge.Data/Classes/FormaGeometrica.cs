﻿/******************************************************************************************************************/
/******* ¿Qué pasa si debemos soportar un nuevo idioma para los reportes, o agregar más formas geométricas? *******/
/******************************************************************************************************************/

/*
 * TODO: 
 * Refactorizar la clase para respetar principios de la programación orientada a objetos.
 * Implementar la forma Trapecio/Rectangulo. 
 * Agregar el idioma Italiano (o el deseado) al reporte.
 * Se agradece la inclusión de nuevos tests unitarios para validar el comportamiento de la nueva funcionalidad agregada (los tests deben pasar correctamente al entregar la solución, incluso los actuales.)
 * Una vez finalizado, hay que subir el código a un repo GIT y ofrecernos la URL para que podamos utilizar la nueva versión :).
 */

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DevelopmentChallenge.Data.Classes
{
    public class FormaGeometrica
    {
        #region Formas
        public const int Cuadrado = 1;
        public const int TrianguloEquilatero = 2;
        public const int Circulo = 3;
        public const int Trapecio = 4;

        #endregion

        #region Idiomas

        public const int Castellano = 1;
        public const int Ingles = 2;
        public const int Italiano = 3;

        #endregion

        private readonly decimal _lado;

        public int Tipo { get; set; }

        public FormaGeometrica(int tipo, decimal ancho)
        {
            Tipo = tipo;
            _lado = ancho;
        }

        public static string Imprimir(List<FormaGeometrica> formas, int idioma)
        {
            
            Dictionary<int,string> map = new Dictionary<int,string>();
            map.Add(FormaGeometrica.Castellano, "Castellano");
            map.Add(FormaGeometrica.Ingles, "Ingles");
            map.Add(FormaGeometrica.Italiano, "Italiano");
            string language = map.FirstOrDefault(pair => pair.Key == idioma).Value;

            Reporte reporte = new Reporte(language);
            var sb = new StringBuilder();

            if (!formas.Any())
            {
                string listaVacia = "<h1>" + reporte.ListaVacia + "</h1>";
                sb.Append(listaVacia);
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER
                string encabezado = "<h1>" + reporte.Encabezado + "</h1>";
                sb.Append(encabezado);
                
                var numeroCuadrados = 0;
                var numeroCirculos = 0;
                var numeroTriangulos = 0;

                var areaCuadrados = 0m;
                var areaCirculos = 0m;
                var areaTriangulos = 0m;

                var perimetroCuadrados = 0m;
                var perimetroCirculos = 0m;
                var perimetroTriangulos = 0m;

                for (var i = 0; i < formas.Count; i++)
                {
                    if (formas[i].Tipo == Cuadrado)
                    {
                        numeroCuadrados++;
                        areaCuadrados += formas[i].CalcularArea();
                        perimetroCuadrados += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == Circulo)
                    {
                        numeroCirculos++;
                        areaCirculos += formas[i].CalcularArea();
                        perimetroCirculos += formas[i].CalcularPerimetro();
                    }
                    if (formas[i].Tipo == TrianguloEquilatero)
                    {
                        numeroTriangulos++;
                        areaTriangulos += formas[i].CalcularArea();
                        perimetroTriangulos += formas[i].CalcularPerimetro();
                    }
                }
                
                sb.Append(ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, Cuadrado, idioma));
                sb.Append(ObtenerLinea(numeroCirculos, areaCirculos, perimetroCirculos, Circulo, idioma));
                sb.Append(ObtenerLinea(numeroTriangulos, areaTriangulos, perimetroTriangulos, TrianguloEquilatero, idioma));

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(numeroCuadrados + numeroCirculos + numeroTriangulos + " " + (idioma == Castellano ? "formas" : "shapes") + " ");
                sb.Append((idioma == Castellano ? "Perimetro " : "Perimeter ") + (perimetroCuadrados + perimetroTriangulos + perimetroCirculos).ToString("#.##") + " ");
                sb.Append("Area " + (areaCuadrados + areaCirculos + areaTriangulos).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, int tipo, int idioma)
        {
            if (cantidad > 0)
            {
                if (idioma == Castellano)
                    return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>";

                return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimeter {perimetro:#.##} <br/>";
            }

            return string.Empty;
        }

        private static string TraducirForma(int tipo, int cantidad, int idioma)
        {
            switch (tipo)
            {
                case Cuadrado:
                    if (idioma == Castellano) return cantidad == 1 ? "Cuadrado" : "Cuadrados";
                    else return cantidad == 1 ? "Square" : "Squares";
                case Circulo:
                    if (idioma == Castellano) return cantidad == 1 ? "Círculo" : "Círculos";
                    else return cantidad == 1 ? "Circle" : "Circles";
                case TrianguloEquilatero:
                    if (idioma == Castellano) return cantidad == 1 ? "Triángulo" : "Triángulos";
                    else return cantidad == 1 ? "Triangle" : "Triangles";
            }

            return string.Empty;
        }

        public decimal CalcularArea()
        {
            switch (Tipo)
            {
                case Cuadrado: return new Cuadrado(_lado).CalcularArea();
                case Circulo: return new Circulo(_lado).CalcularArea();
                case TrianguloEquilatero: return new TrianguloEquilatero(_lado).CalcularArea();
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }

        public decimal CalcularPerimetro()
        {
            switch (Tipo)
            {
                case Cuadrado: return new Cuadrado(_lado).CalcularPerimetro();
                case Circulo: return new Circulo(_lado).CalcularPerimetro();
                case TrianguloEquilatero: return new TrianguloEquilatero(_lado).CalcularPerimetro();
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }
    }
}
