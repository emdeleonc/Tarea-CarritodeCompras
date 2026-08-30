
public class Notificacion
{
    private String destinatario;
    private String mensaje;
    private String fecha;

    public Notificacion(String destinatario, String mensaje, String fecha)
    {
        this.destinatario = destinatario;
        this.mensaje = mensaje;
        this.fecha = fecha;
    }

    public String getDestinatario()
    {
        return destinatario;
    }

    public String getMensaje()
    {
        return mensaje;
    }

    public void enviar()
    {
        System.out.println("[Notificación para " + destinatario + " - " + fecha + "] " + mensaje);
    }

    public String toString()
    {
        return "Para " + destinatario + ": " + mensaje;
    }
}