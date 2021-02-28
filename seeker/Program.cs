using System;
using System.IO;
using System.Diagnostics;
using System.Text;

using System.Linq;

namespace seeker{

   public class ActOne{
      public void executeProgram(string path){
         string[] filePaths = Directory.GetFiles(path + "\\files");
         createFile(path + "\\results\\act1\\results.txt");

         Stopwatch watch = Stopwatch.StartNew();

         //ABRE UNO POR UNO LOS ARCHIVOS DEL DIRECTORIO
         foreach(string filePath in filePaths){
            openFile(path, filePath);
         }
         
         watch.Stop();
         string value = "\nTiempo total en abrir los archivos: " + watch.Elapsed;
         writeOnFile(path, value);
         //Console.WriteLine(value);

         Console.WriteLine("\nOperacion completada exitosamente, Noice");
         Console.Read();
      }

      public void openFile(string path, string filePath){
         Stopwatch watch = Stopwatch.StartNew();

         using(FileStream fs = File.OpenRead(filePath)) { 
            byte[] b = new byte[1024]; 
            //UTF8Encoding temp = new UTF8Encoding(true); 
  
            while (fs.Read(b, 0, b.Length) > 0) { 
                // Printing the file contents 
                //Console.WriteLine(temp.GetString(b)); 
            } 
         } 

         watch.Stop();
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         //Console.WriteLine(value);
         writeOnFile(path, value);
      }

      public void writeOnFile(string path, string value){
         FileStream fs = new FileStream(path + "\\results\\act1\\results.txt", FileMode.Append);
         byte[] bdata = Encoding.Default.GetBytes(value + "\n");
         fs.Write(bdata, 0, bdata.Length);
         fs.Close();
      }

      public static void createFile(string path){
         if (File.Exists(path)){
            File.Delete(path);
         }
         FileStream fs = File.Create(path);
         fs.Close();
      }
   }

   public class ActTwo{
      public void executeProgram(string path){
         string[] filePaths = Directory.GetFiles(path + "\\files");
         createFile(path + "\\results\\act2\\results.txt");
         Directory.CreateDirectory(path + "\\results\\act2\\files");
         Stopwatch watch = Stopwatch.StartNew();

         foreach(string filePath in filePaths){
            openFile(path, filePath);
         }
         watch.Stop();
         string value = "\nTiempo total en abrir los archivos: " + watch.Elapsed;
         writeOnFile(path, value);
         //Console.WriteLine(value);

         Console.WriteLine("\nOperacion completada exitosamente, Noice");
         Console.Read();
      }

      public void openFile(string path, string filePath){
         string changeText = "";
         Stopwatch watch = Stopwatch.StartNew();

         using(FileStream fs = File.OpenRead(filePath)) { 
            byte[] b = new byte[1024]; 
            UTF8Encoding temp = new UTF8Encoding(true); 
  
            while (fs.Read(b, 0, b.Length) > 0) { 
               changeText += temp.GetString(b);
            } 
            changeText = System.Text.RegularExpressions.Regex.Replace(changeText ,@"<(.|\n)+?>", string.Empty);
            
            File.WriteAllText(path + "\\results\\act2\\files\\" + new DirectoryInfo(filePath).Name, changeText);
         } 
         watch.Stop();
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         //Console.WriteLine(value);
         writeOnFile(path, value);
      }

      public void writeOnFile(string path, string value){
         FileStream fs = new FileStream(path + "\\results\\act2\\results.txt", FileMode.Append);
         byte[] bdata = Encoding.Default.GetBytes(value + "\n");
         fs.Write(bdata, 0, bdata.Length);
         fs.Close();
      }

      public static void createFile(string path){
         if (File.Exists(path)){
            File.Delete(path);
         }
         FileStream fs = File.Create(path);
         fs.Close();
      }


      
     
   }

   public class ActThree{
      public void executeProgram(string path){
         Console.WriteLine("Operacion iniciada");
         string[] filePaths = Directory.GetFiles(path + "\\results\\act2\\files");
         createFile(path + "\\results\\act3\\results.txt");
         Directory.CreateDirectory(path + "\\results\\act3\\files");
         Stopwatch watch = Stopwatch.StartNew();

         foreach(string filePath in filePaths){
            openFile(path, filePath);
         }
         
         watch.Stop();
         string value = "\nTiempo total en abrir los archivos: " + watch.Elapsed;
         writeOnFile(path, value);
         //Console.WriteLine(value);

         Console.WriteLine("\nOperacion completada exitosamente, Noice");
         Console.Read();
      }

      public void openFile(string path, string filePath){
         string text = "";
         string[] words;
         Stopwatch watch = Stopwatch.StartNew();

         using(FileStream fs = File.OpenRead(filePath)) { 
            byte[] b = new byte[1024]; 
            UTF8Encoding temp = new UTF8Encoding(true); 
  
            while (fs.Read(b, 0, b.Length) > 0) { 
               text += temp.GetString(b);
            } 
            words = text.Split(new string[] { " ", ",", "." }, StringSplitOptions.None).Select(t => t.Trim()).ToArray();
            Array.Sort(words, StringComparer.InvariantCulture);
            File.WriteAllLines(path + "\\results\\act3\\files\\" + new DirectoryInfo(filePath).Name, words);
         } 
         watch.Stop();
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         //Console.WriteLine(value);
         writeOnFile(path, value);
      }

      public void writeOnFile(string path, string value){
         FileStream fs = new FileStream(path + "\\results\\act3\\results.txt", FileMode.Append);
         byte[] bdata = Encoding.Default.GetBytes(value + "\n");
         fs.Write(bdata, 0, bdata.Length);
         fs.Close();
      }

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
