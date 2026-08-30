import lang.stride.*;
import java.util.List;
import java.util.ArrayList;

public class Carrito
{
    private static final double PORCENTAJE_IVA = 0.12;
    private Cliente cliente;
    private List<DetalleCarrito> items;

    public Carrito(Cliente cliente)
    {
        this.cliente = cliente;
        this.items =  new  ArrayList < DetalleCarrito > ();
    }

    public Cliente getCliente()
    {
        return cliente;
    }

    public List<DetalleCarrito> getItems()
    {
        return items;
    }

    public boolean agregarItem(Producto producto, int cantidad)
    {
        if (producto == null || cantidad <= 0) {
            return false;
        }
        if ( ! producto.reducirStock(cantidad)) {
            Notificacion aviso =  new  Notificacion("Administrador", "Sin stock suficiente de: " + producto.getNombre(), "hoy");
            aviso.enviar();
            return false;
        }
        for (final DetalleCarrito detalle : items) {
            if (detalle.getProducto().getId() == producto.getId()) {
                detalle.setCantidad(detalle.getCantidad() + cantidad);
                return true;
            }
        }
        items.add( new  DetalleCarrito(producto, cantidad));
        return true;
    }

    public boolean eliminarItem(int idProducto)
    {
        int i = 0;
        while (i < items.size()) {
            DetalleCarrito detalle = items.get(i);
            if (detalle.getProducto().getId() == idProducto) {
                detalle.getProducto().devolverStock(detalle.getCantidad());
                items.remove(i);
                return true;
            }
            i = i + 1;
        }
        return false;
    }

    public boolean estaVacio()
    {
        return items.isEmpty();
    }

    public double calcularSubtotal()
    {
        double subtotal = 0;
        for (final DetalleCarrito detalle : items) {
            subtotal = subtotal + detalle.calcularSubtotal();
        }
        return subtotal;
    }

    public double calcularImpuestos()
    {
        return calcularSubtotal() * PORCENTAJE_IVA;
    }

    public double calcularTotal()
    {
        return calcularSubtotal() + calcularImpuestos();
    }

    public void vaciarCarrito()
    {
        items.clear();
    }

    public boolean finalizarCompra(Pago pago)
    {
        if (estaVacio()) {
            return false;
        }
        boolean aprobado = pago.procesarPago(calcularTotal());
        if (aprobado) {
            vaciarCarrito();
        }
        return aprobado;
    }
}
