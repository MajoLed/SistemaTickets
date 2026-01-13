using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.IO; // Para el guardado de archivos
using System.Text.Json; //
public class GestorTickets
{
    private List<Ticket> tickets = new List<Ticket>();
    private int siguienteId = 1;

    private List<string> canalesContacto = new List<string>
    {
        "Correo",
        "Presencial",
        "WhatsApp"
    };

    private readonly string archivoTickets = "tickets.json";

    //Constructor
    public GestorTickets()
    {
        CargarTickets();

    }

    // ------------ MÉTODOS --------------

    // Crear un nuevo ticket
    public void CrearTicket(string asunto, string descripcion, string canal)
    {
        var ticket = new Ticket
        {
            ID = siguienteId++,
            Asunto = asunto,
            Descripcion = descripcion,
            Estado = "Abierto",
            FechaCreacion = DateTime.Now,
            TecnicoAsignado = "Sin asignar"
        };

        tickets.Add(ticket);
        Console.WriteLine($"\n✓ Ticket #{ticket.ID} creado exitosamente");

        //Se llama al método de Guardar tickets
        GuardarTickets();
    }

    public void MostrarCanales()
    {
        Console.WriteLine("\nSeleccione el canal de conectacto: ");
        for (int i = 0; i < canalesContacto.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {canalesContacto[i]}");
        }

    }
    public string ObtenerCanal(int indice)
    {
        if (indice >= 1 && indice <= canalesContacto.Count)
            return canalesContacto[indice - 1];

        return "Desconocido";
    }

    // Ver todos los tickets
    public void MostrarTodosLosTickets()
    {
        if (tickets.Count == 0)
        {
            Console.WriteLine("\nNo hay tickets registrados.");
            return;
        }

        Console.WriteLine("\n=== TODOS LOS TICKETS ===");

        foreach (var ticket in tickets)
        {
            MostrarTicket(ticket);
        }
    }

    // Ver tickets de un técnico específico
    public void MostrarMisTickets(string nombreTecnico)
    {
        var misTickets = tickets.Where(t => t.TecnicoAsignado == nombreTecnico).ToList();

        if (misTickets.Count == 0)
        {
            Console.WriteLine($"\nNo tienes tickets asignados, {nombreTecnico}.");
            return;
        }

        Console.WriteLine($"\n=== MIS TICKETS ({nombreTecnico}) ===");
        foreach (var ticket in misTickets)
        {
            MostrarTicket(ticket);
        }
    }

    // Asignar un ticket a un técnico
    public void AsignarTicket(int ticketId, string nombreTecnico)
    {
        var ticket = tickets.FirstOrDefault(t => t.ID == ticketId);

        if (ticket == null)
        {
            Console.WriteLine($"\n✗ No existe el ticket #{ticketId}");
            return;
        }

        ticket.TecnicoAsignado = nombreTecnico;
        ticket.Estado = "EnProceso";
        Console.WriteLine($"\n✓ Ticket #{ticketId} asignado a {nombreTecnico}");

        GuardarTickets();
    }

    // Cerrar un ticket
    public void CerrarTicket(int ticketId)
    {
        var ticket = tickets.FirstOrDefault(t => t.ID == ticketId);

        if (ticket == null)
        {
            Console.WriteLine($"\nNo existe el ticket #{ticketId}");
            return;
        }

        ticket.Estado = "Cerrado";
        ticket.FechaCierre = DateTime.Now;
        Console.WriteLine($"\n✓ Ticket #{ticketId} cerrado");

        GuardarTickets();
    }

    // Método auxiliar para mostrar un ticket
    private void MostrarTicket(Ticket ticket)
    {
        Console.WriteLine($"\n--- Ticket #{ticket.ID} ---");
        Console.WriteLine($"Cliente: {ticket.Asunto}");
        Console.WriteLine($"Problema: {ticket.Descripcion}");
        Console.WriteLine($"Canal: {ticket.CanalContacto}");
        Console.WriteLine($"Estado: {ticket.Estado}");
        Console.WriteLine($"Técnico: {ticket.TecnicoAsignado}");
        Console.WriteLine($"Creado: {ticket.FechaCreacion:dd/MM/yyyy HH:mm}");

        if (ticket.FechaCierre != null)
            Console.WriteLine($"Cerrado: {ticket.FechaCierre}");
    }


    // ------------ MÉTODOS JSON --------------
    private void CargarTickets()
    {
        try
        {
            if (File.Exists(archivoTickets))
            {
                string json = File.ReadAllText(archivoTickets);
                tickets = JsonSerializer.Deserialize<List<Ticket>>(json) ?? new List<Ticket>();
    
                if (tickets.Count > 0)
                {
                    siguienteId = tickets.Max(t => t.ID) + 1;
                }

                Console.WriteLine($"Se cargaron {tickets.Count} tickets desde el archivo.");
            }
            else
            {
                Console.WriteLine("No se encontró el archivo. Se creará uno nuevo.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar tickets: {ex.Message}");
            tickets = new List<Ticket>();
        }
    }

    private void GuardarTickets()
    {
        try
        {
            var opciones = new JsonSerializerOptions
            {
                WriteIndented = true 
            };

            string json = JsonSerializer.Serialize(tickets, opciones);
            File.WriteAllText(archivoTickets, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar el ticket: {ex.Message}");
        }
    }

}