using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Linq;
public class ActSix{
     
     public void executeProgram(String path){
        string filePaths = "C:\\Users\\Admin\\OneDrive\\Documentos\\GitHub\\proyecto-ingenieria-software\\seeker\\results\\act4\\resultsWords.txt";
        LecturaCaracteres(filePaths);
        PalabrasContador(filePaths);
     }
     public void PalabrasContador(String filePaths){
      StreamReader srt = new StreamReader(filePaths);
      string registro; //informacion
      registro = srt.ReadLine();
      string texto = registro;
      bool nueva = true;
      int palabras = 0;

      string docPath =Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "C:\\Users\\Admin\\OneDrive\\Documentos\\GitHub\\proyecto-ingenieria-software\\seeker\\results\\act4\\CountWords.txt")))
      foreach (Char c in texto)
      {
        if (Char.IsLetter(c))
        {
          if (nueva)
          {
            palabras++;
            nueva = false;

          }
        }
        else nueva = true;
        outputFile.WriteLine(c);
        outputFile.WriteLine(palabras);
      }
        
     }
     

     public void LecturaCaracteres(String filePaths){
      StreamReader srt = new StreamReader(filePaths);
      string registro; //informacion
      registro = srt.ReadLine();
      int linea = 0;
      int carac = 0;
      while(registro != null)
      {
        Console.WriteLine(registro);
        
        carac = carac + registro.Length;
        registro = srt.ReadLine();
        linea = linea + 1;
      }

      Console.WriteLine("<--------------------------------------------------------->");
      Console.WriteLine("Numero de lineas: " + linea);
      Console.WriteLine("Numero de caracteres: " + carac);
     
     }
   }

