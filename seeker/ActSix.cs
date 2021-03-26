using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
using ConsoleTables;
using System.Collections.Generic;

public class ActSix{
   public void executeProgram(string path){
      //INICIA EL CRONOMETRO
      Stopwatch watch = Stopwatch.StartNew();
      Console.WriteLine("Actividad 6 en proceso... ");

      //OBTIENE LAS RUTAS DE LOS ARCHIVOS HTML
      string[] filePaths = Directory.GetFiles(path + "\\results\\act3\\files");

      //SE CREA UN ARCHIVO TXT DE SALIDA DE DATOS
      Directory.CreateDirectory(path + "\\results\\act6");

      //TABLA PARA GUARDAR LOS DATOS DE MANERA ORDENADA
      var dataTable = new ConsoleTable("Palabra", "Frecuencia", "Existencia");

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
            //exDict[wordObj.content].references.Add(wordObj.reference);

            //PROGRESO
            count++;
            progress.Report((double) count / wordsList.Count);
         }
         
         
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
      for(int i = 0; i < finalWordsList.Count; i++){
         dataTable.AddRow(finalWordsList[i].content,
                          finalWordsList[i].freq, 
                          finalWordsList[i].freqFile);
      }

      //TERMINA EL CRONOMETRO
      watch.Stop();
      
      //ESCRIBE TODAS LAS PALABRAS EN UN ARCHIVO CONSOLIDADO
      File.WriteAllText(path + "\\results\\act6\\consolidatedFile.html", dataTable.ToMinimalString());
      File.WriteAllText(path + "\\results\\act6\\results.txt", "Tiempo total en ejecutar el programa: " + watch.Elapsed);
      Console.WriteLine("Actividad 6 completada exitosamente, Noice");
   }
}

