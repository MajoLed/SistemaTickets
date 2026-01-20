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

    private readonly string archivoTickets = "tickets.json";

    //Constructor
    public GestorTickets()
    {
        CargarTickets();
    }

    // ------------ MÉTODOS --------------

    // Crear un nuevo ticket
    public void CrearTicket(Ticket ticket)
    {
        ticket.ID = siguienteId++;
        ticket.FechaCreacion = DateTime.Now;
        ticket.TecnicoAsignado = "Sin asignar";
        ticket.Estado = "Abierto";

        tickets.Add(ticket);

        //Se llama al método de Guardar tickets
        GuardarTickets();
    }

    // Ver tickets de un técnico específico
    public List<Ticket> MostrarMisTickets(string nombreTecnico)
    {
        var misTickets = tickets.Where(t => t.TecnicoAsignado == nombreTecnico).ToList();

        if (misTickets.Count == 0)
        {
            Console.WriteLine($"\nNo tienes tickets asignados, {nombreTecnico}.");
            return misTickets;
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
        ticket.Estado = "En Progreso";
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
    //private void mostrarticket(ticket ticket)
    //{
    //    console.writeline($"\n--- ticket #{ticket.id} ---");
    //    console.writeline($"cliente: {ticket.asunto}");
    //    console.writeline($"problema: {ticket.descripcion}");
    //    console.writeline($"canal: {ticket.canalcontacto}");
    //    console.writeline($"estado: {ticket.estado}");
    //    console.writeline($"técnico: {ticket.tecnicoasignado}");
    //    console.writeline($"creado: {ticket.fechacreacion:dd/mm/yyyy hh:mm}");

    //    if (ticket.fechacierre != null)
    //        console.writeline($"cerrado: {ticket.fechacierre}");
    //}

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