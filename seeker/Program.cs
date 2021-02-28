using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

using System.Linq;

namespace seeker{

   public class ActOne{
      public void executeProgram(string path){

         //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
         string[] filePaths = Directory.GetFiles(path + "\\files");
         
         //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
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

   public class ActThree{
      //EJECUTA EL PROGRAMA
      public void executeProgram(string path){

         Console.WriteLine("Operacion iniciada");

         //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
         string[] filePaths = Directory.GetFiles(path + "\\results\\act2\\files");

         //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
         createFile(path + "\\results\\act3\\results.txt");

         //CREA UN DIRECTORIO PARA LOS NUEVOS ARCHIVOS HTML
         Directory.CreateDirectory(path + "\\results\\act3\\files");

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

         Console.WriteLine("\nOperacion completada exitosamente, Noice");
      }


      //ABRE EL ARCHIVO HTMWL
      public void openFile(string path, string filePath){
         //VARIABLES CHIDAS
         string text = "";
         string[] words;
         
         //CARACTERES ESPECIALES, PON AQUI UN CARACTER SEPARADOR DE TEXTO
         char[] specialChars = new char[] {
            ' ', ',', '.', '/', '&',
            '\n', '\r'
         };

         //EMPIEZA EL CRONOMETRO
         Stopwatch watch = Stopwatch.StartNew();

         //ABRE EL DOCUMENTO HTML
         using(FileStream fs = File.OpenRead(filePath)) { 
            byte[] b = new byte[1024]; 
            UTF8Encoding temp = new UTF8Encoding(true); 

            //LEE EL ARCHIVO HTML Y LO GUARDA EN UN STRING
            while (fs.Read(b, 0, b.Length) > 0) { 
               text += temp.GetString(b);
            }
            
            //DIVIDE EL TEXTO EN UN ARRAY DE PALABRAS
            words = text.Split(specialChars).ToArray();
            
            //BORRA LOS CARACTERES ESPECIALES DEL TEXTO
            for(int i = 0; i < words.Length; i++){
               //BORRA LOS ESPACIOS EXISTENTES EN EL TEXTO
               words[i] = Regex.Replace(words[i],  @"\s+", string.Empty);

               //BORRA TODOS LOS CARACTERES NO ALFABETICOS
               words[i] = Regex.Replace(words[i], "[^a-zA-Z0-9]", string.Empty);
            }
            
            //BORRA TODOS LOS ESPACIOS RESIDUALES
            words = words.Where(word => !string.IsNullOrWhiteSpace(word)).ToArray();

            //ORDENA EL ARRAY
            Array.Sort(words);

            //ESCRIBE EL ARRAY ORDENADO EN UN NUEVO ARCHIVO HTML
            File.WriteAllLines(path + "\\results\\act3\\files\\" + new DirectoryInfo(filePath).Name, words);
         } 

         //TERMINA EL CRONOMETRO
         watch.Stop();

         //GUARDA EL TIEMPO Y LO ESCRIBE EN UN TXT
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         writeOnFile(path, value);
      }

      //METODO PARA ESCRIBIR INFORMACION EN UN ARCHIVO TXT
      public void writeOnFile(string path, string value){
         FileStream fs = new FileStream(path + "\\results\\act3\\results.txt", FileMode.Append);
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


   class Program{
      static void Main(string[] args){

         string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
         path += "\\GitHub\\proyecto-ingenieria-software\\seeker";

         /*
         ActOne act1 = new ActOne();
         act1.executeProgram(path);
         */

         /*
         ActTwo act2 = new ActTwo();
         act2.executeProgram(path);
         */

         ActThree act3 = new ActThree();
         act3.executeProgram(path);
      }

      /*
      public static void copyFilesFromTo(string sourcePath, string targetPath){
         string fileName;
         string destFile;

         Directory.CreateDirectory(targetPath);

         string[] files = Directory.GetFiles(sourcePath);
            foreach (string file in files)
            {
                fileName = Path.GetFileName(file);
                destFile = Path.Combine(targetPath, fileName);
                File.Copy(file, destFile, true);
            }
      }
      */
   }
}
