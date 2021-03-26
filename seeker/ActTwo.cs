using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleTables;

public class ActTwo{
      //EJECUTA EL PROGRAMA
   public void executeProgram(string path){
      //INICIA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      Console.WriteLine("Actividad 2 en proceso... ");

      //TABLA PARA GUARDAR LOS DATOS DE MANERA ORDENADA
      var table = new ConsoleTable("Archivo", "Tiempo");

      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string[] filePaths = Directory.GetFiles(path + "\\files");
         
      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      Directory.CreateDirectory(path + "\\results\\act2");
         
      //CREA UN DIRECTORIO PARA LOS NUEVOS ARCHIVOS HTML
      Directory.CreateDirectory(path + "\\results\\act2\\files");
      
      using (var progress = new ProgressBar()) {
         progress.setTask("Eliminando tags");
         int count = 0;
         foreach(string filePath in filePaths){
            count++;
            //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
            table.AddRow(new DirectoryInfo(filePath).Name , openFile(path, filePath));
            
            progress.Report((double) count / filePaths.Length);
         }
         Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Eliminando tags");
      }

      //TERMINA EL CRONOMETRO
      watch.Stop();

      //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
      File.WriteAllText(path + "\\results\\act2\\results.txt", table.ToMinimalString());
      File.WriteAllText(path + "\\results\\act2\\results.txt", "Tiempo total en ejecutar el programa: " + watch.Elapsed);
      Console.WriteLine("Actividad 2 completada exitosamente, Noice\n");         
   }

   public string openFile(string path, string filePath){
      //EMPIEZA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      
      //lEE EL ARCHIVO
      string text = File.ReadAllText(filePath);

      //REEMPLAZA TODOS LOS TAGS HTML CON UN STRING VACIO
      string changeText = Regex.Replace(text, @"<(.|\n)+?>", string.Empty);
      
      //ESCRIBE UN NUEVO DOCUMENTO SIN TAGS DE HTML
      File.WriteAllText(path + "\\results\\act2\\files\\" + new DirectoryInfo(filePath).Name, changeText);

      //TERMINA EL CRONOMETRO
      watch.Stop();
      return watch.Elapsed.ToString();
   }
     
}

