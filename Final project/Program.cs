using Renta_de_juegos;
using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

// ========================//
//         DATABASE        //
//=========================//
public static class Database
{
    public static string ConnectionString =
("Server=DESKTOP-VRBJONV\\SQLEXPRESS01;Database=todos_podemos;Trusted_Connection=True;TrustServerCertificate=True;");

    [Obsolete]
    public static SqlConnection GetConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}

// =========================
//  ENTIDADES
// =========================

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public int Edad { get; set; }
    public string Telefono { get; set; }
}

public class Rentals
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int JuegoId { get; set; }
    public DateTime Fecha { get; set; }
}

public class Reservation
{
    public string ClienteNombre { get; set; }
    public string Juego { get; set; }
    public int Horas { get; set; }
    public double Total { get; set; }
    public DateTime FechaReserva { get; set; }
}


public class Games
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public bool Disponible { get; set; }
}


// =========================
//  REPOSITORIOS
// =========================

public class ClienteRepository
{
    public void CrearCliente(Cliente cliente)
    {
        using var conexion = Database.GetConnection();
        conexion.Open();

        string query = @"INSERT INTO Clientes (Id, Nombre, Apellido, Edad, Telefono)
                         VALUES (@Id, @Nombre, @Apellido, @Edad, @Telefono)";

        using var cmd = new SqlCommand(query, conexion);

        cmd.Parameters.AddWithValue("@Id", cliente.Id);
        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
        cmd.Parameters.AddWithValue("@Edad", cliente.Edad);
        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);

        cmd.ExecuteNonQuery();
    }

    internal void ListarClientes()
    {
        throw new NotImplementedException();
    }
}

public class RentaRepository
{
    [Obsolete]
    public void CrearRenta(Rentals renta)
    {
        using var conexion = Database.GetConnection();
        conexion.Open();

        string query = @"INSERT INTO Reservaciones (ClienteNombre, Juego, Horas, Total, FechaReserva)";

        using var cmd = new SqlCommand(query, conexion);

        cmd.Parameters.AddWithValue("@Id", renta.Id);
        cmd.Parameters.AddWithValue("@ClienteId", renta.ClienteId);
        cmd.Parameters.AddWithValue("@JuegoId", renta.JuegoId);
        cmd.Parameters.AddWithValue("@Fecha", renta.Fecha);

        cmd.ExecuteNonQuery();
    }

    internal void ListarRentas()
    {
        throw new NotImplementedException();
    }
}

public class ReservacionRepository
{
    public void CrearReservacion(Reservation r)
    {
        using var conexion = Database.GetConnection();
        conexion.Open();

        string query = @"INSERT INTO Reservaciones (ClienteNombre, Juego, Horas, Total, FechaReserva)
                         VALUES (@ClienteNombre, @Juego, @Horas, @Total, @FechaReserva)";

        using var cmd = new SqlCommand(query, conexion);

        cmd.Parameters.AddWithValue("@ClienteNombre", r.ClienteNombre);
        cmd.Parameters.AddWithValue("@Juego", r.Juego);
        cmd.Parameters.AddWithValue("@Horas", r.Horas);
        cmd.Parameters.AddWithValue("@Total", r.Total);
        cmd.Parameters.AddWithValue("@FechaReserva", r.FechaReserva);

        cmd.ExecuteNonQuery();
    }

    internal void ListarReservaciones()
    {
        throw new NotImplementedException();
    }
}

public class JuegoRepository
{
    public void CrearJuego(Games j)
    {
        using var conexion = Database.GetConnection();
        conexion.Open();

        string query = @"INSERT INTO Juegos (Id, Nombre, Disponible)
                         VALUES (@Id, @Nombre, @Disponible)";

        using var cmd = new SqlCommand(query, conexion);

        cmd.Parameters.AddWithValue("@Id", j.Id);
        cmd.Parameters.AddWithValue("@Nombre", j.Nombre);
        cmd.Parameters.AddWithValue("@Disponible", j.Disponible);

        cmd.ExecuteNonQuery();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==============================================");
        Console.WriteLine("                 WE ALL CAN");
        Console.WriteLine("==============================================");
        Console.ResetColor();

        Console.WriteLine("Welcome to WE ALL CAN.");
        Console.WriteLine("A place where we all can play");
        Console.WriteLine("Choose an option from the menu to get started.");
        Console.WriteLine("==============================================");

        bool running = true;

        while (running)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nMain Menu:");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("1. Challenge a game");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("2. Game Classes");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("3. Rentals");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("4. Clients");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("5. Exit");
            Console.ResetColor();

            Console.Write("\nEnter your option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid number.");
                Console.ResetColor();
                continue;
            }

