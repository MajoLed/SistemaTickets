using System;
using System.Net.Sockets;

class ProgramTickets
{
    static GestorTickets gestor = new GestorTickets();

    static private List<string> canalesContacto = new List<string> {
        "Correo",
        "Presencial",
        "WhatsApp",
        "Teams"
        };
    static void Main(string[] args)
    {
        string nombreTecnico = "";

        // Pedir nombre del técnico al inicio
        Console.WriteLine("=== SISTEMA DE TICKETS ===");
        Console.Write("Ingresa por favor el nombre del técnico: ");
        nombreTecnico = Console.ReadLine();

        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("\n========== MENÚ PRINCIPAL ==========");
            Console.WriteLine("1. Crear nuevo ticket");
            Console.WriteLine("2. Ver todos los tickets");
            Console.WriteLine("3. Ver mis tickets");
            Console.WriteLine("4. Asignarme un ticket");
            Console.WriteLine("5. Cerrar un ticket");
            Console.WriteLine("0. Salir");
            Console.Write("\nElige una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CrearNuevoTicket();
                    break;
                case "2":
                    gestor.MostrarTodosLosTickets();
                    break;
                case "3":
                    gestor.MostrarMisTickets(nombreTecnico);
                    break;
                case "4":
                    AsignarTicket(gestor, nombreTecnico);
                    break;
                case "5":
                    CerrarTicket(gestor);
                    break;
                case "0":
                    continuar = false;
                    Console.WriteLine("\n¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("\nError: Opción inválida, vuelva a intentar");
                    break;
            }

            if (continuar)
            {
                Console.WriteLine("\nPresiona ENTER para continuar...");
                Console.ReadLine();
            }
        }
    }

    static void CrearNuevoTicket() // que la función no llame a gestor
    {
        Console.WriteLine("\n=== CREAR NUEVO TICKET ===");

        Ticket ticket = new Ticket();

        //asignar asunto
        Console.WriteLine("Indique el Asunto: ");
        ticket.Asunto = Console.ReadLine();
        
        //asignar descripcion
        Console.WriteLine("Indique la descripción: ");
        ticket.Descripcion = Console.ReadLine();

        //mostrar canales
        ticket.CanalContacto = ValidarCanal(canalesContacto);
        

        Console.WriteLine($"\n✓ Ticket #{ticket.ID} creado exitosamente");

        gestor.CrearTicket(ticket);

    }
    private void MostrarTicket(Ticket ticket)
    {
        Console.WriteLine($"\n--- Ticket #{ticket.ID} ---");
        Console.WriteLine($"Asunto: {ticket.Asunto}");
        Console.WriteLine($"Problema: {ticket.Descripcion}");
        Console.WriteLine($"Canal: {ticket.CanalContacto}");
        Console.WriteLine($"Estado: {ticket.Estado}");
        Console.WriteLine($"Técnico: {ticket.TecnicoAsignado}");
        Console.WriteLine($"Creado: {ticket.FechaCreacion:dd/MM/yyyy HH:mm}");

        if (ticket.FechaCierre != null)
            Console.WriteLine($"Cerrado: {ticket.FechaCierre}");
    }

    static void AsignarTicket(GestorTickets gestor, string nombreTecnico)
    {
        Console.Write("\nIngresa el número de ticket a asignarte: ");
        if (int.TryParse(Console.ReadLine(), out int ticketId))
        {
            gestor.AsignarTicket(ticketId, nombreTecnico);
        }
        else
        {
            Console.WriteLine("\n Número inválido");
        }
    }

    static void CerrarTicket(GestorTickets gestor)
    {
        Console.Write("\nIngresa el número de ticket a cerrar: ");
        if (int.TryParse(Console.ReadLine(), out int ticketId))
        {
            gestor.CerrarTicket(ticketId);
        }
        else
        {
            Console.WriteLine("\nNúmero inválido");
        }
    }

    static string ValidarCanal(List<String> listaCanales)
    {
        Boolean entradaValida = false;
        int opcion = 0;

        do
        {
            //Mostrar canales
            for (int i = 0; i < canalesContacto.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {canalesContacto[i]}");
            }

            Console.WriteLine("\nSeleccione el canal de contacto: ");

            //lee y valida rango de entrada usuario
            if (int.TryParse(Console.ReadLine(), out opcion) && (opcion >= 1 && opcion <= canalesContacto.Count))
            {
                entradaValida = true;
            }
            else
            {
                Console.WriteLine("Opción no válida");
            }
        }
        while (!entradaValida);

        return (canalesContacto[opcion - 1]);

    }
}