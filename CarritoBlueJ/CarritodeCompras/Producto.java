public class Producto
{
    private int id;
    private String nombre;
    private double precio;
    private int stock;

    public Producto(int id, String nombre, double precio, int stock)
    {
        this.id = id;
        this.nombre = nombre;
        this.precio = precio;
        this.stock = stock;
    }

    public int getId()
    {
        return id;
    }

    public String getNombre()
    {
        return nombre;
    }

    public double getPrecio()
    {
        return precio;
    }

    public int getStock()
    {
        return stock;
    }

    public boolean tieneStock(int cantidad)
    {
        return cantidad > 0 && cantidad <= stock;
    }

    public boolean reducirStock(int cantidad)
    {
        if (!tieneStock(cantidad)) {
            return false;
        }
        stock -= cantidad;
        return true;
    }

    public void devolverStock(int cantidad)
    {
        stock += cantidad;
    }

    public String toString()
    {
        return "[" + id + "] " + nombre + " - Q" + precio + " (stock: " + stock + ")";
    }
}