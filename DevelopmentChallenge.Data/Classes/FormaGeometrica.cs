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
        public IFigura Tipo { get; }

        public FormaGeometrica(IFigura tipo)
        {
            this.Tipo = tipo;
        }

        public static string Imprimir(List<FormaGeometrica> formas, IIdiomaHelper idiomaHelper)
        {
            var sb = new StringBuilder();

            if (!formas.Any())
            {
                string listaVacia = "<h1>" + idiomaHelper.ListaVacia + "</h1>";
                sb.Append(listaVacia);
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER
                string encabezado = "<h1>" + idiomaHelper.Encabezado + "</h1>";
                sb.Append(encabezado);

                int cantidadTotalFormas = 0;
                decimal cantidadTotalArea = 0m;
                decimal cantidadTotalPerimetro = 0m;

                Dictionary<string, DatosFormaReporte> dicForm = new Dictionary<string, DatosFormaReporte>();
                foreach (var forma in formas)
                {
                    string key = forma.Tipo.Nombre;
                    DatosFormaReporte dfr = new DatosFormaReporte();
                    if (dicForm.ContainsKey(key))
                    {
                        dfr = dicForm[key];
                        LlenarDatosFormaReporte(forma, dfr);
                        dicForm[key] = dfr;
                    }
                    else
                    {
                        LlenarDatosFormaReporte(forma, dfr);
                        dfr.fg = forma.Tipo;
                        dicForm.Add(key, dfr);
                    }

                }

                foreach (var item in dicForm)
                {
                    int elementos = item.Value.cantidadDeElementos;
                    decimal area = item.Value.totalArea;
                    decimal perimetro = item.Value.totalPerimetro;
                    IFigura forma = item.Value.fg;
                    sb.Append(ObtenerLinea(elementos, area, perimetro, forma, idiomaHelper));
                    cantidadTotalFormas += elementos;
                    cantidadTotalArea += area;
                    cantidadTotalPerimetro += perimetro;
                }

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(cantidadTotalFormas + " " + idiomaHelper.Formas("Formas") + " ");
                sb.Append(idiomaHelper.Formas("Perimetro") + " " + (cantidadTotalPerimetro).ToString("#.##") + " ");
                sb.Append(idiomaHelper.Formas("Area") + " " + (cantidadTotalArea).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static void LlenarDatosFormaReporte(FormaGeometrica forma, DatosFormaReporte dfr)
        {
            dfr.cantidadDeElementos++;
            dfr.totalArea += forma.CalcularArea();
            dfr.totalPerimetro += forma.CalcularPerimetro();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, IFigura tipo, IIdiomaHelper idiomaHelper)
        {
            if (cantidad > 0)
            {
                return $"{cantidad} {TraducirForma(tipo, cantidad, idiomaHelper)} | {idiomaHelper.Area} {area:#.##} | {idiomaHelper.Perimetro} {perimetro:#.##} <br/>";
            }
            return string.Empty;
        }

        private static string TraducirForma(IFigura tipo, int cantidad, IIdiomaHelper idiomaHelper)
        {
            return cantidad == 1 ? idiomaHelper.Formas(tipo.Nombre) : idiomaHelper.Formas(tipo.NombrePlural);
        }

        public decimal CalcularArea()
        {
            return Tipo.CalcularArea();
        }

        public decimal CalcularPerimetro()
        {
            return Tipo.CalcularPerimetro();
        }
    }
}
