/******************************************************************************************************************/
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
using System.Runtime.CompilerServices;
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
                
                sb.Append(ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, Cuadrado, idioma, reporte));
                sb.Append(ObtenerLinea(numeroCirculos, areaCirculos, perimetroCirculos, Circulo, idioma, reporte));
                sb.Append(ObtenerLinea(numeroTriangulos, areaTriangulos, perimetroTriangulos, TrianguloEquilatero, idioma, reporte));

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(numeroCuadrados + numeroCirculos + numeroTriangulos + " " + reporte.Formas("Formas") + " ");
                sb.Append(reporte.Formas("Perimetro") + " " + (perimetroCuadrados + perimetroTriangulos + perimetroCirculos).ToString("#.##") + " ");
                sb.Append(reporte.Formas("Area") + " " + (areaCuadrados + areaCirculos + areaTriangulos).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, int tipo, int idioma, Reporte reporte)
        {
            if (cantidad > 0)
            {
                return $"{cantidad} {TraducirForma(tipo, cantidad, idioma, reporte)} | {reporte.Area} {area:#.##} | {reporte.Perimetro} {perimetro:#.##} <br/>";
            }
            return string.Empty;
        }

        private static string TraducirForma(int tipo, int cantidad, int idioma, Reporte reporte)
        {
            IFormaGeometrica forma;
            switch (tipo)
            {
                case Cuadrado:
                    forma = new Cuadrado();
                    return cantidad == 1 ? reporte.Formas(forma.Nombre) : reporte.Formas(forma.NombrePlural);
                case Circulo:
                    forma = new Circulo();
                    return cantidad == 1 ? reporte.Formas(forma.Nombre) : reporte.Formas(forma.NombrePlural);
                case TrianguloEquilatero:
                    forma = new TrianguloEquilatero();
                    return cantidad == 1 ? reporte.Formas(forma.Nombre) : reporte.Formas(forma.NombrePlural);
            }

            return string.Empty;
        }

        public decimal CalcularArea()
        {
            switch (Tipo)
            {
                case Cuadrado: return new Cuadrado().CalcularArea(_lado);
                case Circulo: return new Circulo().CalcularArea(_lado);
                case TrianguloEquilatero: return new TrianguloEquilatero().CalcularArea(_lado);
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }

        public decimal CalcularPerimetro()
        {
            switch (Tipo)
            {
                case Cuadrado: return new Cuadrado().CalcularPerimetro(_lado);
                case Circulo: return new Circulo().CalcularPerimetro(_lado);
                case TrianguloEquilatero: return new TrianguloEquilatero().CalcularPerimetro(_lado);
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }
    }
}
