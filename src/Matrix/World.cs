
//
// *World* is the entire game-state for the currently active game. There
// should definitely not be that much computation in here, merely just a
// set of properties.
//

// Module dependencies

using System.Collections.Generic;

namespace Matrix
{

  sealed class World
  {

    // Game internals

    public Game.State State { get; set; }
    public Game.Difficulty Difficulty { get; set; }
    public int Level { get; set; }

    // Words & input

    public Queue<string> Words = new Queue<string>();
    public string CurrentWord { get; set; }
    public Queue<char> CurrentInput = new Queue<char>();

    // Correct character-inputs

    public uint Hits { get; set; }

    // Wrong character-inputs

    public uint Misses { get; set; }

    // Accuracy ratio evaluated as percentage

    public double Accuracy()
    {
      double accuracy = (this.Hits + this.Misses) * (this.Hits * 0.01);
      return accuracy > 0 ? accuracy : 0;
    }

    // Total game score

    public uint Score()
    {
      byte multiplier = 1;
      switch (this.Difficulty) {
        case Game.Difficulty.Novice :
          multiplier = 2;
          break;
        case Game.Difficulty.Advanced :
          multiplier = 3;
          break;
      }
      uint score = this.Hits - (this.Misses * multiplier);
      return score;
    }

  }

}

