using System;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace seeker{
   class Program{
      static void Main(string[] args){
         //CREA UN PATH HACIA EL DESKTOP
         string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

         //CREA UN ARCHIVO DE SALIDA
         createFile(path + "\\seeker\\Result.txt");

         //ABRE LOS DOCUMENTOS DEL PATH SELECCIONADO
         openFiles(path);
         Console.Read();
      }

      //ABRE LOS ARCHIVOS DEL PATH
      public static void openFiles(string path){
         //GUARDA EL PATH DE LOS ARCHIVOS EN UN ARRAY
         string[] filePaths = Directory.GetFiles(path + "\\seeker\\files");

         //INICIA UN CRONOMETRO
         Stopwatch watch = Stopwatch.StartNew();

         //ABRE UNO POR UNO LOS ARCHIVOS DEL DIRECTORIO
         foreach(string filePath in filePaths){
            openFile(path, filePath);
         }
         
         watch.Stop();
         string value = "\nTiempo total en abrir los archivos: " + watch.Elapsed;
         addText(path, value);
         Console.WriteLine(value);
      }

      public static void openFile(string originPath, string filePath){
         Stopwatch watch = Stopwatch.StartNew();
         FileStream fs = File.OpenRead(filePath);
         fs.Close();
         watch.Stop();
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         addText(originPath, value);
         Console.WriteLine(value);
      }

      public static void createFile(string filePath){
         if (File.Exists(filePath)){
            File.Delete(filePath);
         }
         FileStream fs = File.Create(filePath);
         fs.Close();
      }


      public static void addText(string filePath, string value)  
      {  
        FileStream fs = new FileStream(filePath  + "\\seeker\\Result.txt", FileMode.Append);
        byte[] bdata = Encoding.Default.GetBytes(value + "\n");
        fs.Write(bdata, 0, bdata.Length);
        fs.Close();
      }  

     
   }
}
