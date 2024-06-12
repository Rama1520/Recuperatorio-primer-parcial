using System;
using System.Collections.Generic;

namespace Parcial
{
    public class Escaner
    {
        public enum TipoDoc { libro, mapa }

        private List<Documento> documentos;

        public string Locacion { get; }
        public IReadOnlyList<Documento> Documentos => documentos.AsReadOnly();

        public Escaner(string marca, TipoDoc tipoDoc)
        {
            documentos = new List<Documento>();
            Locacion = tipoDoc == TipoDoc.libro ? "procesosTecnicos" : "mapoteca";
        }

        public static bool operator ==(Escaner escaner, Documento documento)
        {
            if ((documento is Libro && escaner.Locacion != "procesosTecnicos") ||
                (documento is Mapa && escaner.Locacion != "mapoteca"))
            {
                throw new TipoIncorrectoException("Este escáner no acepta este tipo de documento", new Exception());
            }

            return escaner.documentos.Contains(documento);
        }

        public static bool operator !=(Escaner escaner, Documento documento)
        {
            return !(escaner == documento);
        }

        public static bool operator +(Escaner escaner, Documento documento)
        {
            try
            {
                if ((documento is Libro && escaner.Locacion != "procesosTecnicos") ||
                    (documento is Mapa && escaner.Locacion != "mapoteca"))
                {
                    throw new TipoIncorrectoException("Este escáner no acepta este tipo de documento", new Exception());
                }

                if (escaner == documento)
                {
                    return false; // Documento duplicado
                }

                if (documento.Estado != Estado.Inicio)
                {
                    return false; // Documento no está en estado inicial
                }

                documento.AvanzarEstado();
                escaner.documentos.Add(documento);
                return true;
            }
            catch (TipoIncorrectoException ex)
            {
                throw new Exception("El documento no se pudo añadir a la lista", ex);
            }
        }

        public void CambiarEstadoDocumento(Documento documento)
        {
            documento.AvanzarEstado();
        }

        public override bool Equals(object obj)
        {
            if (obj is Escaner escaner)
            {
                return this.Locacion == escaner.Locacion && this.documentos.Equals(escaner.documentos);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Locacion, documentos);
        }
    }

    public class TipoIncorrectoException : Exception
    {
        public TipoIncorrectoException(string message, Exception innerException)
            : base(message, innerException) { }

        public override string ToString()
        {
            return $"Excepción en el método {this.TargetSite.Name} de la clase {this.TargetSite.DeclaringType.Name}. Algo salió mal, revisa los detalles. Detalles: {this.InnerException.Message}";
        }
    }
}
