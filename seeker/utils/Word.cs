using System.Collections;
using System.Collections.Generic;

public class Word{
   public string content;
   public int freq;
   public int freqFile;
   public string reference;
   public ArrayList references;

   public Word(string content){
      this.content = content;
      this.references = new ArrayList();
   }

   public Word(string content, string reference){
      this.content = content;
      this.reference = reference;
      this.references = new ArrayList();
   }

   public Word(string content, int freq){
      this.content = content;
      this.freq = freq;
      this.references = new ArrayList();
   }

   public Word(string content, int freq, int freqFile){
      this.content = content;
      this.freq = freq;
      this.freqFile = freqFile;
      this.references = new ArrayList();
   }
}
