using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;


  public class ActOne{
      public void executeProgram(string path){

         //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
         string[] filePaths = Directory.GetFiles(path + "\\files");
         
         //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
         Directory.CreateDirectory(path + "\\results\\act1");
         createFile(path + "\\results\\act1\\results.txt");

         //EMPIEZA EL CRONOMETRO
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

         Console.WriteLine("\nOperacion completada exitosamente, Noice");
      }

      public void openFile(string path, string filePath){

         //EMPIEZA EL CRONOMETRO
         Stopwatch watch = Stopwatch.StartNew();


         //ABRE EL DOCUMENTO HTML
         using(FileStream fs = File.OpenRead(filePath)) { 
            byte[] b = new byte[1024]; 
            //UTF8Encoding temp = new UTF8Encoding(true); 

            //LEE EL ARCHIVO BYTE POR BYTE
            while (fs.Read(b, 0, b.Length) > 0) { 
                // Printing the file contents 
                //Console.WriteLine(temp.GetString(b)); 
            } 
         } 

         //TERMINA EL CRONOMETRO
         watch.Stop();

         //GUARDA EL TIEMPO Y LO ESCRIBE EN UN TXT
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         writeOnFile(path, value);
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

