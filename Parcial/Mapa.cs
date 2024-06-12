using System.Text;

namespace Parcial
{
    public class Mapa : Documento
    {
        public int Ancho { get; }
        public int Alto { get; }

        public int Superficie => Ancho * Alto;

        public Mapa(string titulo, string autor, int año, string numNormalizado, string barcode, int ancho, int alto)
            : base(titulo, autor, año, numNormalizado, barcode)
        {
            Ancho = ancho;
            Alto = alto;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            sb.AppendLine($"Ancho: {Ancho}");
            sb.AppendLine($"Alto: {Alto}");
            sb.AppendLine($"Superficie: {Superficie}");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Mapa mapa)
            {
                return this == mapa;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Ancho, Alto);
        }
    }
}
