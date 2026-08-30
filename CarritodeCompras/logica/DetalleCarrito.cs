using System;

namespace CarritodeCompras.logica
{
    public class DetalleCarrito
    {
        private Producto producto;
        private int cantidad;

        public DetalleCarrito(Producto producto, int cantidad)
        {
            this.producto = producto;
            this.cantidad = cantidad;
        }

        public Producto Producto { get { return producto; } }
        public int Cantidad { get { return cantidad; } }

        public double CalcularSubtotal()
        {
            return producto.Precio * cantidad;
        }

        public override string ToString()
        {
            return $"{producto.Nombre} x{cantidad} = Q{CalcularSubtotal():F2}";
        }
    }
}
