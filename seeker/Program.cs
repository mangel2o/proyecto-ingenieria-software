using System;
using System.IO;
using System.Diagnostics;
using System.Text;

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
         Console.Read();
      }

      public void openFile(string path, string filePath){
         Stopwatch watch = Stopwatch.StartNew();

         FileStream fs = File.OpenRead(filePath);
         fs.Close();

         watch.Stop();
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         writeOnFile(path, value);
         //Console.WriteLine(value);
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


   class Program{
      static void Main(string[] args){

         string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
         path += "\\GitHub\\proyecto-ingenieria-software\\seeker";
         
         ActOne act1 = new ActOne();
         act1.executeProgram(path);
         

      }

     public static void remove_html_tags(FileStream fs)
     {
        string text = "";
        byte[] buf = new byte[1024];
        int c;

         while ((c = fs.Read(buf, 0, buf.Length)) > 0)
         {
         text = text + (Encoding.UTF8.GetString(buf, 0, c));
         }
        text = System.Text.RegularExpressions.Regex.Replace(text ,@"<(.|\n)+?>", string.Empty);
        text = "";
     }

   }
}
