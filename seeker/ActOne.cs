using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using ConsoleTables;

public class ActOne{
   public void executeProgram(string path){
      //EMPIEZA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      Console.WriteLine("Actividad 1 en proceso... ");

      //TABLA PARA GUARDAR LOS DATOS DE MANERA ORDENADA
      var table = new ConsoleTable("Archivo", "Tiempo");
         
      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string[] filePaths = Directory.GetFiles(path + "\\files");
         
      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      Directory.CreateDirectory(path + "\\results\\act1");
      createFile(path + "\\results\\act1\\results.txt");

      //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
      using (var progress = new ProgressBar()) {
         progress.setTask("Abriendo archivos");
         int count = 0;
         foreach(string filePath in filePaths){
            count++;
            //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
            table.AddRow(new DirectoryInfo(filePath).Name , openFile(path, filePath));
            
            progress.Report((double) count / filePaths.Length);
         }
         Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Abriendo archivos");
      }
         
      //TERMINA EL CRONOMETRO
      watch.Stop();

      //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
      writeOnFile(path, table.ToMinimalString());
      writeOnFile(path, "Tiempo total en ejecutar el programa: " + watch.Elapsed);
      Console.WriteLine("Actividad 1 completada exitosamente, Noice\n");
   }

   public string openFile(string path, string filePath){
      //EMPIEZA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();

      //LEE TODO EL ARCHIVO
      File.ReadAllText(filePath);

      //TERMINA EL CRONOMETRO
      watch.Stop();
      return watch.Elapsed.ToString();
   }

   //METODO PARA ESCRIBIR INFORMACION EN UN ARCHIVO TXT
   public void writeOnFile(string path, string value){
      FileStream fs = new FileStream(path + "\\results\\act1\\results.txt", FileMode.Append);
      byte[] bdata = Encoding.Default.GetBytes(value + "\n");
      fs.Write(bdata, 0, bdata.Length);
      fs.Close();
   }

   //CREA UN NUEVO ARCHIVO TXT
   public static void createFile(string path){
      if (File.Exists(path)){
         File.Delete(path);
      }
      FileStream fs = File.Create(path);
      fs.Close();
   }
}

