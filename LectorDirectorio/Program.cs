Console.WriteLine("Ingrese el path de un directorio:");
string path;
path = Console.ReadLine();
bool existe = false;

do
{
    if (Directory.Exists(path))
    {
        Console.WriteLine("Existe el directorio.");
        string[] archivos = Directory.GetFiles(path);
        string[] carpetas = Directory.GetDirectories(path);
        Console.WriteLine("\nCARPETAS:");
        foreach (string carpeta in carpetas)
        {
            Console.WriteLine(Path.GetFileName(carpeta));
        }
        Console.WriteLine("\nARCHIVOS:");
        List<string> lineas = new List<string>();
        lineas.Add("Nombre archivo,Tamaño (KB),Fecha de Última Modificación");
        foreach (string archivo in archivos)
        {
            System.IO.FileInfo archivoInfo = new FileInfo(archivo);
            string nombreArchivo = archivoInfo.Name;
            double tamanoKb = (double)archivoInfo.Length / 1024;
            string fechaModificacion = archivoInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

            Console.WriteLine($"{nombreArchivo} - {tamanoKb} KB");
            lineas.Add($"{nombreArchivo}, {tamanoKb}, {fechaModificacion}");
        }

        string rutaCsv = Path.Combine(path, "reporte_archivos.csv");
        File.WriteAllLines(rutaCsv, lineas);
        existe = true;
    }else
    {
        Console.WriteLine("NO existe el directorio.");
        Console.WriteLine("Ingrese el path de un directorio:");
        path = Console.ReadLine();

    }
} while (existe != true);
