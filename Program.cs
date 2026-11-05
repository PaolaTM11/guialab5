using System;
using System.IO;

int opcion = 0;

while (opcion != 7)
{
    Console.WriteLine("1. Agregar contacto");
    Console.WriteLine("2. Mostrar contactos");
    Console.WriteLine("3. Buscar contacto");
    Console.WriteLine("4. Contar contactos");
    Console.WriteLine("5. Guardar en archivo binario");
    Console.WriteLine("6. Leer archivo binario");
    Console.WriteLine("7. Salir");
    opcion = int.Parse(Console.ReadLine());

    switch (opcion)
    {
        case 1:
            AgregarContacto();
            break;
        case 2:
            MostrarContactos();
            break;
         case 3:
            BuscarContacto();
            break;
        case 4:
            ContarContactos();
            break;
        case 5:
            GuardarBinario();
            break;
        case 6:
            LeerBinario();
            break;
    }
}
static void AgregarContacto()
{
    string id = "";
    bool valido = false;

    while (!valido)
    {
        Console.WriteLine("ID (4 números):");
        id = Console.ReadLine();

        if (!IdValido(id))
        {
            Console.WriteLine("El ID debe tener exactamente 4 números");
        }
        else if (IdExiste(id))
        {
            Console.WriteLine("Ese ID ya existe");
        }
        else
        {
            valido = true;
        }
    }

    Console.WriteLine("Nombre:");
    string nombre = Console.ReadLine();

    string telefono = "";
    valido = false;

    while (!valido)
    {
        Console.WriteLine("Teléfono (9 números):");
        telefono = Console.ReadLine();

        if (!TelefonoValido(telefono))
        {
            Console.WriteLine("El teléfono debe tener exactamente 9 números");
        }
        else
        {
            valido = true;
        }
    }

    using (var archivo = new StreamWriter("contactos.txt", true))
    {
        archivo.WriteLine($"{id},{nombre},{telefono}");
    }
    Console.WriteLine("Contacto guardado");
}

static void MostrarContactos()
{
    if (!File.Exists("contactos.txt"))
    {
        Console.WriteLine("No hay contactos");
        return;
    }

    using (var archivo = new StreamReader("contactos.txt"))
    {
        string linea = archivo.ReadLine();
        while (linea != null)
        {
            Console.WriteLine(linea);
            linea = archivo.ReadLine();
        }
    }
}

static void BuscarContacto()
{
    if (!File.Exists("contactos.txt"))
    {
        Console.WriteLine("No hay contactos");
        return;
    }

    Console.WriteLine("Nombre a buscar:");
    string buscado = Console.ReadLine();
    bool encontrado = false;

    using (var archivo = new StreamReader("contactos.txt"))
    {
        string linea = archivo.ReadLine();
        while (linea != null)
        {
            string[] partes = linea.Split(',');
            if (partes[1].ToLower().Contains(buscado.ToLower()))
            {
                Console.WriteLine(linea);
                encontrado = true;
            }
            linea = archivo.ReadLine();
        }
    }

    if (!encontrado)
    {
        Console.WriteLine("No se encontró");
    }
}
static void ContarContactos()
{
    int cantidad = 0;

    using (var archivo = new StreamReader("contactos.txt"))
    {
        string linea = archivo.ReadLine();
        while (linea != null)
        {
            if (linea.Trim() != "") cantidad++;
            linea = archivo.ReadLine();
        }
    }
    Console.WriteLine($"Total de contactos: {cantidad}");
}
static void GuardarBinario()
{
    using (var lector = new StreamReader("contactos.txt"))
    using (var archivo = new BinaryWriter(File.Open("contactos.bin", FileMode.Create)))
    {
        string linea = lector.ReadLine();
        while (linea != null)
        {
            string[] partes = linea.Split(',');
            archivo.Write(int.Parse(partes[0]));
            archivo.Write(partes[1]);
            archivo.Write(partes[2]);
            linea = lector.ReadLine();
        }
    }
    Console.WriteLine("Archivo binario creado");
}
static void LeerBinario()
{
    if (!File.Exists("contactos.bin"))
    {
        Console.WriteLine("Primero guarde el archivo binario");
        return;
    }

    using (var archivo = new BinaryReader(File.OpenRead("contactos.bin")))
    {
        while (archivo.BaseStream.Position < archivo.BaseStream.Length)
        {
            int id = archivo.ReadInt32();
            string nombre = archivo.ReadString();
            string telefono = archivo.ReadString();
            Console.WriteLine($"{id} - {nombre} - {telefono}");
        }
    }
}
static bool IdValido(string id)
{
    if (id.Length != 4)
    {
        return false;
    }

    for (int i = 0; i < id.Length; i++)
    {
        if (!char.IsDigit(id[i]))
        {
            return false;
        }
    }
    return true;
}

static bool IdExiste(string id)
{
    if (!File.Exists("contactos.txt"))
    {
        return false;
    }

    using (var archivo = new StreamReader("contactos.txt"))
    {
        string linea = archivo.ReadLine();
        while (linea != null)
        {
            string[] partes = linea.Split(',');
            if (partes[0] == id)
            {
                return true;
            }
            linea = archivo.ReadLine();
        }
    }
    return false;
}
static bool TelefonoValido(string telefono)
{
    if (telefono.Length != 9)
    {
        return false;
    }

    for (int i = 0; i < telefono.Length; i++)
    {
        if (!char.IsDigit(telefono[i]))
        {
            return false;
        }
    }
    return true;
}