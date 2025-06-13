using System.Text;
using tags;
Console.WriteLine("Ingrese el path del archivo MP3:");
string path = Console.ReadLine();

if(!File.Exists(path)){
    Console.WriteLine("El archivo no existe.");
}

FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        if (fs.Length < 128)
        {
            Console.WriteLine("El archivo es demasiado pequeño para contener un tag ID3v1.");
        }

        fs.Seek(-128, SeekOrigin.End); // Posicionarse 128 bytes antes del final
        using BinaryReader br = new BinaryReader(fs);

        byte[] tagBytes = br.ReadBytes(128);

        string header = Encoding.ASCII.GetString(tagBytes, 0, 3);
        if (header != "TAG")
        {
            Console.WriteLine("El archivo no contiene un tag ID3v1.");
        }

        Encoding latin1 = Encoding.GetEncoding("latin1");

        Id3v1Tag tag = new Id3v1Tag
        {
            Titulo = latin1.GetString(tagBytes, 3, 30).TrimEnd('\0', ' '),
            Artista = latin1.GetString(tagBytes, 33, 30).TrimEnd('\0', ' '),
            Album = latin1.GetString(tagBytes, 63, 30).TrimEnd('\0', ' '),
            Anio = latin1.GetString(tagBytes, 93, 4).TrimEnd('\0', ' ')
        };

Console.WriteLine("Título:  " + tag.Titulo);
Console.WriteLine("Artista: " + tag.Artista);
Console.WriteLine("Álbum:   " + tag.Album);
Console.WriteLine("Año:     " + tag.Anio);
