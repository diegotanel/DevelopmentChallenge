using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using DevelopmentChallenge.Data.Classes;
using NUnit.Framework;

namespace DevelopmentChallenge.Data.Tests
{
    [TestFixture]
    public class DataTests
    {

        [TestCase]
        public void TestCalcularAreaConUnCuadrado()
        {
            Assert.AreEqual(25m, new Cuadrado(5).CalcularArea());
        }

        [TestCase]
        public void TestCalcularPerimetroConUnCuadrado()
        {
            Assert.AreEqual(20m, new Cuadrado(5).CalcularPerimetro());
        }

        [TestCase]
        public void TestCalcularAreaConUnCirculo()
        {
            Assert.AreEqual(3.14159265358979m, new Circulo(2).CalcularArea());
        }

        [TestCase]
        public void TestCalcularPerimetroConUnCirculo()
        {
            Assert.AreEqual(6.28318530717958m, new Circulo(2).CalcularPerimetro());
        }

        [TestCase]
        public void TestCalcularAreaConUnTrianguloEquilatero()
        {
            Assert.AreEqual(10.8253175473055m, new TrianguloEquilatero(5).CalcularArea());
        }
        [TestCase]
        public void TestCalcularPerimetroConUnTrianguloEquilatero()
        {
            Assert.AreEqual(15m, new TrianguloEquilatero(5).CalcularPerimetro());
        }

        [TestCase]
        public void TestCalcularAreaConUnRectangulo()
        {
            Assert.AreEqual(8m, new Rectangulo(4,2).CalcularArea());
        }

        [TestCase]
        public void TestCalcularPerimetroConUnRectangulo()
        {
            Assert.AreEqual(12m, new Rectangulo(4,2).CalcularPerimetro());
        }

        [TestCase]
        public void TestResumenListaVacia()
        {
            Assert.AreEqual("<h1>Lista vacía de formas!</h1>",
                FormaGeometrica.Imprimir(new List<FormaGeometrica>(), 1));
        }

        [TestCase]
        public void TestResumenListaVaciaFormasEnIngles()
        {
            Assert.AreEqual("<h1>Empty list of shapes!</h1>",
                FormaGeometrica.Imprimir(new List<FormaGeometrica>(), 2));
        }

        [TestCase]
        public void TestResumenListaVaciaFormasEnItaliano()
        {
            Assert.AreEqual("<h1>Elenco vuoto di forme!</h1>",
                FormaGeometrica.Imprimir(new List<FormaGeometrica>(), 3));
        }

        [TestCase]
        public void TestResumenListaConUnCuadrado()
        {
            var cuadrados = new List<FormaGeometrica> { new FormaGeometrica(new Cuadrado(5)) };

            var resumen = FormaGeometrica.Imprimir(cuadrados, FormaGeometrica.Castellano);

            Assert.AreEqual("<h1>Reporte de Formas</h1>1 Cuadrado | Area 25 | Perimetro 20 <br/>TOTAL:<br/>1 formas Perimetro 20 Area 25", resumen);
        }

        [TestCase]
        public void TestResumenListaConUnCuadradoEnIngles()
        {
            var cuadrados = new List<FormaGeometrica> { new FormaGeometrica(new Cuadrado(5)) };

            var resumen = FormaGeometrica.Imprimir(cuadrados, FormaGeometrica.Ingles);

            Assert.AreEqual("<h1>Shapes report</h1>1 Square | Area 25 | Perimeter 20 <br/>TOTAL:<br/>1 shapes Perimeter 20 Area 25", resumen);
        }

        [TestCase]
        public void TestResumenListaConRectanguloEnItaliano()
        {
            var cuadrados = new List<FormaGeometrica> { new FormaGeometrica(new Rectangulo(4,2)) };

            var resumen = FormaGeometrica.Imprimir(cuadrados, FormaGeometrica.Italiano);

            Assert.AreEqual("<h1>Rapporto sui moduli</h1>1 Rettangolo | La zona 8 | Perimetro 12 <br/>TOTAL:<br/>1 Forme Perimetro 12 La zona 8", resumen);
        }

