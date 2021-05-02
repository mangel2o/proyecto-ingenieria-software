using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
using ConsoleTables;
using System.Collections.Generic;

public class EviOne{
   public void executeProgram(string path, int cant, bool tokenization, bool indexation){
      //INICIA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      int auxCant = 0;
      Console.WriteLine(cant + "Docs: Evidencia 1 en proceso... ");

      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string[] filePaths = Directory.GetFiles(path + "\\results\\act3\\files");

      //OBTIENE LAS PALABRAS DE LA STOPLIST
      string[] stopList = File.ReadAllLines(path + "\\utils\\stoplist.html");

      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      Directory.CreateDirectory(path + "\\results\\Evi1\\tokenization");
      Directory.CreateDirectory(path + "\\results\\Evi1\\indexation");

      //TABLA PARA GUARDAR LOS DATOS DE MANERA ORDENADA
      var dataTable = new ConsoleTable("Palabra", "Existencia", "Posting");
      var dataTablePostings = new ConsoleTable("Palabra", "Documento", "Peso");

      //GUARDA TODAS LAS PALABRAS Y SU FRECUENCIA
      List<Word> wordsList = new List<Word>();

      //ENLISTA LA FRECUENCIA DE LAS PALABRAS POR ARCHIVO
      Dictionary<string, Word> freqDict;
      using (var progress = new ProgressBar()) {
         progress.setTask("Calculando frecuencia de palabras");
         int count = 0;
         
         foreach(string filepath in filePaths){
            
               if(auxCant != cant){
                  freqDict = new Dictionary<string, Word>();
                  string[] words = File.ReadAllLines(filepath);
                  foreach(string word in words){
                     foreach(string stopWord in stopList){
                        if(word != stopWord){
                           if (!freqDict.ContainsKey(word)){
                              freqDict.Add(word, new Word(word/*, filepath*/));
                           }
                           freqDict[word].freq++;
                           //CONSIGUE EL FILE NAME DEL PATH
                           string fileName = (string)Path.GetFileName(filepath);
                           //REVISA SI EXISTE YA EL ARCHIVO EN EL DICCIONARIO
                           if (!freqDict[word].KeyFileRepetitions.ContainsKey(fileName)){
                              //AGREGA EL NOMBRE DEL ARCHIVO AL DICCIONARIO
                              freqDict[word].KeyFileRepetitions.Add(fileName,0);
                           }
                           freqDict[word].KeyFileRepetitions[fileName]++;
                        }
                     }
                     
                  }
                  foreach(KeyValuePair<string, Word> word in freqDict) { 
                     if(word.Value.freq >= 3){
                        wordsList.Add(word.Value);
                     }
                  } 
                  auxCant++;
                  
               }
            //PROGRESO
            count++;
            progress.Report((double) count / filePaths.Length);
         }
         
         //IMPRIME EL ULTIMO LOG DE PROGRESO
         Console.SetCursorPosition(0, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Calculando frecuencia de palabras");
      }

      if(indexation){
         //ENLISTA LA EXISTENCIA DE LAS PALABRAS POR ARCHIVO
         SortedDictionary<string, Word> exDict = new SortedDictionary<string, Word>();
         using (var progress = new ProgressBar()) {
            progress.setTask("Calculando existencia de palabras");
            int count = 0;
            foreach(Word wordObj in wordsList){
               
            
                     if (!exDict.ContainsKey(wordObj.content)){
                        exDict.Add(wordObj.content, new Word(wordObj.content, wordObj.freq));
                     }
                     exDict[wordObj.content].freq += wordObj.freq;
                     exDict[wordObj.content].freqFile++;
                     foreach(KeyValuePair<string,int> temp in wordObj.KeyFileRepetitions){
                        exDict[wordObj.content].references.Add(temp);
                     }

                     //PROGRESO
                     count++;
                  
                     progress.Report((double) count / wordsList.Count);
               
               
            }

            //IMPRIME EL ULTIMO LOG DE PROGRESO        
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("\n[##########]  100%  | Calculando existencia de palabras");
         }
   
         //HASHTABLE PARA GUARDAR LOS DATOS
         Hashtable hTable = new Hashtable(exDict);

         //ARRAY PARA GUARDAR EL RESULTADO
         int freqPosting = 0;
         foreach(DictionaryEntry wordObj in hTable){
            dataTable.AddRow(wordObj.Key,
                           (wordObj.Value as Word).freqFile,
                           freqPosting);

            freqPosting += (wordObj.Value as Word).freqFile;

            foreach(KeyValuePair<string, int> repetition in (wordObj.Value as Word).references){
               dataTablePostings.AddRow((wordObj.Value as Word).content,
                                       repetition.Key,
                                       ((wordObj.Value as Word).freq * 100) / (wordObj.Value as Word).freqFile);
            }
         }
      }
      //TERMINA EL CRONOMETRO
      watch.Stop();
      
      //ESCRIBE TODAS LAS PALABRAS EN UN ARCHIVO CONSOLIDADO
      if(indexation){
          /*
         File.WriteAllText(path + "\\results\\Evi1\\indexation\\"+ cant +"DocsConsolidatedFilePostings.html", dataTablePostings.ToMinimalString());
         File.WriteAllText(path + "\\results\\Evi1\\indexation\\"+ cant +"DocsConsolidatedFile.html", dataTable.ToMinimalString());
         */

         File.WriteAllText(path + "\\results\\Evi1\\indexation\\"+ cant +"DocsResults.txt", "\nTiempo total en ejecutar el programa: " + watch.Elapsed);
         
      }else if(tokenization){
        /*
         File.WriteAllText(path + "\\results\\Evi1\\tokenization\\"+ cant +"DocsConsolidatedFilePostings.html", dataTablePostings.ToMinimalString());
         File.WriteAllText(path + "\\results\\Evi1\\tokenization\\"+ cant +"DocsConsolidatedFile.html", dataTable.ToMinimalString());
         */

         File.WriteAllText(path + "\\results\\Evi1\\tokenization\\"+ cant +"DocsResults.txt", "\nTiempo total en ejecutar el programa: " + watch.Elapsed);
      }
      
      Console.WriteLine("Evidencia 1 completada exitosamente, Noice\n");
   }
}

