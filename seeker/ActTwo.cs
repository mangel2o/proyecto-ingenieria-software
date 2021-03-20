using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;


public class ActTwo{
      //EJECUTA EL PROGRAMA
   public void executeProgram(string path){

      Console.WriteLine("Operacion iniciada");

      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string[] filePaths = Directory.GetFiles(path + "\\files");
         
      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      createFile(path + "\\results\\act2\\results.txt");
         
      //CREA UN DIRECTORIO PARA LOS NUEVOS ARCHIVOS HTML
      Directory.CreateDirectory(path + "\\results\\act2\\files");
         
      //INICIA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();

      //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
      foreach(string filePath in filePaths){
         openFile(path, filePath);
      }

      //TERMINA EL CRONOMETRO
      watch.Stop();

      //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
      string value = "\nTiempo total en abrir los archivos: " + watch.Elapsed;
      writeOnFile(path, value);
      //Console.WriteLine(value);

      Console.WriteLine("\nOperacion completada exitosamente, Noice");         
      Console.Read();
   }

      public void openFile(string path, string filePath){
         //VARIABLE CHIDA
         string changeText = "";

         //EMPIEZA EL CRONOMETRO
         Stopwatch watch = Stopwatch.StartNew();

         //ABRE EL DOCUMENTO HTML
         using(FileStream fs = File.OpenRead(filePath)) { 
            byte[] b = new byte[1024]; 
            UTF8Encoding temp = new UTF8Encoding(true); 
  
            //LEE EL ARCHIVO HTML Y LO GUARDA EN UN STRING
            while (fs.Read(b, 0, b.Length) > 0) { 
               changeText += temp.GetString(b);
            } 

            //REEMPLAZA TODOS LOS TAGS HTML CON UN STRING VACIO
            changeText = Regex.Replace(changeText, @"<(.|\n)+?>", string.Empty);
            
            //ESCRIBE UN NUEVO DOCUMENTO SIN TAGS DE HTML
            File.WriteAllText(path + "\\results\\act2\\files\\" + new DirectoryInfo(filePath).Name, changeText);
         } 

         //TERMINA EL CRONOMETRO
         watch.Stop();

         //GUARDA EL TIEMPO Y LO ESCRIBE EN UN TXT
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         writeOnFile(path, value);
      }

      //METODO PARA ESCRIBIR INFORMACION EN UN ARCHIVO TXT
      public void writeOnFile(string path, string value){
         FileStream fs = new FileStream(path + "\\results\\act2\\results.txt", FileMode.Append);
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

