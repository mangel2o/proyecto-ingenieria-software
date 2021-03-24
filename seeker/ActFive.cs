using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

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
      createFile(path + "\\results\\act5\\results.txt");

      //CREA UN ARRAYLIST PARA GUARDAR LAS PALABRAS
      ArrayList wordsAndFreq = new ArrayList();

      //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
      string[] documentWords = File.ReadAllLines(filePath);

      //DICCIONARIO PARA GUARDAR LAS PALABRAS Y CONTEO DE DUPLICADOS
      Dictionary<string, int> dict = new Dictionary<string, int>();

      //IDENTIFICA PALABRAS DUPLICADAS
      using (var progress = new ProgressBar()) {
         progress.setTask("Consolidando palabras");
         int count = 0;
         foreach(string word in documentWords){
            count++;
            if (!dict.ContainsKey(word)){
               dict.Add(word, 0);

            }
            dict[word]++;
            progress.Report((double) count / documentWords.Length);
         }

         //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
         foreach(KeyValuePair<string, int> word in dict) { 
            wordsAndFreq.Add(word.Key + ", " + word.Value);
         } 

         Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Identificando duplicados");
      }

      //CONVIERTE EL ARRAYLIST EN UN ARRAY DE STRINGS
      string[] finalWords = wordsAndFreq.ToArray(typeof(string)) as string[];

      //ESCRIBE TODAS LAS PALABRAS EN UN ARCHIVO CONSOLIDADO
      File.WriteAllLines(path + "\\results\\act5\\consolidatedFile.html", finalWords);

      //TERMINA EL CRONOMETRO
      watch.Stop();

      //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
      writeOnFile(path, "Tiempo total en ejecutar el programa: " + watch.Elapsed);
      Console.WriteLine("Actividad 5 completada exitosamente, Noice\n"); 
   }

   //METODO PARA ESCRIBIR INFORMACION EN UN ARCHIVO TXT, MANEJA LOS TIEMPOS
   public void writeOnFile(string path, string value){
      FileStream fs = new FileStream(path + "\\results\\act5\\results.txt", FileMode.Append);
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

