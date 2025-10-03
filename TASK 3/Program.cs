{
        Console.WriteLine("Bienvenido a mi lista de Contactes");

        //names, lastnames, addresses, telephones, emails, ages, bestfriend
        bool runing = true;
        List<int> ids = new List<int>();
        Dictionary<int, string> names = new Dictionary<int, string>();
        Dictionary<int, string> lastnames = new Dictionary<int, string>();
        Dictionary<int, string> addresses = new Dictionary<int, string>();
        Dictionary<int, string> telephones = new Dictionary<int, string>();
        Dictionary<int, string> emails = new Dictionary<int, string>();
        Dictionary<int, int> ages = new Dictionary<int, int>();
        Dictionary<int, bool> bestFriends = new Dictionary<int, bool>();

        while (runing)
        {
            Console.WriteLine(@"1. Agregar Contacto     2. Ver Contactos    3. Buscar Contactos     4. Modificar Contacto   5. Eliminar Contacto    6. Salir");
            Console.WriteLine("Digite el número de la opción deseada");

            int typeOption = Convert.ToInt32(Console.ReadLine());

            switch (typeOption)
            {
                case 1:
                    AddContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
                    break;
                case 2: // ver contactos
                    ShowContacts(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
                    break;
                case 3: // buscar
                    SearchContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
                    break;
                case 4: // modificar
                    ModifyContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
                    break;
                case 5: // eliminar
                    DeleteContact(ids, names, lastnames, addresses, telephones, emails, ages, bestFriends);
                    break;
                case 6:
                    runing = false;
                    break;
                default:
                    Console.WriteLine("Tu eres o te haces el idiota?");
                    break;
            }
        }
    }

    // Método para agregar contactos
    static void AddContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
        Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails,
        Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
    {
        Console.WriteLine("Digite el nombre de la persona");
        string name = Console.ReadLine();
        Console.WriteLine("Digite el apellido de la persona");
        string lastname = Console.ReadLine();
        Console.WriteLine("Digite la dirección");
        string address = Console.ReadLine();
        Console.WriteLine("Digite el telefono de la persona");
        string phone = Console.ReadLine();
        Console.WriteLine("Digite el email de la persona");
        string email = Console.ReadLine();
        Console.WriteLine("Digite la edad de la persona en números");
        int age = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Especifique si es mejor amigo: 1. Si, 2. No");
        bool isBestFriend = Convert.ToInt32(Console.ReadLine()) == 1;

        var id = ids.Count + 1;
        ids.Add(id);
        names.Add(id, name);
        lastnames.Add(id, lastname);
        addresses.Add(id, address);
        telephones.Add(id, phone);
        emails.Add(id, email);
        ages.Add(id, age);
        bestFriends.Add(id, isBestFriend);

        Console.WriteLine("Contacto agregado correctamente!");
    }

    // Método para mostrar todos los contactos
    static void ShowContacts(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
        Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails,
        Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
    {
        if (ids.Count == 0)
        {
            Console.WriteLine("No hay contactos para mostrar.");
            return;
        }

        Console.WriteLine($"ID  Nombre    Apellido    Dirección    Telefono    Email    Edad    Mejor Amigo?");
        Console.WriteLine("------------------------------------------------------------------------------------------------");
        foreach (var id in ids)
        {
            var isBestFriend = bestFriends[id];
            string isBestFriendStr = (isBestFriend == true) ? "Si" : "No";

            Console.WriteLine($"{id}  {names[id]}  {lastnames[id]}  {addresses[id]}  {telephones[id]}  {emails[id]}  {ages[id]}  {isBestFriendStr}");
        }
    }

    // Método para buscar contactos
    static void SearchContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
        Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails,
        Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
    {
        Console.WriteLine("Digite el nombre, apellido, telefono o email del contacto a buscar:");
        string criteria = Console.ReadLine().ToLower();

        bool found = false;
        foreach (var id in ids)
        {
            if (names[id].ToLower().Contains(criteria) || lastnames[id].ToLower().Contains(criteria) ||
                telephones[id].ToLower().Contains(criteria) || emails[id].ToLower().Contains(criteria))
            {
                var isBestFriend = bestFriends[id];
                string isBestFriendStr = (isBestFriend == true) ? "Si" : "No";

                Console.WriteLine($"{id} - {names[id]} {lastnames[id]} - {telephones[id]} - {emails[id]} - {ages[id]} años - Mejor amigo: {isBestFriendStr}");
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No se encontraron contactos con ese criterio.");
    }

    // Método para modificar contactos
    static void ModifyContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
        Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails,
        Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
    {
        Console.WriteLine("Digite el ID del contacto a modificar:");
        int id = Convert.ToInt32(Console.ReadLine());

        if (!ids.Contains(id))
        {
            Console.WriteLine("Contacto no encontrado.");
            return;
        }

        Console.WriteLine("¿Qué desea modificar? (nombre, apellido, direccion, telefono, email, edad, amigo)");
        string option = Console.ReadLine().ToLower();

        switch (option)
        {
            case "nombre":
                Console.WriteLine("Nuevo nombre:");
                names[id] = Console.ReadLine();
                break;
            case "apellido":
                Console.WriteLine("Nuevo apellido:");
                lastnames[id] = Console.ReadLine();
                break;
            case "direccion":
                Console.WriteLine("Nueva dirección:");
                addresses[id] = Console.ReadLine();
                break;
            case "telefono":
                Console.WriteLine("Nuevo telefono:");
                telephones[id] = Console.ReadLine();
                break;
            case "email":
                Console.WriteLine("Nuevo email:");
                emails[id] = Console.ReadLine();
                break;
            case "edad":
                Console.WriteLine("Nueva edad:");
                ages[id] = Convert.ToInt32(Console.ReadLine());
                break;
            case "amigo":
                Console.WriteLine("¿Es mejor amigo? 1. Sí, 2. No");
                bestFriends[id] = Convert.ToInt32(Console.ReadLine()) == 1;
                break;
            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Console.WriteLine("Contacto modificado correctamente!");
    }

    // Método para eliminar contactos
    static void DeleteContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
        Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails,
        Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
    {
        Console.WriteLine("Digite el ID del contacto a eliminar:");
        int id = Convert.ToInt32(Console.ReadLine());

        if (!ids.Contains(id))
        {
            Console.WriteLine("Contacto no encontrado.");
            return;
        }

        // Eliminar el contacto de todas las colecciones
        ids.Remove(id);
        names.Remove(id);
        lastnames.Remove(id);
        addresses.Remove(id);
        telephones.Remove(id);
        emails.Remove(id);
        ages.Remove(id);
        bestFriends.Remove(id);

        Console.WriteLine("Contacto eliminado correctamente!");
    }

