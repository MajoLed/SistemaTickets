using System;

class Program
{
    static void Main(string[] args)
    {
        GestorTickets gestor = new GestorTickets();
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
                    CrearNuevoTicket(gestor);
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

    //------------- MÉTODOS --------------------
    static void CrearNuevoTicket(GestorTickets gestor)
    {
        Console.WriteLine("\n=== CREAR NUEVO TICKET ===");

        Console.Write("Nombre del cliente: ");
        string cliente = Console.ReadLine();

        Console.Write("Descripción del problema: ");
        string descripcion = Console.ReadLine();

        gestor.MostrarCanales();
        Console.Write("Escribe el número de la opción: ");

        //Mini lógica 
        if (int.TryParse(Console.ReadLine(), out int opcionCanal))
        {
            String canal = gestor.ObtenerCanal(opcionCanal);
            gestor.CrearTicket(cliente, descripcion, canal);
        }
        else
        {
            Console.WriteLine("\n✗ Opción inválida");
        }

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
            Console.WriteLine("\n✗ Número inválido");
        }
    }
}
