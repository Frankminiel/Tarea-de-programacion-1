    {        Console.WriteLine("Bienvenido a mi lista de Contactos");

        //names, lastnames, addresses, telephones, emails, ages, bestfriend
        bool running = true;
        List<int> ids = new List<int>();
        Dictionary<int, string> names = new Dictionary<int, string>();
        Dictionary<int, string> lastnames = new Dictionary<int, string>();
        Dictionary<int, string> addresses = new Dictionary<int, string>();
        Dictionary<int, string> telephones = new Dictionary<int, string>();
        Dictionary<int, string> emails = new Dictionary<int, string>();
        Dictionary<int, int> ages = new Dictionary<int, int>();
        Dictionary<int, bool> bestFriends = new Dictionary<int, bool>();

        while (running)
        {
            Console.WriteLine("\n------------------------------------------------------------------------------------------------");
            Console.WriteLine(@"1. Agregar Contacto      2. Ver Contactos    3. Buscar Contactos     4. Modificar Contacto   5. Eliminar Contacto    6. Salir");
            Console.WriteLine("Digite el número de la opción deseada");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            if (int.TryParse(Console.ReadLine(), out int typeOption))
            {
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
                        running = false;
                        Console.WriteLine("¡Gracias por usar la lista de contactos! Adiós.");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, digite un número del 1 al 6.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, digite un número.");
            }
        }
    }

    // Método de utilidad para validar campos de texto no vacíos
    static string GetNonEmptyInput(string prompt)
    {
        string input;
        do
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Este campo no puede estar vacío. Por favor, intente de nuevo.");
            }
        } while (string.IsNullOrEmpty(input));
        return input;
    }

    // Método de utilidad para validar la edad
    static int GetValidAge()
    {
        int age;
        const int MAX_AGE = 110;

        while (true)
        {
            Console.WriteLine($"Digite la edad de la persona en números (máximo {MAX_AGE})");
            if (int.TryParse(Console.ReadLine(), out age))
            {
                if (age > 0 && age <= MAX_AGE)
                {
                    return age;
                }
                else
                {
                    Console.WriteLine($"Edad inválida. Debe ser un número entre 1 y {MAX_AGE}. Por favor, intente de nuevo.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Por favor, digite un número entero.");
            }
        }
    }

    // Método para agregar contactos (MODIFICADO CON VALIDACIÓN)
    static void AddContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
        Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails,
        Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
    {
        Console.WriteLine("\n--- Agregar Contacto ---");
        // Se usa GetNonEmptyInput para asegurar que los campos no estén vacíos
        string name = GetNonEmptyInput("Digite el nombre de la persona");
        string lastname = GetNonEmptyInput("Digite el apellido de la persona");
        string address = GetNonEmptyInput("Digite la dirección");
        string phone = GetNonEmptyInput("Digite el teléfono de la persona");
        string email = GetNonEmptyInput("Digite el email de la persona");

        // Se usa GetValidAge para asegurar que la edad sea válida (máximo 110)
        int age = GetValidAge();

        bool isBestFriend;
        while (true)
        {
            Console.WriteLine("Especifique si es mejor amigo: 1. Sí, 2. No");
            if (int.TryParse(Console.ReadLine(), out int friendOption))
            {
                if (friendOption == 1)
                {
                    isBestFriend = true;
                    break;
                }
                else if (friendOption == 2)
                {
                    isBestFriend = false;
                    break;
                }
            }
            Console.WriteLine("Opción inválida. Por favor, digite 1 para Sí o 2 para No.");
        }

        var id = ids.Count > 0 ? ids.Max() + 1 : 1; // Generar ID único y consecutivo
        ids.Add(id);
        names.Add(id, name);
        lastnames.Add(id, lastname);
        addresses.Add(id, address);
        telephones.Add(id, phone);
        emails.Add(id, email);
        ages.Add(id, age);
        bestFriends.Add(id, isBestFriend);

        Console.WriteLine("\n ¡Contacto agregado correctamente!");
    }

    // Método para mostrar todos los contactos
    static void ShowContacts(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
        Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails,
        Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
    {
        Console.WriteLine("\n--- Lista de Contactos ---");
        if (ids.Count == 0)
        {
            Console.WriteLine("No hay contactos para mostrar.");
            return;
        }

        // Formato para mejor visualización
        Console.WriteLine("{0,-4} {1,-15} {2,-15} {3,-15} {4,-15} {5,-25} {6,-5} {7,-10}",
            "ID", "Nombre", "Apellido", "Teléfono", "Email", "Dirección", "Edad", "Mejor Amigo");
        Console.WriteLine(new string('-', 112)); // Línea separadora

        foreach (var id in ids)
        {
            var isBestFriend = bestFriends[id];
            string isBestFriendStr = isBestFriend ? "Sí" : "No";

            Console.WriteLine("{0,-4} {1,-15} {2,-15} {3,-15} {4,-15} {5,-25} {6,-5} {7,-10}",
                id, names[id], lastnames[id], telephones[id], emails[id], addresses[id], ages[id], isBestFriendStr);
        }
    }

    // Método para buscar contactos
    static void SearchContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
        Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails,
        Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
    {
        Console.WriteLine("\n--- Buscar Contacto ---");
        Console.WriteLine("Digite el nombre, apellido, teléfono o email del contacto a buscar:");
        string criteria = Console.ReadLine()?.ToLower() ?? string.Empty;

        if (string.IsNullOrEmpty(criteria))
        {
            Console.WriteLine("Criterio de búsqueda no puede estar vacío.");
            return;
        }

        bool found = false;
        foreach (var id in ids)
        {
            if (names[id].ToLower().Contains(criteria) || lastnames[id].ToLower().Contains(criteria) ||
                telephones[id].ToLower().Contains(criteria) || emails[id].ToLower().Contains(criteria))
            {
                if (!found) // Encabezado solo si se encuentra el primero
                {
                    Console.WriteLine("\nResultados de la búsqueda:");
                    Console.WriteLine(new string('-', 80));
                    found = true;
                }
                var isBestFriend = bestFriends[id];
                string isBestFriendStr = isBestFriend ? "Sí" : "No";

                Console.WriteLine($"ID: {id} | Nombre: {names[id]} {lastnames[id]} | Teléfono: {telephones[id]} | Email: {emails[id]} | Edad: {ages[id]} años | Mejor amigo: {isBestFriendStr}");
            }
        }

        if (!found)
            Console.WriteLine("X No se encontraron contactos con ese criterio.");
    }

    // Método para modificar contactos (MODIFICADO CON VALIDACIÓN)
    static void ModifyContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
        Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails,
        Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
    {
        Console.WriteLine("\n--- Modificar Contacto ---");
        Console.WriteLine("Digite el ID del contacto a modificar:");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido. Por favor, digite un número.");
            return;
        }

        if (!ids.Contains(id))
        {
            Console.WriteLine("❌ Contacto no encontrado.");
            return;
        }

        Console.WriteLine("¿Qué desea modificar? (nombre, apellido, direccion, telefono, email, edad, amigo)");
        string option = Console.ReadLine()?.ToLower() ?? string.Empty;

        switch (option)
        {
            case "nombre":
                names[id] = GetNonEmptyInput("Nuevo nombre:");
                break;
            case "apellido":
                lastnames[id] = GetNonEmptyInput("Nuevo apellido:");
                break;
            case "direccion":
                addresses[id] = GetNonEmptyInput("Nueva dirección:");
                break;
            case "telefono":
                telephones[id] = GetNonEmptyInput("Nuevo teléfono:");
                break;
            case "email":
                emails[id] = GetNonEmptyInput("Nuevo email:");
                break;
            case "edad":
                ages[id] = GetValidAge(); // Usa el método con validación de edad
                break;
            case "amigo":
                while (true)
                {
                    Console.WriteLine("¿Es mejor amigo? 1. Sí, 2. No");
                    if (int.TryParse(Console.ReadLine(), out int friendOption))
                    {
                        if (friendOption == 1 || friendOption == 2)
                        {
                            bestFriends[id] = friendOption == 1;
                            break;
                        }
                    }
                    Console.WriteLine("Opción inválida. Por favor, digite 1 para Sí o 2 para No.");
                }
                break;
            default:
                Console.WriteLine("Opción de modificación inválida.");
                return;
        }

        Console.WriteLine("\n ¡Contacto modificado correctamente!");
    }

    // Método para eliminar contactos
    static void DeleteContact(List<int> ids, Dictionary<int, string> names, Dictionary<int, string> lastnames,
        Dictionary<int, string> addresses, Dictionary<int, string> telephones, Dictionary<int, string> emails,
        Dictionary<int, int> ages, Dictionary<int, bool> bestFriends)
    {
        Console.WriteLine("\n--- Eliminar Contacto ---");
        Console.WriteLine("Digite el ID del contacto a eliminar:");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido. Por favor, digite un número.");
            return;
        }

        if (!ids.Contains(id))
        {
            Console.WriteLine("X Contacto no encontrado.");
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

        Console.WriteLine("\n ¡Contacto eliminado correctamente!");
    }
