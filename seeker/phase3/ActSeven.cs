using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
using ConsoleTables;
using System.Collections.Generic;

public class ActSeven{
   public void executeProgram(string path){
      //INICIA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      Console.WriteLine("Actividad 7 en proceso... ");

      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string[] filePaths = Directory.GetFiles(path + "\\results\\act3\\files");

      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      Directory.CreateDirectory(path + "\\results\\act7");

      //TABLA PARA GUARDAR LOS DATOS DE MANERA ORDENADA
      var dataTable = new ConsoleTable("Palabra", "Existencia", "Posting");
      var dataTablePostings = new ConsoleTable("Palabra", "Documento", "Frequencia");

      //GUARDA TODAS LAS PALABRAS Y SU FRECUENCIA
      List<Word> wordsList = new List<Word>();

      //GUARDA TODAS LAS PALABRAS, FRECUENCIA TOTAL Y EXISTENCIA
      List<Word> finalWordsList = new List<Word>();

      //ENLISTA LA FRECUENCIA DE LAS PALABRAS POR ARCHIVO
      using (var progress = new ProgressBar()) {
         progress.setTask("Calculando frecuencia de palabras");
         int count = 0;
         foreach(string filepath in filePaths){
            Dictionary<string, Word> freqDict = new Dictionary<string, Word>();
            string[] words = File.ReadAllLines(filepath);
            foreach(string word in words){
               if (!freqDict.ContainsKey(word)){
                  freqDict.Add(word, new Word(word/*, filepath*/));
               }
               freqDict[word].freq++;
               //CONSIGUE EL FILE NAME DEL PATH
               string fileName = (string)Path.GetFileName(filepath);
               //REVISA SI EXISTE YA EL ARCHIVO EN EL DICCIONARIO
               if (!freqDict[word].KeyFileRepetitions.ContainsKey(fileName))
               {
                  //AGREGA EL NOMBRE DEL ARCHIVO AL DICCIONARIO
                  freqDict[word].KeyFileRepetitions.Add(fileName,0);
               }
               freqDict[word].KeyFileRepetitions[fileName]++;
            }
            foreach(KeyValuePair<string, Word> word in freqDict) { 
               wordsList.Add(word.Value);
            } 

            //PROGRESO
            count++;
            progress.Report((double) count / filePaths.Length);
         }
         
         //IMPRIME EL ULTIMO LOG DE PROGRESO
         Console.SetCursorPosition(0, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Calculando frecuencia de palabras");
      }

      //ENLISTA LA EXISTENCIA DE LAS PALABRAS POR ARCHIVO
      Dictionary<string, Word> exDict = new Dictionary<string, Word>();
      using (var progress = new ProgressBar()) {
         progress.setTask("Calculando existencia de palabras");
         int count = 0;
         foreach(Word wordObj in wordsList){
            if (!exDict.ContainsKey(wordObj.content)){
               exDict.Add(wordObj.content, new Word(wordObj.content, wordObj.freq));
            }
            exDict[wordObj.content].freq += wordObj.freq;
            exDict[wordObj.content].freqFile++;
            foreach(KeyValuePair<string,int> temp in wordObj.KeyFileRepetitions)
            {
               exDict[wordObj.content].references.Add(temp);
            }

            //PROGRESO
            count++;
            progress.Report((double) count / wordsList.Count);
         }     
         //SOLO HAY QUE TRAERNOS LOS VALORES, NO TODO EL DICCIONARIO
         foreach(KeyValuePair<string, Word> wordObj in exDict) { 
            finalWordsList.Add(wordObj.Value);
         } 
         
          //IMPRIME EL ULTIMO LOG DE PROGRESO        
         Console.SetCursorPosition(0, Console.CursorTop - 1);
         Console.WriteLine("\n[##########]  100%  | Calculando existencia de palabras");
      }
      
      //ORDENA LAS PALABRAS
      finalWordsList.Sort((x, y) => x.content.CompareTo(y.content));

      //ARRAY PARA GUARDAR EL RESULTADO
      int freqPosting = 0;
      for(int i = 0; i < finalWordsList.Count; i++){
         dataTable.AddRow(finalWordsList[i].content,
                          finalWordsList[i].freqFile, 
                          freqPosting);
         freqPosting += finalWordsList[i].freqFile;

         foreach(KeyValuePair<string,int> repetition in finalWordsList[i].references){ 
            dataTablePostings.AddRow(finalWordsList[i].content,
                                    repetition.Key,
                                    repetition.Value);
         }
         
      }

      //TERMINA EL CRONOMETRO
      watch.Stop();
      
      //ESCRIBE TODAS LAS PALABRAS EN UN ARCHIVO CONSOLIDADO
      File.WriteAllText(path + "\\results\\act7\\consolidatedFilePostings.html", dataTablePostings.ToMinimalString());
      File.WriteAllText(path + "\\results\\act7\\consolidatedFile.html", dataTable.ToMinimalString());
      File.WriteAllText(path + "\\results\\act7\\results.txt", "\nTiempo total en ejecutar el programa: " + watch.Elapsed);
      Console.WriteLine("Actividad 7 completada exitosamente, Noice");
   }
}

