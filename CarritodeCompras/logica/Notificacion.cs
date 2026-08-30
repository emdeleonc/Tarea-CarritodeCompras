using System;

namespace CarritodeCompras.logica
{
    public class Notificacion
    {
        private string destinatario;
        private string mensaje;
        private DateTime fecha;

        public Notificacion(string destinatario, string mensaje)
        {
            this.destinatario = destinatario;
            this.mensaje = mensaje;
            this.fecha = DateTime.Now;
        }

        public string Destinatario { get { return destinatario; } }
        public string Mensaje { get { return mensaje; } }

        public void Enviar()
        {
            Console.WriteLine($"[Notificación para {destinatario} - {fecha:dd/MM/yyyy HH:mm}] {mensaje}");
        }
    }
}
