
using System;
using ConsoleTables;

namespace seeker{
   class Program{
      static void Main(string[] args){
         string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
         path += "\\GitHub\\proyecto-ingenieria-software\\seeker";
         
         ActOne act1 = new ActOne();
         act1.executeProgram(path);

         ActTwo act2 = new ActTwo();
         act2.executeProgram(path);
         
         ActThree act3 = new ActThree();
         act3.executeProgram(path);
         
         ActFour act4 = new ActFour();
         act4.executeProgram(path);
         
         ActFive act5 = new ActFive();
         act5.executeProgram(path);
         
         ActSix act6 = new ActSix();
         act6.executeProgram(path);

         ActSeven act7 = new ActSeven();
         act7.executeProgram(path);

         ActEight act8 = new ActEight();
         act8.executeProgram(path);
         
         /*
         ActNine act9 = new ActNine();
         act9.executeProgram(path);

         ActTen act10 = new ActTen();
         act10.executeProgram(path);
         */

         EviOne evi1 = new EviOne();
         int[] cantDocs = {10, 20, 30, 40, 50, 100};

         foreach(int cant in cantDocs){
            //TOKENIZACIÓN
            evi1.executeProgram(path, cant, true, false);

            //INDEXACIÓN
            evi1.executeProgram(path, cant, true, true);
         }
      }
   }
}
