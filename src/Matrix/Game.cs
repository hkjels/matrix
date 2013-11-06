
//
// This is where the magic happens! I've created a basic game-loop that
// refreshes at every 30 millisecond; that's roughly 30 fps. Accuracy is
// measured in percentage of hits vs misses. The score is as simple as
// one point per character hit and one negative point for misses. If you
// choose novice-mode every miss has a double negative count and advanced
// uses triple negative count.
//

// Module dependencies

using System;
using System.Linq;
using System.Threading;

namespace Matrix
{

  sealed class Game
  {

    // Game difficulty

    public enum Difficulty
    {
      Beginner,
      Novice,
      Advanced
    };

    // States that a game can be in

    public enum State
    {
      Introduction,
      LevelUp,
      Running,
      Paused,
      Ended,
      Quit
    };

    // Game-loop update-frequency in milliseconds

    private byte Frequency = 30;

    // Delay in-between levels in seconds

    private byte LevelDelay = 3;

    // Retrieve a random plain or fancy word

    private Word Word = new Word();

    // The entire game-state

    private World World;

    // A little artwork

    private Display Display = new Display();

    // Game-constructor

    public Game(World world)
    {
      // There's not much needed to be constructed ATM as you can assign
      // properties directly without using a constructor and I'm not really
      // planing on using this code to create other games. However, it
      // could be cool with a few preferences at some point. Ex. A socket
      // to stream to for multi-player mode or color-theme etc.

      this.World = world;
      this.World.Level = 1;
    }

    // The `GameLoop` simply churns away to infinity if not stopped. It
    // will go at the rate of our update-frequency and update the world
    // according to user-input.

    public void Start()
    {
      int previous = Environment.TickCount & Int32.MaxValue;
      while (this.World.State != Game.State.Quit)
      {
        int current = Environment.TickCount & Int32.MaxValue;
        int elapsed = current - previous;
        previous = current;

        // Make changes to the world from user-input and render the result
        this.ProcessUserInput()
            .UpdateWorld(elapsed)
            .Render();

        // Update frequency
        Thread.Sleep(this.Frequency);
      }
    }

    // Checks user-input for a single iteration of our game-loop.
    // Input is stored, so that the world can be updated and rendered.

    private Game ProcessUserInput()
    {
      if (Console.KeyAvailable)
      {
        ConsoleKeyInfo key = Console.ReadKey(true);
        this.World.CurrentInput.Enqueue(key.KeyChar);
      }
      return this;
    }

    // Modifies current game-state according to user-input

    private Game UpdateWorld(int elapsed)
    {
      World world = this.World;
      switch (world.State)
      {

        case Game.State.Introduction :
          if (world.CurrentInput.Count() > 0)
          {
            if (world.CurrentInput.Contains('b'))
              world.Difficulty = Game.Difficulty.Beginner;
            else if (world.CurrentInput.Contains('n'))
              world.Difficulty = Game.Difficulty.Novice;
            else if (world.CurrentInput.Contains('a'))
              world.Difficulty = Game.Difficulty.Advanced;
            world.State++;
          }
          break;

        case Game.State.LevelUp :
          world.Level++;
          world.State++;
          break;

        case Game.State.Running :
          Random random = new Random();
          bool spawnCluster = (random.Next(0, 5) * -1) > 0;

          // Hard-coded as 1/5 chance of there being spawned a cluster and
          // 1/5 chance of there being a word in that spawned cluster.
          if (spawnCluster)
          {
            bool withWord = (random.Next(0, 5) * -1) > 0;
            if (withWord)
            {
              switch (world.Difficulty)
              {
                case Game.Difficulty.Beginner :
                  world.Words.Enqueue(this.Word.Plain());
                  break;
                case Game.Difficulty.Novice :
                case Game.Difficulty.Advanced :
                  world.Words.Enqueue(this.Word.Any());
                  break;
              }
            }
          }

          if (world.CurrentInput.Count > 0)
          {
          }
          break;

        case Game.State.Paused :
          // This should probably be triggered by <C-z>. That means the
          // SIGTERM signal must be trapped and re-applied after the game
          // goes to a stop.
          break;

        case Game.State.Ended :
          break;
      }

      return this;
    }

    // Draws the Matrix

    private Game Render()
    {
      World world = this.World;

      switch (world.State)
      {

        // Introduction is just a static screen that requires input about
        // the level of difficulty of the game. Since the screen ATM is
        // static, we do not need to refresh on every tick.

        case Game.State.Introduction :
          Console.Clear();
          MatrixConsole.Write(this.Display.Introduction());
          Console.ResetColor();
          break;

        // Level up is a little pause you get every now and then, so your
        // brain-juice doesn't vaporize immediately. The screen is static
        // ATM and so, does not need to be refreshed on every tick.

        case Game.State.LevelUp :
          Console.Clear();
          MatrixConsole.Write(this.Display.Level(world.Level));
          Console.ResetColor();
          Thread.Sleep(this.LevelDelay * 1000);
          break;

        // Running, is the actual game-play. Here the screen is totally
        // dynamic and the app really comes alive.

        case Game.State.Running :
          Console.Clear();
          MatrixConsole.Prompt(world.CurrentInput.ToString(), world.CurrentWord);
          Console.ResetColor();
          break;

        case Game.State.Ended :
          Console.Clear();
          MatrixConsole.Write(this.Display.GameOver(world.Accuracy(), world.Score()));
          Console.ResetColor();
          break;

      }

      return this;
    }

    /*
    private bool Shoot(string letter)
    {
      if (string.IsNullOrEmpty(letter))
      {
        return false;
      }

      World world = this.World;

      if (string.IsNullOrEmpty(world.CurrentWord))
      {
        // Pluck a word from list of words based on first character
        // world.CurrentWord = world.Words.Where(
        //   s => s.StartsWith(letter)
        // ).First();

        // Word was chosen
        if (!string.IsNullOrEmpty(world.CurrentWord))
        {
          world.CurrentInput = letter;
          world.Hits++;
          return true;
        }
      }

      string word = world.CurrentWord;
      if (word[world.CurrentInput.Length + 1].Equals(letter))
      {
        world.CurrentInput = world.CurrentInput + letter;
        world.Hits++;
        return true;
      }

      return false;
    }
    */

  }

}

