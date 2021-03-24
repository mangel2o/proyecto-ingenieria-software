using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
using ConsoleTables;

public class ActSix{
   public void executeProgram(string path){
      //INICIA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      Console.WriteLine("Actividad 6 en proceso... ");

      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string filePath = path + "\\results\\act5\\consolidatedFile.html";
      string[] filePaths = Directory.GetFiles(path + "\\results\\act3\\files");

      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      Directory.CreateDirectory(path + "\\results\\act6");
      createFile(path + "\\results\\act6\\results.txt");

      //CREA UN ARRAYLIST PARA GUARDAR LAS PALABRAS
      ArrayList changes = new ArrayList();

      //GUARDA TODAS LAS PALABRAS DEL ARCHIVO CONSOLIDADO EN UN ARRAY
      string[] documentWords = File.ReadAllLines(filePath);

      //BORRA LOS CARACTERES ESPECIALES DEL TEXTO
      for(int i = 0; i < documentWords.Length; i++){
         //BORRA LOS ESPACIOS EXISTENTES EN EL TEXTO
         documentWords[i] = Regex.Replace(documentWords[i],  @"\s+", string.Empty);

         //BORRA TODOS LOS CARACTERES NO ALFABETICOS
         documentWords[i] = Regex.Replace(documentWords[i], "[^a-zA-Z]", string.Empty);
      }

      using (var progress = new ProgressBar()) {
         progress.setTask("Comparando palabras");
         for(int i = 0; i < documentWords.Length; i++){
            //GUARDA LAS PALABRAS DE CADA ARCHIVO EN UN ARRAY AUXILIAR
            int count = 0;
            foreach(string file in filePaths){
               string[] auxWords = File.ReadAllLines(file);
               if(auxWords.Contains(documentWords[i])){
                  count += 1;
               }
            }
            changes.Add(", " + count);
            progress.Report((double) i / documentWords.Length);
         } 
         Console.SetCursorPosition(0, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Comparando palabras");
      }

      documentWords = File.ReadAllLines(filePath);
      for(int i = 0; i < documentWords.Length; i++){
         documentWords[i] += changes[i];
      }

      //ORDENA LAS PALABRAS
      Array.Sort(documentWords);

      //ESCRIBE TODAS LAS PALABRAS EN UN ARCHIVO CONSOLIDADO
      File.WriteAllLines(path + "\\results\\act6\\consolidatedFile.html", documentWords);

      //TERMINA EL CRONOMETRO
      watch.Stop();

      //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
      writeOnFile(path, "Tiempo total en ejecutar el programa: " + watch.Elapsed);
      Console.WriteLine("Actividad 6 completada exitosamente, Noice\n"); 
   }

   //METODO PARA ESCRIBIR INFORMACION EN UN ARCHIVO TXT, MANEJA LOS TIEMPOS
   public void writeOnFile(string path, string value){
      FileStream fs = new FileStream(path + "\\results\\act6\\results.txt", FileMode.Append);
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

