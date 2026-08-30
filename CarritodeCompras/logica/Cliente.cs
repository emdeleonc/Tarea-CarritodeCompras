using System;

namespace CarritodeCompras.logica
{
    public class Cliente
    {
        private int id;
        private string nombre;
        private string email;
        private bool esInvitado;

        public Cliente(int id, string nombre, string email)
        {
            this.id = id;
            this.nombre = nombre;
            this.email = email;
            this.esInvitado = false;
        }

        public Cliente()
        {
            this.id = 0;
            this.nombre = "Invitado";
            this.email = "";
            this.esInvitado = true;
        }

        public int Id { get { return id; } }
        public string Nombre { get { return nombre; } }
        public string Email { get { return email; } }
        public bool EsInvitado { get { return esInvitado; } }

        public override string ToString()
        {
            return esInvitado ? "Cliente invitado" : $"Cliente #{id}: {nombre} ({email})";
        }
    }
}
