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

         ArrayList words = new ArrayList();
         Console.WriteLine("Operacion iniciada");

         //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
         string[] filePaths = Directory.GetFiles(path + "\\results\\act2\\files");

         //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
         createFile(path + "\\results\\act4\\resultsLog.txt");

         createFile(path + "\\results\\act4\\resultsWords.txt");

         //CREA UN DIRECTORIO PARA LOS NUEVOS ARCHIVOS HTML
         Directory.CreateDirectory(path + "\\results\\act4\\files");

         //INICIA EL CRONOMETRO
         Stopwatch watch = Stopwatch.StartNew();

         //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
         foreach(string filePath in filePaths){
            openFile(path, filePath, words);
         }
         
         //TERMINA EL CRONOMETRO
         watch.Stop();
         double tiempoTotal = watch.Elapsed.TotalSeconds;
         //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
         string value = "\nTiempo total en abrir los archivos: " + watch.Elapsed;
         writeOnFileLog(path, value);
         //NUEVO STOPWATCH PARA MEDIR TIEMPO DE ESCRITURA DE PALABRAS
         watch = Stopwatch.StartNew();

         //ORDENAR LAS PALABRAS
         words.Sort();
         //ESCRIBIR LAS PALABRAS
         writeOnFileWords(path,words);
         
         watch.Stop();
         tiempoTotal = tiempoTotal + watch.Elapsed.TotalSeconds;
         //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
         value = "\nTiempo total en escribir las palabras: " + watch.Elapsed;
         writeOnFileLog(path, value);
         value = "\nTiempo total: " + tiempoTotal;
         writeOnFileLog(path, value);

         Console.WriteLine("\nOperación actividad 4");
      }


      //ABRE EL ARCHIVO HTMWL
      public void openFile(string path, string filePath, ArrayList words2){
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
               words[i] = Regex.Replace(words[i], "[^a-zA-Z]", string.Empty).ToLower();
            }
            
            //BORRA TODOS LOS ESPACIOS RESIDUALES
            words = words.Where(word => !string.IsNullOrWhiteSpace(word)).ToArray();

            //ORDENA EL ARRAY
            Array.Sort(words);

            //ESCRIBE EL ARRAY ORDENADO EN UN NUEVO ARCHIVO HTML
            File.WriteAllLines(path + "\\results\\act3\\files\\" + new DirectoryInfo(filePath).Name, words);

            //ESCRIBE TODAS LAS PALABRAS A UN ARRAY LIST
            for(int i = 0; i < words.Length;i++)
            {
            words2.Add(words[i]);
            }
         } 

         //TERMINA EL CRONOMETRO
         watch.Stop();

         //GUARDA EL TIEMPO Y LO ESCRIBE EN UN TXT
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         writeOnFileLog(path, value);
      }

      //METODO PARA ESCRIBIR INFORMACION EN UN ARCHIVO TXT, MANEJA LAS PALABRAS
      public void writeOnFileWords(string path, ArrayList value){
         FileStream fs = new FileStream(path + "\\results\\act4\\resultsWords.txt", FileMode.Append);
         ArrayList bdataFinal = new ArrayList();
         foreach(object str in value){
         byte[] bdata = Encoding.Default.GetBytes((string)str + "\n");
         foreach(byte bt in bdata)
         bdataFinal.Add(bt);
         }
         byte[] test = (byte[]) bdataFinal.ToArray(typeof(byte));
         fs.Write(test, 0, bdataFinal.ToArray().Length);
         fs.Close();
      }
      
      //METODO PARA ESCRIBIR INFORMACION EN UN ARCHIVO TXT, MANEJA LOS TIEMPOS
      public void writeOnFileLog(string path, string value){
         FileStream fs = new FileStream(path + "\\results\\act4\\resultsLog.txt", FileMode.Append);
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

