
public class Pago
{
    private String numeroTarjeta;
    private String titular;
    private String fechaExpiracion; 
    private String cvv;

    public Pago(String numeroTarjeta, String titular, String fechaExpiracion, String cvv)
    {
        this.numeroTarjeta = numeroTarjeta;
        this.titular = titular;
        this.fechaExpiracion = fechaExpiracion;
        this.cvv = cvv;
    }

    public boolean validarDatos()
    {
        if (numeroTarjeta == null || numeroTarjeta.replace(" ", "").length() != 16) {
            return false;
        }
        if (titular == null || titular.trim().isEmpty()) {
            return false;
        }
        if (cvv == null || (cvv.length() != 3 && cvv.length() != 4)) {
            return false;
        }
        if (fechaExpiracion == null || !fechaExpiracion.contains("/")) {
            return false;
        }
        return true;
    }

    public boolean procesarPago(double monto)
    {
        if (!validarDatos() || monto <= 0) {
            return false;
        }
        return true;
    }

    public String numeroEnmascarado()
    {
        String limpio = numeroTarjeta.replace(" ", "");
        return "**** **** **** " + limpio.substring(limpio.length() - 4);
    }
}