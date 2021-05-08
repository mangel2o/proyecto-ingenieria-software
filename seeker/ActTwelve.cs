using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
using ConsoleTables;
using System.Collections.Generic;

public class ActTwelve{
   public void executeProgram(string path, string wordToFind, bool stoplistEnabled){
      //INICIA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      if(stoplistEnabled){
         Console.WriteLine("Actividad 12 con stoplist en proceso... ");
      }else{
         Console.WriteLine("Actividad 12 sin stoplist en proceso... ");
      }
      

      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string[] filePaths = Directory.GetFiles(path + "\\results\\act3\\files");

      //OBTIENE LAS PALABRAS DE LA STOPLIST
      string[] stopList = File.ReadAllLines(path + "\\utils\\stoplist.html");

      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      Directory.CreateDirectory(path + "\\results\\act12");

      //TABLA PARA GUARDAR LOS DATOS DE MANERA ORDENADA
      var dataTable = new ConsoleTable("Palabra", "Existencia", "Posting");
      var dataTablePostings = new ConsoleTable("Palabra", "ID Documento", "Peso");
      var dataTableRelations = new ConsoleTable("ID", "Documento");
      var dataTableDocSearch = new ConsoleTable("ID", "Documento");

      //GUARDA RELACION ID - DOCUMENTO HTML
      Dictionary<int, string> relationDocs = new Dictionary<int, string>();

      //GUARDA TODAS LAS PALABRAS Y SU FRECUENCIA
      List<Word> wordsList = new List<Word>();

      //GUARDA LOS DOCUMENTOS QUE CONTENGAN LA PALABRA BUSCADA
      List<string> docSearch = new List<string>();

      //ENLISTA LA FRECUENCIA DE LAS PALABRAS POR ARCHIVO
      Dictionary<string, Word> freqDict;
      using (var progress = new ProgressBar()) {
         progress.setTask("Calculando frecuencia de palabras");
         int count = 0;
         int countDocs = 1;
         foreach(string filepath in filePaths){
            freqDict = new Dictionary<string, Word>();
            string[] words = File.ReadAllLines(filepath);
            foreach(string word in words){
               if(stoplistEnabled){
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
               }else{
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
               
               if(wordToFind == word){
                  docSearch.Add((string)Path.GetFileName(filepath));
               }
            }
            foreach(KeyValuePair<string, Word> word in freqDict) { 
               if(word.Value.freq >= 3){
                  wordsList.Add(word.Value);
               }
            } 

            //PROGRESO
            count++;
            relationDocs.Add(countDocs, (string)Path.GetFileName(filepath));
            countDocs++;
            progress.Report((double) count / filePaths.Length);
         }
         
         //IMPRIME EL ULTIMO LOG DE PROGRESO
         Console.SetCursorPosition(0, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Calculando frecuencia de palabras");
      }

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
                                      relationDocs.FirstOrDefault(x => x.Value == repetition.Key).Key,
                                      ((wordObj.Value as Word).freq * 100) / (wordObj.Value as Word).freqFile);
         }
      }

      foreach(KeyValuePair<int, string> document in relationDocs){
          dataTableRelations.AddRow(document.Key, document.Value);
      }

      int counter = 1;
      foreach(string doc in docSearch){
         dataTableDocSearch.AddRow(counter, doc);
         counter++;
      }

      //TERMINA EL CRONOMETRO
      watch.Stop();
      
      //ESCRIBE TODAS LAS PALABRAS EN UN ARCHIVO CONSOLIDADO
      File.WriteAllText(path + "\\results\\act12\\posting.html", dataTablePostings.ToMinimalString());
      File.WriteAllText(path + "\\results\\act12\\file.html", dataTable.ToMinimalString());
      File.WriteAllText(path + "\\results\\act12\\relation.html", dataTableRelations.ToMinimalString());
      File.WriteAllText(path + "\\results\\act12\\documentsSearchWord.html", dataTableDocSearch.ToMinimalString());
      File.WriteAllText(path + "\\results\\act12\\results.txt", "\nTiempo total en ejecutar el programa: " + watch.Elapsed);
      Console.WriteLine("Actividad 12 completada exitosamente, Noice\n");
   }
}

