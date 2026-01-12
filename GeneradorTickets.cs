using System;

public class Ticket
{
    public int ID { get; set; }
    public string Asunto { get; set; }
    public string Descripcion { get; set; }
    public string Estado { get; set; }  // "Nuevo", "EnProceso", "Cerrado"
    public string CanalContacto { get; set; } 
    public DateTime FechaCreacion { get; set; }
    public Nullable<DateTime> FechaCierre { get; set; }
    public string TecnicoAsignado { get; set; }
}
public class Usuario
{
    public int ID { get; set; }
    public string NombreCliente { get; set; }
    public string Apellido { get; set; }

}