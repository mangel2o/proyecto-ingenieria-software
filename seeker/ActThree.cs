using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using ConsoleTables;

public class ActThree{
   //EJECUTA EL PROGRAMA
   public void executeProgram(string path){
      //INICIA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      Console.WriteLine("Actividad 3 en proceso... ");

      //TABLA PARA GUARDAR LOS DATOS DE MANERA ORDENADA
      var table = new ConsoleTable("Archivo", "Tiempo");

      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string[] filePaths = Directory.GetFiles(path + "\\results\\act2\\files");

      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      Directory.CreateDirectory(path + "\\results\\act3");

      //CREA UN DIRECTORIO PARA LOS NUEVOS ARCHIVOS HTML
      Directory.CreateDirectory(path + "\\results\\act3\\files");

      //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
      using (var progress = new ProgressBar()) {
         progress.setTask("Extrayendo palabras");
         int count = 0;
         foreach(string filePath in filePaths){
            count++;
            table.AddRow(new DirectoryInfo(filePath).Name , openFile(path, filePath));
            
            progress.Report((double) count / filePaths.Length);
         }
         Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Extrayendo palabras");
      }
      
      //TERMINA EL CRONOMETRO
      watch.Stop();

      //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
      File.WriteAllText(path + "\\results\\act3\\results.txt", table.ToMinimalString());
      File.WriteAllText(path + "\\results\\act3\\results.txt", "Tiempo total en ejecutar el programa: " + watch.Elapsed);
      Console.WriteLine("Actividad 3 completada exitosamente, Noice\n");         
   }


   //ABRE EL ARCHIVO HTMWL
   public string openFile(string path, string filePath){
      //EMPIEZA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      
      //LEE EL ARCHIVO
      string text = File.ReadAllText(filePath);
      
      //CARACTERES ESPECIALES, PON AQUI UN CARACTER SEPARADOR DE TEXTO
      char[] specialChars = new char[] {
         ' ',
         '\n', '\r'
      };

      //DIVIDE EL TEXTO EN UN ARRAY DE PALABRAS
      string[] words = text.Split(specialChars).ToArray();
      
      //BORRA LOS CARACTERES ESPECIALES DEL TEXTO
      for(int i = 0; i < words.Length; i++){
         words[i].ToLower();

         //BORRA LOS ESPACIOS EXISTENTES EN EL TEXTO
         words[i] = Regex.Replace(words[i],  @"\s+", string.Empty);

         //BORRA TODOS LOS CARACTERES NO ALFABETICOS
         words[i] = Regex.Replace(words[i], "[^a-zA-Z]", string.Empty);
      }
      
      //BORRA TODOS LOS ESPACIOS RESIDUALES
      words = words.Where(word => !string.IsNullOrWhiteSpace(word)).ToArray();
      words = words.Where(word => !(word.Length > 100)).ToArray();
      words = words.Select(s => s.ToLowerInvariant()).ToArray();

      //ORDENA EL ARRAY
      Array.Sort(words);

      //ESCRIBE EL ARRAY ORDENADO EN UN NUEVO ARCHIVO HTML
      File.WriteAllLines(path + "\\results\\act3\\files\\" + new DirectoryInfo(filePath).Name, words);
      
      //TERMINA EL CRONOMETRO
      watch.Stop();
      return watch.Elapsed.ToString();
   }
}

