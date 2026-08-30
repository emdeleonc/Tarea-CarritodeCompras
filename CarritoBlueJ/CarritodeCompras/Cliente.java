
public class Cliente
{
    private int id;
    private String nombre;
    private String email;
    private boolean esInvitado;

    public Cliente(int id, String nombre, String email)
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

    public int getId()
    {
        return id;
    }

    public String getNombre()
    {
        return nombre;
    }

    public String getEmail()
    {
        return email;
    }

    public boolean esInvitado()
    {
        return esInvitado;
    }

    public String toString()
    {
        if (esInvitado) {
            return "Cliente invitado (sin registro)";
        }
        return "Cliente #" + id + ": " + nombre + " (" + email + ")";
    }
}