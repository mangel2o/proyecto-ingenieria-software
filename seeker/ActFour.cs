using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
using ConsoleTables;

public class ActFour{
   //EJECUTA EL PROGRAMA
   public void executeProgram(string path){
      //INICIA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      Console.WriteLine("Actividad 4 en proceso... ");

      //TABLA PARA GUARDAR LOS DATOS DE MANERA ORDENADA
      var table = new ConsoleTable("Archivo", "Tiempo");

      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string[] filePaths = Directory.GetFiles(path + "\\results\\act3\\files");

      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      Directory.CreateDirectory(path + "\\results\\act4");

      //CREA UN ARRAYLIST PARA GUARDAR LAS PALABRAS
      ArrayList words = new ArrayList();

      using (var progress = new ProgressBar()) {
         progress.setTask("Consolidando palabras");
         int count = 0;
         //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
         foreach(string filePath in filePaths){
            Stopwatch auxWatch = Stopwatch.StartNew();
            count++;
            //GUARDA LAS PALABRAS DE CADA ARCHIVO EN UN ARRAY AUXILIAR
            string[] documentWords = File.ReadAllLines(filePath);

            //ESCRIBE CADA PALABRA EN UN ARRAYLIST
            foreach(string word in documentWords){
               //AGREGA CADA PALABRA AL ARRAYLIST
               words.Add(word);
            }
            auxWatch.Stop();
            table.AddRow(new DirectoryInfo(filePath).Name , auxWatch.Elapsed);
            progress.Report((double) count / filePaths.Length);
         }
         Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Consolidando palabras");
      }

      //CONVIERTE EL ARRAYLIST EN UN ARRAY DE STRINGS
      string[] finalWords = words.ToArray(typeof(string)) as string[];

      //ORDENA LAS PALABRAS
      Array.Sort(finalWords);

      //ESCRIBE TODAS LAS PALABRAS EN UN ARCHIVO CONSOLIDADO
      File.WriteAllLines(path + "\\results\\act4\\consolidatedFile.html", finalWords);

      //TERMINA EL CRONOMETRO
      watch.Stop();
      
      //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
      File.WriteAllText(path + "\\results\\act4\\results.txt", table.ToMinimalString());
      File.AppendAllText(path + "\\results\\act4\\results.txt", "\nTiempo total en ejecutar el programa: " + watch.Elapsed);
      Console.WriteLine("Actividad 4 completada exitosamente, Noice\n");         
   }
}

