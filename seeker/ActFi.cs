using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class ActFi{
      //EJECUTA EL PROGRAMA
      public void executeProgram(string path){
         Console.WriteLine("Operacion iniciada");

         //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
         string filePath = path + "\\results\\act4\\consolidatedFile.html";

         //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
         Directory.CreateDirectory(path + "\\results\\act5");
         createFile(path + "\\results\\act5\\results.txt");

         //CREA UN ARRAYLIST PARA GUARDAR LAS PALABRAS
         List<string> words = new List<string>();
         ArrayList wordsAndFreq = new ArrayList();

         //INICIA EL CRONOMETRO
         Stopwatch watch = Stopwatch.StartNew();
         

         //NAVEGA POR CADA ARCHIVO HTML DEL DIRECTORIO
         string[] documentWords = openFile(path, filePath);

         //ESCRIBE CADA PALABRA EN UN ARRAYLIST
         foreach(string word in documentWords){
            //AGREGA CADA PALABRA AL ARRAYLIST
            words.Add(word);
         }
         
         //MAPEA TODAS LAS PALABRAS Y SU FRECUENCIA
         Dictionary<string, int> freqMap = words.GroupBy(x => x)
                                            .Where(g => g.Count() > 1)
                                            .ToDictionary(x => x.Key, x => x.Count());
 
         //GUARDA EL MAPEO EN UN ARRAYLIST
         foreach(KeyValuePair<string, int> word in freqMap) { 
            wordsAndFreq.Add(word.Key + ", " + word.Value);
         } 

         //TERMINA EL CRONOMETRO
         watch.Stop();

         //GUARDA EL TIEMPO TOTAL Y LO ESCRIBE EN UN TXT
         string value = "\nTiempo total en abrir los archivos: " + watch.Elapsed;
         writeOnFile(path, value);

         //CONVIERTE EL ARRAYLIST EN UN ARRAY DE STRINGS
         string[] finalWords = wordsAndFreq.ToArray(typeof(string)) as string[];

         //ESCRIBE TODAS LAS PALABRAS EN UN ARCHIVO CONSOLIDADO
         File.WriteAllLines(path + "\\results\\act5\\consolidatedFile.html", finalWords);
         Console.WriteLine("\nOperación actividad 5");
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
               words[i] = Regex.Replace(words[i], "[^a-zA-Z0-9]", string.Empty);
            }
            
            //BORRA TODOS LOS ESPACIOS RESIDUALES
            words = words.Where(word => !string.IsNullOrWhiteSpace(word)).ToArray();
         } 

         return words;
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

