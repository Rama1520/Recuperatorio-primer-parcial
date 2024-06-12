using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parcial
{
    public static class Informes
    {
        public static void MostrarDistribuidos(Escaner escaner, out int extension, out int cantidad, out string resumen)
        {
            MostrarInformes(escaner, Estado.Distribuido, out extension, out cantidad, out resumen);
        }

        public static void MostrarEnEscaner(Escaner escaner, out int extension, out int cantidad, out string resumen)
        {
            MostrarInformes(escaner, Estado.EnEscaner, out extension, out cantidad, out resumen);
        }

        public static void MostrarEnRevision(Escaner escaner, out int extension, out int cantidad, out string resumen)
        {
            MostrarInformes(escaner, Estado.EnRevision, out extension, out cantidad, out resumen);
        }

        public static void MostrarTerminados(Escaner escaner, out int extension, out int cantidad, out string resumen)
        {
            MostrarInformes(escaner, Estado.Terminado, out extension, out cantidad, out resumen);
        }

        private static void MostrarInformes(Escaner escaner, Estado estado, out int extension, out int cantidad, out string resumen)
        {
            var documentosEnEstado = escaner.Documentos.Where(d => d.Estado == estado).ToList();
            extension = documentosEnEstado.Sum(d =>
            {
                if (d is Libro libro) return libro.NumPaginas;
                if (d is Mapa mapa) return mapa.Superficie;
                return 0;
            });

            cantidad = documentosEnEstado.Count;
            StringBuilder sb = new StringBuilder();
            documentosEnEstado.ForEach(d => sb.AppendLine(d.ToString()));
            resumen = sb.ToString();
        }
    }
}
