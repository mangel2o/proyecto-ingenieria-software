using System;

namespace seeker{
   class Program{
      static void Main(string[] args){
         string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
         path += "\\GitHub\\proyecto-ingenieria-software\\seeker";

         /*
         ActOne act1 = new ActOne();
         act1.executeProgram(path);
         

         ActTwo act2 = new ActTwo();
         act2.executeProgram(path);
         
         ActThree act3 = new ActThree();
         act3.executeProgram(path);
         */
         ActFour act4 = new ActFour();
         act4.executeProgram(path);
         
      }
   }
}
