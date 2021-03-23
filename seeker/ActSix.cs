using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;

public class ActSix{
     public void executeProgram(string path){
         Console.WriteLine("Operacion iniciada");

         //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
         string filePath = path + "\\results\\act5\\consolidatedFile.html";
         string[] filePaths = Directory.GetFiles(path + "\\results\\act3\\files");

         //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
         Directory.CreateDirectory(path + "\\results\\act6");
         createFile(path + "\\results\\act6\\results.txt");

         //CREA UN ARRAYLIST PARA GUARDAR LAS PALABRAS
         ArrayList changes = new ArrayList();

         //INICIA EL CRONOMETRO
         Stopwatch watch = Stopwatch.StartNew();
         
         //GUARDA TODAS LAS PALABRAS DEL ARCHIVO CONSOLIDADO EN UN ARRAY
         string[] documentWords = openFile(path, filePath);

        for(int i = 0; i < documentWords.Length; i++){
          Stopwatch auxWatch = Stopwatch.StartNew();
            //GUARDA LAS PALABRAS DE CADA ARCHIVO EN UN ARRAY AUXILIAR
            int count = 0;

            foreach(string file in filePaths){
                string[] auxWords = File.ReadAllLines(file);
                if(auxWords.Contains(documentWords[i])){
                  count += 1;
                }
            }
            changes.Add(", " + count);
            auxWatch.Stop();
            Console.WriteLine(auxWatch.Elapsed);
        }

        documentWords = File.ReadAllLines(filePath);
        for(int i = 0; i < documentWords.Length; i++){
          documentWords[i] += changes[i];
        }

         //TERMINA EL CRONOMETRO
         watch.Stop();

         //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
         string value = "\nTiempo total en abrir los archivos: " + watch.Elapsed;
         writeOnFile(path, value);

         //ORDENA LAS PALABRAS
         Array.Sort(documentWords);

         //ESCRIBE TODAS LAS PALABRAS EN UN ARCHIVO CONSOLIDADO
         File.WriteAllLines(path + "\\results\\act6\\consolidatedFile.html", documentWords);
         Console.WriteLine("\nOperación actividad 6");
      }


      //ABRE EL ARCHIVO HTMWL
      public string[] openFile(string path, string filePath){
         //VARIABLES CHIDAS
         string text = "";
         string[] words;
         
         //CARACTERES ESPECIALES, PON AQUI UN CARACTER SEPARADOR DE TEXTO
         char[] specialChars = new char[] {
            ' ', ',', '.', '/', '&',
            '\n', '\r'
         };

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
               words[i] = Regex.Replace(words[i], "[^a-zA-Z]", string.Empty);
            }
            
            //BORRA TODOS LOS ESPACIOS RESIDUALES
            words = words.Where(word => !string.IsNullOrWhiteSpace(word)).ToArray();
         } 

         return words;
      }

      //METODO PARA ESCRIBIR INFORMACION EN UN ARCHIVO TXT, MANEJA LOS TIEMPOS
      public void writeOnFile(string path, string value){
         FileStream fs = new FileStream(path + "\\results\\act4\\results.txt", FileMode.Append);
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

