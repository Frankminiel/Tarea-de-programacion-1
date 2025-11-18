{
    Agenda miAgenda = new Agenda();
    Console.WriteLine("Mi Agenda Perrón");
    Console.WriteLine("Bienvenido a tu lista de contactes");

    bool running = true;
    while (running)
    {
        Console.WriteLine("\n1. Agregar Contacto");
        Console.WriteLine("2. Ver Contactos");
        Console.WriteLine("3. Buscar Contacto");
        Console.WriteLine("4. Modificar Contacto");
        Console.WriteLine("5. Eliminar Contacto");
        Console.WriteLine("6. Salir");

        int choice = PedirNumero("Elige una opción: ");

        switch (choice)
        {
            case 1:
                miAgenda.AgregarContacto();
                break;
            case 2:
                miAgenda.VerContactos();
                break;
            case 3:
                miAgenda.BuscarContacto();
                break;
            case 4:
                miAgenda.ModificarContacto();
                break;
            case 5:
                miAgenda.EliminarContacto();
                break;
            case 6:
                running = false;
                break;
            default:
                Console.WriteLine("Opción no válida");
                break;
        }
    }
}

// Método genérico para pedir un número válido
static int PedirNumero(string mensaje)
{
    int numero;
    while (true)
    {
        Console.Write(mensaje);
        string input = Console.ReadLine();
        if (int.TryParse(input, out numero))
            return numero;
        Console.WriteLine("Por favor, ingresa un número válido.");
    }
}


class Contacto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
    public string Direccion { get; set; }

    public Contacto(int id, string nombre, string telefono, string email, string direccion)
    {
        Id = id;
        Nombre = nombre;
        Telefono = telefono;
        Email = email;
        Direccion = direccion;
    }

    public void Mostrar()
    {
        Console.WriteLine($"ID: {Id} | Nombre: {Nombre} | Teléfono: {Telefono} | Email: {Email} | Dirección: {Direccion}");
    }
}

class Agenda
{
    private List<Contacto> contactos = new List<Contacto>();
    private int siguienteId = 1;

    public void AgregarContacto()
    {
        Console.WriteLine("\nVamos a agregar ese contacte que te trae loco.");
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();
        Console.Write("Teléfono: ");
        string telefono = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Dirección: ");
        string direccion = Console.ReadLine();

        Contacto nuevo = new Contacto(siguienteId, nombre, telefono, email, direccion);
        contactos.Add(nuevo);
        siguienteId++;
        Console.WriteLine("Contacto agregado con éxito.\n");
    }

    public void VerContactos()
    {
        if (contactos.Count == 0)
        {
            Console.WriteLine("No hay contactos aún.");
            return;
        }

        Console.WriteLine("\nLista de Contactos:");
        foreach (var c in contactos)
            c.Mostrar();
    }

    public void BuscarContacto()
    {
        int id = PedirId("\nIngrese ID del contacto a buscar: ");
        var contacto = contactos.FirstOrDefault(c => c.Id == id);
        if (contacto != null)
            contacto.Mostrar();
        else
            Console.WriteLine("Contacto no encontrado.");
    }

    public void ModificarContacto()
    {
        int id = PedirId("\nIngrese ID del contacto a modificar: ");
        var contacto = contactos.FirstOrDefault(c => c.Id == id);

        if (contacto != null)
        {
            Console.Write($"Nombre ({contacto.Nombre}): ");
            string nombre = Console.ReadLine();
            if (!string.IsNullOrEmpty(nombre)) contacto.Nombre = nombre;

            Console.Write($"Teléfono ({contacto.Telefono}): ");
            string telefono = Console.ReadLine();
            if (!string.IsNullOrEmpty(telefono)) contacto.Telefono = telefono;

            Console.Write($"Email ({contacto.Email}): ");
            string email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email)) contacto.Email = email;

            Console.Write($"Dirección ({contacto.Direccion}): ");
            string direccion = Console.ReadLine();
            if (!string.IsNullOrEmpty(direccion)) contacto.Direccion = direccion;

            Console.WriteLine("Contacto actualizado con éxito.");
        }
        else
            Console.WriteLine("Contacto no encontrado.");
    }

    public void EliminarContacto()
    {
        int id = PedirId("\nIngrese ID del contacto a eliminar: ");
        var contacto = contactos.FirstOrDefault(c => c.Id == id);

        if (contacto != null)
        {
            contactos.Remove(contacto);
            Console.WriteLine("Contacto eliminado con éxito.");
        }
        else
            Console.WriteLine("Contacto no encontrado.");
    }

    // Método interno para pedir ID válido
    private int PedirId(string mensaje)
    {
        int id;
        while (true)
        {
            Console.Write(mensaje);
            string input = Console.ReadLine();
            if (int.TryParse(input, out id))
                return id;
            Console.WriteLine("Por favor, ingresa un número válido.");
        }
    }
}