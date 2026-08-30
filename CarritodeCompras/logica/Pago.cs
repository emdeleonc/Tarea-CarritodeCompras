using System;

namespace CarritodeCompras.logica
{
    public class Pago
    {
        private string numeroTarjeta;
        private string titular;
        private string fechaExpiracion;
        private string cvv;

        public Pago(string numeroTarjeta, string titular, string fechaExpiracion, string cvv)
        {
            this.numeroTarjeta = numeroTarjeta;
            this.titular = titular;
            this.fechaExpiracion = fechaExpiracion;
            this.cvv = cvv;
        }

        public bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(numeroTarjeta) || numeroTarjeta.Replace(" ", "").Length != 16)
                return false;
            if (string.IsNullOrWhiteSpace(titular))
                return false;
            if (string.IsNullOrWhiteSpace(cvv) || (cvv.Length != 3 && cvv.Length != 4))
                return false;
            if (string.IsNullOrWhiteSpace(fechaExpiracion) || !fechaExpiracion.Contains("/"))
                return false;

            return true;
        }

        public bool ProcesarPago(double monto)
        {
            if (!ValidarDatos())
            {
                return false;
            }
            if (monto <= 0)
            {
                return false;
            }
            return true;
        }

        public string NumeroEnmascarado()
        {
            string limpio = numeroTarjeta.Replace(" ", "");
            return "**** **** **** " + limpio.Substring(limpio.Length - 4);
        }
    }
}