        [TestCase]
        public void TestResumenListaConUnCuadradoEnItaliano()
        {
            var cuadrados = new List<FormaGeometrica> { new FormaGeometrica(new Cuadrado(5)) };

            var resumen = FormaGeometrica.Imprimir(cuadrados, FormaGeometrica.Italiano);

            Assert.AreEqual("<h1>Rapporto sui moduli</h1>1 Piazza | La zona 25 | Perimetro 20 <br/>TOTAL:<br/>1 Forme Perimetro 20 La zona 25", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasCuadrados()
        {
            var cuadrados = new List<FormaGeometrica>
            {
                new FormaGeometrica(new Cuadrado(5)),
                new FormaGeometrica(new Cuadrado(1)),
                new FormaGeometrica(new Cuadrado(3))
            };

            var resumen = FormaGeometrica.Imprimir(cuadrados, FormaGeometrica.Ingles);

            Assert.AreEqual("<h1>Shapes report</h1>3 Squares | Area 35 | Perimeter 36 <br/>TOTAL:<br/>3 shapes Perimeter 36 Area 35", resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTipos()
        {
            var formas = new List<FormaGeometrica>
            {
                new FormaGeometrica(new Cuadrado(5)),
                new FormaGeometrica(new Circulo(3)),
                new FormaGeometrica(new TrianguloEquilatero(4)),
                new FormaGeometrica(new Cuadrado(2)),
                new FormaGeometrica(new TrianguloEquilatero(9)),
                new FormaGeometrica(new Circulo(2.75m)),
                new FormaGeometrica(new TrianguloEquilatero(4.2m))
            };

            var resumen = FormaGeometrica.Imprimir(formas, FormaGeometrica.Ingles);

            Assert.AreEqual(
                "<h1>Shapes report</h1>2 Squares | Area 29 | Perimeter 28 <br/>2 Circles | Area 13,01 | Perimeter 18,06 <br/>3 Triangles | Area 49,64 | Perimeter 51,6 <br/>TOTAL:<br/>7 shapes Perimeter 97,66 Area 91,65",
                resumen);
        }

        [TestCase]
        public void TestResumenListaConMasTiposEnCastellano()
        {
            var formas = new List<FormaGeometrica>
            {
                new FormaGeometrica(new Cuadrado(5)),
                new FormaGeometrica(new Circulo(3)),
                new FormaGeometrica(new TrianguloEquilatero(4)),
                new FormaGeometrica(new Cuadrado(2)),
                new FormaGeometrica(new TrianguloEquilatero(9)),
                new FormaGeometrica(new Circulo(2.75m)),
                new FormaGeometrica(new TrianguloEquilatero(4.2m))
            };

            var resumen = FormaGeometrica.Imprimir(formas, FormaGeometrica.Castellano);

            Assert.AreEqual(
                "<h1>Reporte de Formas</h1>2 Cuadrados | Area 29 | Perimetro 28 <br/>2 Circulos | Area 13,01 | Perimetro 18,06 <br/>3 Triangulos | Area 49,64 | Perimetro 51,6 <br/>TOTAL:<br/>7 formas Perimetro 97,66 Area 91,65",
                resumen);
        }

        [TestCase]
        public void TestObtenerNombreDeFormaEnIngles()
        {
            Reporte rep = new Reporte("Ingles");
            Assert.AreEqual("Circle", rep.Formas("Circulo"));
        }

        [TestCase]
        public void TestObtenerNombreDeFormaEnItaliano()
        {
            Reporte rep = new Reporte("Italiano");
            Assert.AreEqual("Cerchio", rep.Formas("Circulo"));
        }

        [TestCase]
        public void TestObtenerNombreDeFormaEnCastellano()
        {
            Reporte rep = new Reporte("Castellano");
            Assert.AreEqual("Circulo", rep.Formas("Circulo"));
        }

        [TestCase]
        public void TestObtenerNombreDeFormaTrianguloEquilateroEnItaliano()
        {
            Reporte rep = new Reporte("Italiano");
            Assert.AreEqual("triangoli", rep.Formas("Triangulos_Equilateros"));
        }

        [TestCase]
        public void TestObtenerListaVaciaEnCastellanoDesdeReporte()
        {
            Reporte rep = new Reporte("Castellano");
            Assert.AreEqual("Lista vacía de formas!", rep.ListaVacia);
        }

        [TestCase]
        public void TestObtenerAreaEnItalianoDesdeReporte()
        {
            Reporte rep = new Reporte("Italiano");
            Assert.AreEqual("La zona", rep.Area);
        }

        [TestCase]
        public void TestObtenerPerimetroEnInglesDesdeReporte()
        {
            Reporte rep = new Reporte("Ingles");
            Assert.AreEqual("Perimeter", rep.Perimetro);
        }

        [TestCase]
        public void TestObtenerPerimetroEnItalianoDesdeReporte()
        {
            Reporte rep = new Reporte("Italiano");
            Assert.AreEqual("Perimetro", rep.Perimetro);
        }

    }
}
