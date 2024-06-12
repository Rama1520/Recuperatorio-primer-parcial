using System;
using System.Text;

namespace Parcial
{
    public class Libro : Documento
    {
        public int NumPaginas { get; }

        public string ISBN => NumNormalizado;

        public Libro(string titulo, string autor, int año, string isbn, string barcode, int numPaginas)
            : base(titulo, autor, año, isbn, barcode)
        {
            NumPaginas = numPaginas;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(base.ToString());
            sb.AppendLine($"ISBN: {ISBN}");
            sb.AppendLine($"Número de páginas: {NumPaginas}");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Libro libro)
            {
                return this == libro;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), NumPaginas);
        }
    }
}
