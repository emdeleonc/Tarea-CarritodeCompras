using System;
using System.Collections.Generic;
using System.Linq;

namespace CarritodeCompras.logica
{
    public class Carrito
    {
        private const double PORCENTAJE_IVA = 0.12;

        private Cliente cliente;
        private List<DetalleCarrito> items;

        public Carrito(Cliente cliente)
        {
            this.cliente = cliente;
            items = new List<DetalleCarrito>();
        }

        public Cliente Cliente { get { return cliente; } }
        public List<DetalleCarrito> Items { get { return items; } }
        public bool AgregarItem(Producto producto, int cantidad)
        {
            if (producto == null || cantidad <= 0)
            {
                return false;
            }
            if (!producto.ReducirStock(cantidad))
            {
                var avisoAdmin = new Notificacion("Administrador", $"Sin stock suficiente de: {producto.Nombre}");
                avisoAdmin.Enviar();
                var avisoCliente = new Notificacion("Cliente", $"El producto '{producto.Nombre}' no tiene stock disponible.");
                avisoCliente.Enviar();
                return false;
            }

            var existente = items.FirstOrDefault(d => d.Producto.Id == producto.Id);
            if (existente != null)
            {
                items.Remove(existente);
                items.Add(new DetalleCarrito(producto, existente.Cantidad + cantidad));
            }
            else
            {
                items.Add(new DetalleCarrito(producto, cantidad));
            }
            return true;
        }

        public bool EliminarItem(int idProducto)
        {
            var item = items.FirstOrDefault(d => d.Producto.Id == idProducto);
            if (item == null)
            {
                return false;
            }
            item.Producto.DevolverStock(item.Cantidad);
            items.Remove(item);
            return true;
        }

        public double CalcularSubtotal()
        {
            return items.Sum(d => d.CalcularSubtotal());
        }

        public double CalcularImpuestos()
        {
            return CalcularSubtotal() * PORCENTAJE_IVA;
        }

        public double CalcularTotal()
        {
            return CalcularSubtotal() + CalcularImpuestos();
        }

        public bool EstaVacio()
        {
            return items.Count == 0;
        }

        public void VaciarCarrito()
        {
            items.Clear();
        }

        public bool FinalizarCompra(Pago pago)
        {
            if (EstaVacio() || pago == null)
            {
                return false;
            }
            bool aprobado = pago.ProcesarPago(CalcularTotal());
            if (aprobado)
            {
                VaciarCarrito();
            }
            return aprobado;
        }
    }
}
