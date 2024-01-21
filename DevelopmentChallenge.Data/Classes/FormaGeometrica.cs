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

        //public const int Cuadrado = 1;
        //public const int TrianguloEquilatero = 2;
        //public const int Circulo = 3;
        //public const int Trapecio = 4;

        #endregion

        #region Idiomas

        public const int Castellano = 1;
        public const int Ingles = 2;
        public const int Italiano = 3;

        #endregion

        private readonly decimal _lado;
        public IFormaGeometrica Tipo { get; }

        public FormaGeometrica(IFormaGeometrica tipo, decimal ancho)
        {
            this.Tipo = tipo;
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

                Dictionary<string,DatosFormaReporte> dicForm = new Dictionary<string, DatosFormaReporte>();
                foreach (var forma in formas)
                {
                    string key = forma.Tipo.Nombre;
                    DatosFormaReporte dfr = new DatosFormaReporte();
                    if (dicForm.ContainsKey(key))
                    {
                        dfr = dicForm[key];
                        dfr.cantidadDeElementos++;
                        dfr.totalArea += forma.CalcularArea();
                        dfr.totalPerimetro += forma.CalcularPerimetro();
                        dicForm[key] = dfr;
                    }
                    else
                    {
                        dfr.cantidadDeElementos++;
                        dfr.totalArea += forma.CalcularArea();
                        dfr.totalPerimetro += forma.CalcularPerimetro();
                        dfr.fg = forma.Tipo;
                        dicForm.Add(key, dfr);
                    }                    

                }


                //for (var i = 0; i < formas.Count; i++)
                //{
                //    if (formas[i].Tipo.Nombre == "Cuadrado")
                //    {
                //        numeroCuadrados++;
                //        areaCuadrados += formas[i].CalcularArea();
                //        perimetroCuadrados += formas[i].CalcularPerimetro();
                //        ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, formas[i].Tipo, idioma, reporte);
                //    }
                //    if (formas[i].Tipo.Nombre == "Circulo")
                //    {
                //        numeroCirculos++;
                //        areaCirculos += formas[i].CalcularArea();
                //        perimetroCirculos += formas[i].CalcularPerimetro();
                //    }
                //    if (formas[i].Tipo.Nombre == "Triangulo_Equilatero")
                //    {
                //        numeroTriangulos++;
                //        areaTriangulos += formas[i].CalcularArea();
                //        perimetroTriangulos += formas[i].CalcularPerimetro();
                //    }
                //}
                IFormaGeometrica cuadrado = new Cuadrado();
                IFormaGeometrica circulo = new Circulo();
                IFormaGeometrica trianguloEquilatero = new TrianguloEquilatero();
                if (dicForm.ContainsKey("Cuadrado"))
                {
                    numeroCuadrados = dicForm["Cuadrado"].cantidadDeElementos;
                    areaCuadrados = dicForm["Cuadrado"].totalArea;
                    perimetroCuadrados = dicForm["Cuadrado"].totalPerimetro;
                    cuadrado = dicForm["Cuadrado"].fg;
                }

                if (dicForm.ContainsKey("Circulo"))
                {
                    numeroCirculos = dicForm["Circulo"].cantidadDeElementos;
                    areaCirculos = dicForm["Circulo"].totalArea;
                    perimetroCirculos = dicForm["Circulo"].totalPerimetro;
                    circulo = dicForm["Circulo"].fg;
                }

                if (dicForm.ContainsKey("Triangulo_Equilatero"))
                {
                    numeroTriangulos = dicForm["Triangulo_Equilatero"].cantidadDeElementos;
                    areaTriangulos = dicForm["Triangulo_Equilatero"].totalArea;
                    perimetroTriangulos = dicForm["Triangulo_Equilatero"].totalPerimetro;
                    trianguloEquilatero = dicForm["Triangulo_Equilatero"].fg;
                }

                sb.Append(ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, cuadrado , idioma, reporte)) ;
                sb.Append(ObtenerLinea(numeroCirculos, areaCirculos, perimetroCirculos, circulo, idioma, reporte));
                sb.Append(ObtenerLinea(numeroTriangulos, areaTriangulos, perimetroTriangulos, trianguloEquilatero, idioma, reporte));

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(numeroCuadrados + numeroCirculos + numeroTriangulos + " " + reporte.Formas("Formas") + " ");
                sb.Append(reporte.Formas("Perimetro") + " " + (perimetroCuadrados + perimetroTriangulos + perimetroCirculos).ToString("#.##") + " ");
                sb.Append(reporte.Formas("Area") + " " + (areaCuadrados + areaCirculos + areaTriangulos).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, IFormaGeometrica tipo, int idioma, Reporte reporte)
        {
            if (cantidad > 0)
            {
                return $"{cantidad} {TraducirForma(tipo, cantidad, idioma, reporte)} | {reporte.Area} {area:#.##} | {reporte.Perimetro} {perimetro:#.##} <br/>";
            }
            return string.Empty;
        }

        private static string TraducirForma(IFormaGeometrica tipo, int cantidad, int idioma, Reporte reporte)
        {
            return cantidad == 1 ? reporte.Formas(tipo.Nombre) : reporte.Formas(tipo.NombrePlural);
        }

        public decimal CalcularArea()
        {
            return Tipo.CalcularArea(_lado);                    
        }

        public decimal CalcularPerimetro()
        {
            return Tipo.CalcularPerimetro(_lado);
        }
    }
}
