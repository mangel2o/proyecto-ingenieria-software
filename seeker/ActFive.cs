using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Linq;


public class ActFive{
      int pos = 1;
      string[,] words2 = new string[5000,2];

      //EJECUTA EL PROGRAMA
      public void executeProgram(string path){

         Console.WriteLine("Operacion iniciada");

         //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
         string[] filePaths = Directory.GetFiles(path + "\\act5");

         //CREA UN DIRECTORIO PARA LOS NUEVOS ARCHIVOS HTML
         Directory.CreateDirectory(path + "\\results\\act5\\files");

         //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
         createFile(path + "\\results\\act5\\resultsLog.txt");
         createFile(path + "\\results\\act5\\resultsWords.txt");

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
         writeOnFileLog(path, value);

         //ESCRIBIR LAS PALABRAS
         writeOnFileWords(path);

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
               words[i] = Regex.Replace(words[i], "[^a-zA-Z]", string.Empty).ToLower();
            }
            
            //BORRA TODOS LOS ESPACIOS RESIDUALES
            words = words.Where(word => !string.IsNullOrWhiteSpace(word)).ToArray();

            //ORDENA EL ARRAY
            Array.Sort(words);

            int tempVal = 0;
            //ESCRIBE TODAS LAS PALABRAS A UN ARRAY LIST
            for(int i = 0; i < words.Length;i++)
            { 
               for(int j = 1; j <= pos; j++)
               {
                  if(words[i] == words2[j,0])
                  {
                     try{
                        tempVal = Int32.Parse(words2[j,1]);
                     }catch(Exception e){
                        tempVal = 0;
                     }
                     tempVal++;
                     words2[j,1] = tempVal.ToString();
                     
                     goto Foo;
                  }else if(j == pos)
                  {
                     words2[pos,0] = words[i];
                     words2[pos,1] = "1";
                     pos++;
                     goto Foo;
                  }
               }
                  Foo:
                  string tmp = "";
            }
         } 

         //TERMINA EL CRONOMETRO
         watch.Stop();

         //GUARDA EL TIEMPO Y LO ESCRIBE EN UN TXT
         string value = new DirectoryInfo(filePath).Name + " -/- " + watch.Elapsed;
         writeOnFileLog(path, value);
      }

      //METODO PARA ESCRIBIR INFORMACION EN UN ARCHIVO TXT, MANEJA LAS PALABRAS
      public void writeOnFileWords(string path){
         FileStream fs = new FileStream(path + "\\results\\act5\\resultsWords.txt", FileMode.Append);
         byte[] bdata = new byte[100000];
         DataTable dt = new DataTable();
         // assumes first row contains column names:
         for (int col = 0; col < words2.GetLength(1); col++)
         {
            dt.Columns.Add(words2[0, col]);
         }
         // load data from string array to data table:
         for (int rowindex = 1; rowindex < words2.GetLength(0); rowindex++)
         {
            DataRow row = dt.NewRow();
            for (int col = 0; col < words2.GetLength(1); col++)
            {
               row[col] = words2[rowindex, col];
            }
            dt.Rows.Add(row);
         }
         // sort by third column:
         DataRow[] sortedrows = dt.Select("", "Column1 ASC");
         for(int i = 0; i < sortedrows.Length;i++)
         {
            if (sortedrows[i].ItemArray[0].ToString() == "")
            {
               continue;
            }
            bdata = Encoding.Default.GetBytes(sortedrows[i].ItemArray[0].ToString() + " " + sortedrows[i].ItemArray[1].ToString() + "\n");
            fs.Write(bdata, 0, bdata.Length);
         }
         fs.Close();
      }
      
      //METODO PARA ESCRIBIR INFORMACION EN UN ARCHIVO TXT, MANEJA LOS TIEMPOS
      public void writeOnFileLog(string path, string value){
         FileStream fs = new FileStream(path + "\\results\\act5\\resultsLog.txt", FileMode.Append);
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