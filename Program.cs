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
    }
}
static void AgregarContacto()
{
    Console.WriteLine("ID:");
    string id = Console.ReadLine();
    Console.WriteLine("Nombre:");
    string nombre = Console.ReadLine();
    Console.WriteLine("Teléfono:");
    string telefono = Console.ReadLine();

    using (var archivo = new StreamWriter("contactos.txt", true))
    {
        archivo.WriteLine($"{id},{nombre},{telefono}");
    }
    Console.WriteLine("Contacto guardado");
}