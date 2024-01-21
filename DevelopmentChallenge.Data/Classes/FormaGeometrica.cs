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

                int cantidadTotalFormas = 0;
                decimal cantidadTotalArea = 0m;
                decimal cantidadTotalPerimetro = 0m;

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

                foreach (var item in dicForm)
                {
                    int elementos = item.Value.cantidadDeElementos;
                    decimal area = item.Value.totalArea;
                    decimal perimetro = item.Value.totalPerimetro;
                    IFormaGeometrica forma = item.Value.fg;
                    sb.Append(ObtenerLinea(elementos, area, perimetro, forma, idioma, reporte));
                    cantidadTotalFormas += elementos;
                    cantidadTotalArea += area;
                    cantidadTotalPerimetro += perimetro;
                }

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(cantidadTotalFormas + " " + reporte.Formas("Formas") + " ");
                sb.Append(reporte.Formas("Perimetro") + " " + (cantidadTotalPerimetro).ToString("#.##") + " ");
                sb.Append(reporte.Formas("Area") + " " + (cantidadTotalArea).ToString("#.##"));
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
