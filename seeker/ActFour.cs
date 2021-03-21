using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
public class ActFour{
      //EJECUTA EL PROGRAMA
      public void executeProgram(string path){

         
         Console.WriteLine("Operacion iniciada");

         //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
         string[] filePaths = Directory.GetFiles(path + "\\results\\act3\\files");

         //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
         Directory.CreateDirectory(path + "\\results\\act4");
         createFile(path + "\\results\\act4\\results.txt");

         //CREA UN ARRAYLIST PARA GUARDAR LAS PALABRAS
         ArrayList words = new ArrayList();

         //INICIA EL CRONOMETRO
         Stopwatch watch = Stopwatch.StartNew();
         

         //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
         foreach(string filePath in filePaths){

            //GUARDA LAS PALABRAS DE CADA ARCHIVO EN UN ARRAY AUXILIAR
            string[] documentWords = openFile(path, filePath);

            //ESCRIBE CADA PALABRA EN UN ARRAYLIST
            foreach(string word in documentWords){
               //AGREGA CADA PALABRA AL ARRAYLIST
               words.Add(word);
            }
         }
         
         //TERMINA EL CRONOMETRO
         watch.Stop();

         //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
         string value = "\nTiempo total en abrir los archivos: " + watch.Elapsed;
         writeOnFile(path, value);
         /*
         double tiempoTotal = watch.Elapsed.TotalSeconds;
         //NUEVO STOPWATCH PARA MEDIR TIEMPO DE ESCRITURA DE PALABRAS
         watch = Stopwatch.StartNew();
         */
         //CONVIERTE EL ARRAYLIST EN UN ARRAY DE STRINGS
         string[] finalWords = words.ToArray(typeof(string)) as string[];

         //ORDENA LAS PALABRAS
         Array.Sort(finalWords);

         //ESCRIBE TODAS LAS PALABRAS EN UN ARCHIVO CONSOLIDADO
         File.WriteAllLines(path + "\\results\\act4\\consolidatedFile.html", finalWords);
         
         /*
         watch.Stop();
         tiempoTotal = tiempoTotal + watch.Elapsed.TotalSeconds;
         //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
         value = "\nTiempo total en escribir las palabras: " + watch.Elapsed;
         writeOnFile(path, value);
         value = "\nTiempo total: " + tiempoTotal;
         writeOnFile(path, value);
         */
         Console.WriteLine("\nOperación actividad 4");
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
         } 

         //TERMINA EL CRONOMETRO
         watch.Stop();

         //GUARDA EL TIEMPO Y LO ESCRIBE EN UN TXT
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         writeOnFile(path, value);
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

