using System;
using System.Text;

namespace Parcial
{
    public abstract class Documento
    {
        public string Titulo { get; }
        public string Autor { get; }
        public int Año { get; }
        protected string NumNormalizado { get; }
        public string Barcode { get; }
        public Estado Estado { get; private set; }

        protected Documento(string titulo, string autor, int año, string numNormalizado, string barcode)
        {
            Titulo = titulo;
            Autor = autor;
            Año = año;
            NumNormalizado = numNormalizado;
            Barcode = barcode;
            Estado = Estado.Inicio;
        }

        public bool AvanzarEstado()
        {
            if (Estado == Estado.Terminado)
                return false;
            Estado++;
            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Título: {Titulo}");
            sb.AppendLine($"Autor: {Autor}");
            sb.AppendLine($"Año: {Año}");
            sb.AppendLine($"Estado: {Estado}");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Documento doc)
            {
                return this == doc;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Titulo, Autor, Año, NumNormalizado, Barcode);
        }

        public static bool operator ==(Documento doc1, Documento doc2)
        {
            if (ReferenceEquals(doc1, doc2)) return true;
            if (ReferenceEquals(doc1, null) || ReferenceEquals(doc2, null)) return false;

            if (doc1.GetType() != doc2.GetType()) return false;

            if (doc1.Barcode == doc2.Barcode) return true;

            if (doc1 is Libro libro1 && doc2 is Libro libro2)
            {
                return libro1.ISBN == libro2.ISBN || (libro1.Titulo == libro2.Titulo && libro1.Autor == libro2.Autor);
            }

            if (doc1 is Mapa mapa1 && doc2 is Mapa mapa2)
            {
                return mapa1.Titulo == mapa2.Titulo && mapa1.Autor == mapa2.Autor && mapa1.Año == mapa2.Año && mapa1.Superficie == mapa2.Superficie;
            }

            return false;
        }

        public static bool operator !=(Documento doc1, Documento doc2)
        {
            return !(doc1 == doc2);
        }
    }

    public enum Estado
    {
        Inicio,
        Distribuido,
        EnEscaner,
        EnRevision,
        Terminado
    }
}
