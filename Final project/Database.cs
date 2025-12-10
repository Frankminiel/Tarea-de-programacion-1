using System.Data.SqlClient;

namespace Renta_de_juegos
{
    // ========================//
    //         DATABASE        //
    //=========================//
    public static class Database
    {
        public static string ConnectionString =
            "Server=DESKTOP-VRBJONV\\SQLEXPRESS01;Database=todos_podemos;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }

    // =========================
    //  ENTIDADES
    // =========================
    public class Cliente { public int Id; public string Nombre; public string Apellido; public int Edad; public string Telefono; }
    public class Rentals { public int Id; public int ClienteId; public int JuegoId; public DateTime Fecha; }
    public class Reservation { public int Id; public string ClienteNombre; public string Juego; public int Horas; public double Total; public DateTime FechaReserva; }
    public class Games { public int Id; public string Nombre; public bool Disponible; }
    // =========================
    //  REPOSITORIOS
    // =========================
    public class ClienteRepository
    {
        public void CrearCliente(Cliente c)
        {
            using var con = Database.GetConnection(); con.Open();
            string q = @"INSERT INTO Clientes (Nombre, Apellido, Edad, Telefono)
                     VALUES (@Nombre, @Apellido, @Edad, @Telefono)";
            using var cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
            cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
            cmd.Parameters.AddWithValue("@Edad", c.Edad);
            cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
            cmd.ExecuteNonQuery();
        }
        public void ListarClientes()
        {
            using var con = Database.GetConnection(); con.Open();
            using var cmd = new SqlCommand("SELECT Id, Nombre, Apellido, Edad, Telefono FROM Clientes ORDER BY Id", con);
            using var r = cmd.ExecuteReader();
            Console.WriteLine("\nRegistered clients:");
            while (r.Read())
            {
                Console.WriteLine($"- ID {r.GetInt32(0)} | {r.GetString(1)} {r.GetString(2)} | Age: {r.GetInt32(3)} | Phone: {r.GetString(4)}");
            }
        }
    }

    public class RentaRepository
    {
        public void CrearRenta(Rentals r)
        {
            using var con = Database.GetConnection(); con.Open();
            string q = @"INSERT INTO Rentas (Id, ClienteId, JuegoId, Fecha)
                     VALUES (@Id, @ClienteId, @JuegoId, @Fecha)";
            using var cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@Id", r.Id);
            cmd.Parameters.AddWithValue("@ClienteId", r.ClienteId);
            cmd.Parameters.AddWithValue("@JuegoId", r.JuegoId);
            cmd.Parameters.AddWithValue("@Fecha", r.Fecha);
            cmd.ExecuteNonQuery();
        }
        public void ListarRentas()
        {
            using var con = Database.GetConnection(); con.Open();
            using var cmd = new SqlCommand("SELECT Id, ClienteId, JuegoId, Fecha FROM Rentas ORDER BY Id", con);
            using var r = cmd.ExecuteReader();
            Console.WriteLine("\nCurrent rentals:");
            while (r.Read())
            {
                Console.WriteLine($"- Rental {r.GetInt32(0)} | ClientID: {r.GetInt32(1)} | GameID: {r.GetInt32(2)} | Date: {r.GetDateTime(3):yyyy-MM-dd HH:mm}");
            }
        }
    }

    public class ReservacionRepository
    {
        public void CrearReservacion(Reservation r)
        {
            using var con = Database.GetConnection(); con.Open();
            string q = @"INSERT INTO Reservaciones (ClienteNombre, Juego, Horas, Total, FechaReserva)
                     VALUES (@ClienteNombre, @Juego, @Horas, @Total, @FechaReserva)";
            using var cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@ClienteNombre", r.ClienteNombre);
            cmd.Parameters.AddWithValue("@Juego", r.Juego);
            cmd.Parameters.AddWithValue("@Horas", r.Horas);
            cmd.Parameters.AddWithValue("@Total", r.Total);
            cmd.Parameters.AddWithValue("@FechaReserva", r.FechaReserva);
            cmd.ExecuteNonQuery();
        }
        public void ListarReservaciones()
        {
            using var con = Database.GetConnection(); con.Open();
            using var cmd = new SqlCommand("SELECT Id, ClienteNombre, Juego, Horas, Total, FechaReserva FROM Reservaciones ORDER BY Id DESC", con);
            using var r = cmd.ExecuteReader();
            Console.WriteLine("\nReservations:");
            while (r.Read())
            {
                Console.WriteLine($"- Res {r.GetInt32(0)} | {r.GetString(1)} | Game: {r.GetString(2)} | Hours: {r.GetInt32(3)} | Total: ${r.GetDouble(4)} | Date: {r.GetDateTime(5):yyyy-MM-dd HH:mm}");
            }
        }
    }

    public class JuegoRepository : JuegoRepositoryBase
    {
    }

    public class JuegoRepositoryBase
    {
    }

    public class ClaseRepository
    {
        public void RegistrarClase(string clienteNombre, string juego, DateTime fechaClase)

        {
            using var con = Database.GetConnection();
            con.Open();

            string q = @"INSERT INTO Clases (ClienteNombre, Juego, FechaClase)
                     VALUES (@ClienteNombre, @Juego, @FechaClase)";

            using var cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@ClienteNombre", clienteNombre);
            cmd.Parameters.AddWithValue("@Juego", juego);
            cmd.Parameters.AddWithValue("@FechaClase", fechaClase);

            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine($"Filas insertadas: {rows}");
        }

        public void ListarClases()
        {
            using var con = Database.GetConnection();
            con.Open();

            string q = "SELECT Id, ClienteNombre, Juego, FechaClase FROM Clases ORDER BY Id DESC";
            using var cmd = new SqlCommand(q, con);
            using var r = cmd.ExecuteReader();

            Console.WriteLine("\nRegistered classes:");
            while (r.Read())
            {
                Console.WriteLine($"- ID {r.GetInt32(0)} | {r.GetString(1)} | {r.GetString(2)} | Date: {r.GetDateTime(3):yyyy-MM-dd HH:mm}");
            }
        }
    }
}