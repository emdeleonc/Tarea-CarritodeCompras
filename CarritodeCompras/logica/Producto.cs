using System;

namespace CarritodeCompras.logica
{
    public class Producto
    {
        private int id;
        private string nombre;
        private double precio;
        private int stock;

        public Producto(int id, string nombre, double precio, int stock)
        {
            this.id = id;
            this.nombre = nombre;
            this.precio = precio;
            this.stock = stock;
        }

        public int Id { get { return id; } }
        public string Nombre { get { return nombre; } }
        public double Precio { get { return precio; } }
        public int Stock { get { return stock; } }

        public bool ReducirStock(int cantidad)
        {
            if (cantidad <= 0 || cantidad > stock)
            {
                return false;
            }
            stock -= cantidad;
            return true;
        }

        public void DevolverStock(int cantidad)
        {
            stock += cantidad;
        }

        public override string ToString()
        {
            return $"[{id}] {nombre} - Q{precio:F2} (Stock: {stock})";
        }
    }
}
