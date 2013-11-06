
//
// With *Word* you can retrieve a random word, either a plain or a fancy
// one. Fancy being somewhat more difficult to type.
//
// ### Usage
//
//     Word word = new Word();
//     Console.WriteLine(word.Any());
//

// Module dependencies

using System;

class Word
{

  // Simple / plain words

  string[] _plain = new string[] {
    "anderson",
    "blue",
    "brown",
    "cypher",
    "dodge",
    "green",
    "morpheus",
    "neo",
    "pill",
    "smith",
    "tank"
  };

  // Fancy / more complicated words

  string[] _fancy = new string[] {
    "Architect",
    "B-166ER",
    "EMP",
    "Oracle",
    "Sentinel",
    "Zion"
  };

  // Retrieve a string from a random position in a string Array

  private string RandomString(string[] arr)
  {
    Random random = new Random();
    int count = (int) arr.Length;
    uint rand = (uint) random.Next(0, count);
    return arr[rand];
  }

  // Retrieve a plain word

  public string Plain()
  {
    return this.RandomString(this._plain);
  }

  // Retrieve a fancy word

  public string Fancy()
  {
    return this.RandomString(this._fancy);
  }

  // Retrieve a fancy or plain word

  public string Any()
  {
    Random random = new Random();
    return random.Next(0, 1) == 1
           ? this.Plain()
           : this.Fancy();
  }

}

