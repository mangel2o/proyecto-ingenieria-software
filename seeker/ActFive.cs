using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using ConsoleTables;

public class ActFive{
   //EJECUTA EL PROGRAMA
   public void executeProgram(string path){
      //INICIA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      Console.WriteLine("Actividad 5 en proceso... ");

      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string filePath = path + "\\results\\act4\\consolidatedFile.html";

      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      Directory.CreateDirectory(path + "\\results\\act5");

      //TABLA PARA GUARDAR LOS DATOS DE MANERA ORDENADA
      var dataTable = new ConsoleTable("Palabra", "Frecuencia");

      //CREA UN ARRAYLIST PARA GUARDAR LAS PALABRAS
      ArrayList wordsFreq = new ArrayList();

      //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
      string[] words = File.ReadAllLines(filePath);

      //DICCIONARIO PARA GUARDAR LAS PALABRAS Y CONTEO DE DUPLICADOS
      Dictionary<string, int> dict = new Dictionary<string, int>();

      //IDENTIFICA PALABRAS DUPLICADAS
      using (var progress = new ProgressBar()) {
         progress.setTask("Consolidando palabras");
         int count = 0;
         foreach(string word in words){
            if (!dict.ContainsKey(word)){
               dict.Add(word, 0);
            }
            dict[word]++;

            //PROGRESO
            count++;
            progress.Report((double) count / words.Length);
         }

         //SOLUCION PARA LA ACTIVIDAD 9
         dict = dict.Where(word => !(word.Value < 5)).ToDictionary(word => word.Key, word => word.Value);

         //AGREGA LOS DATOS A LA TABLA
         foreach(KeyValuePair<string, int> word in dict) { 
            
            dataTable.AddRow(word.Key, word.Value);
         } 

         //IMPRIME EL ULTIMO LOG DE PROGRESO
         Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Identificando duplicados");
      }

      //TERMINA EL CRONOMETRO
      watch.Stop();

      //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
      File.WriteAllText(path + "\\results\\act5\\consolidatedFile.html", dataTable.ToMinimalString());
      File.WriteAllText(path + "\\results\\act5\\results.txt", "\nTiempo total en ejecutar el programa: " + watch.Elapsed);
      Console.WriteLine("Actividad 5 completada exitosamente, Noice\n"); 
   }

}