            try
            {
                switch (choice)
                {
                    case 1: RentGame(); break;
                    case 2: GameClasses(); break;
                    case 3: Reservations(); break;
                    case 4: RegisterClient(); break;
                    case 5:
                        running = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Thank you for using WE ALL CAN!");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Only options 1 to 5 are available.");
                        Console.ResetColor();
                        break;
                }
            }
            catch (SqlException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Database error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Console.ResetColor();
            }
        }
    }

    // ==============================
    //       REGISTER CLIENT
    // ==============================
    static void RegisterClient()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n--- CLIENT REGISTRATION ---");
        Console.ResetColor();

        Console.Write("Enter ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid ID.");
            Console.ResetColor();
            return;
        }

        Console.Write("Enter Name: ");
        string nombre = Console.ReadLine();
        Console.Write("Enter Last Name: ");
        string apellido = Console.ReadLine();
        Console.Write("Enter Age: ");
        if (!int.TryParse(Console.ReadLine(), out int edad))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid age.");
            Console.ResetColor();
            return;
        }

        Console.Write("Enter Phone: ");
        string telefono = Console.ReadLine();

        var repo = new ClienteRepository();
        repo.CrearCliente(new Cliente { Id = id, Nombre = nombre, Apellido = apellido, Edad = edad, Telefono = telefono });

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Client {nombre} registered successfully in the database.");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nListing clients:");
        Console.ResetColor();
        repo.ListarClientes();
    }

    // ==============================
    //       RENT GAME
    // ==============================
    static void RentGame()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n--- GAME RENTAL SERVICE ---");
        Console.ResetColor();

        Console.Write("Enter rental ID: ");
        if (!int.TryParse(Console.ReadLine(), out int rentaId))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid rental ID.");
            Console.ResetColor();
            return;
        }

        Console.Write("Enter client ID: ");
        if (!int.TryParse(Console.ReadLine(), out int clienteId))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid client ID.");
            Console.ResetColor();
            return;
        }

        Console.Write("Enter game ID: ");
        if (!int.TryParse(Console.ReadLine(), out int juegoId))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid game ID.");
            Console.ResetColor();
            return;
        }

        var repo = new RentaRepository();
        repo.CrearRenta(new Rentals { Id = rentaId, ClienteId = clienteId, JuegoId = juegoId, Fecha = DateTime.Now });

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Game {juegoId} rented by client {clienteId}.");
        Console.WriteLine("You only have 15 minutes to pick up the game.");
        Console.WriteLine("Enjoy your game!");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nListing rentals:");
        Console.ResetColor();
        repo.ListarRentas();
    }

    // ==============================
    //       GAME CLASSES
    // ==============================
    static void GameClasses()
    {
        
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n--- GAME CLASSES ---");
            Console.ResetColor();

            Console.WriteLine("1 - Introduction to Chess");
            Console.WriteLine("2 - Chess Strategies");
            Console.WriteLine("3 - Monopoly Tactics");
            Console.WriteLine("4 - Ludo Basics");
            Console.WriteLine("5 - Dominoes Techniques");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nSelect a class to reserve:");
            Console.ResetColor();

            Console.Write("Enter option (1-5): ");
            if (!int.TryParse(Console.ReadLine(), out int option) || option < 1 || option > 5)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option.");
                Console.ResetColor();
                return;
            }

            string juego = option switch
            {
                1 => "Introduction to Chess",
                2 => "Chess Strategies",
                3 => "Monopoly Tactics",
                4 => "Ludo Basics",
                5 => "Dominoes Techniques",
                _ => "Unknown"
            };

            Console.Write("Enter client's name: ");
            string clienteNombre = Console.ReadLine();

            DateTime fechaClase = DateTime.Now;

            var repo = new ClaseRepository();
            repo.RegistrarClase(clienteNombre, juego, fechaClase);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nClass '{juego}' reserved successfully for {clienteNombre}.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nListing registered classes:");
            Console.ResetColor();
            repo.ListarClases();
        }
    

    // ==============================
    //       RESERVATIONS
    // ==============================
    static void Reservations()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n--- GAME RESERVATIONS ---");
        Console.ResetColor();

        Console.Write("Enter the client's name: ");
        string client = Console.ReadLine();

        if (!Regex.IsMatch(client ?? "", @"^[a-zA-Z\s]+$"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid name. Please use letters only.");
            Console.ResetColor();
            return;
        }

        Console.WriteLine("Available games: 1. Chess  2. Monopoly  3. Ludo  4. Dominoes  5. Uno");
        Console.Write("Select a game: ");
        string game = Console.ReadLine();

        Console.Write("Enter the number of hours: ");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int hours) || hours <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please enter a positive number.");
            Console.ResetColor();
            return;
        }

        double total = hours * 500;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Total to pay: ${total}");
        Console.ResetColor();

        Console.WriteLine("1. Yes  2. No");
        string res = Console.ReadLine();

        if (res == "2")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Reservation cancelled.");
            Console.ResetColor();
            return;
        }

        var repo = new ReservacionRepository();
        repo.CrearReservacion(new Reservation { ClienteNombre = client, Juego = game, Horas = hours, Total = total, FechaReserva = DateTime.Now });

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Reservation successful!");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nListing reservations:");
        Console.ResetColor();
        repo.ListarReservaciones();
    }
}
