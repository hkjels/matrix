
//
// `Matrix.MatrixConsole` makes it slightly easier to work with raining
// text in the `Console` due to the layering and inline color options.
//

// Module dependencies

using System;
using System.Collections.Generic;

namespace Matrix
{

  public static class MatrixConsole
  {

    // Write
    //
    // Same as Console.Write except you can add a block-statement with a
    // foreground-color.
    // TODO Benchmark against a "RegExp with callback" solution

    public static void Write(string message)
    {
      bool block = false;
      string color = "";

      foreach (char letter in message)
      {
        if (letter.Equals('{'))
        {
          block = true;
          continue;
        }
        if (block)
        {
          if (letter.Equals('}'))
          {
            block = false;
            Console.ForegroundColor = (ConsoleColor) System.Enum.Parse(typeof(ConsoleColor), color);
            color = "";
            continue;
          }
          color = color + letter;
          continue;
        }
        Console.Write(letter);
      }
    }

    // Write on top
    //
    // Same as Console.Write except spaces are just padded so you can layer
    // text.

    public static void WriteOnTop(string message)
    {
      foreach (char letter in message)
      {
        if (letter.Equals(' '))
        {
          Console.CursorLeft++;
          continue;
        }
        Console.Write(letter);
      }
    }

    // Prompt
    //
    // A little "command-prompt" if you like. Where are the letters you
    // type will gather up.
    //
    // I will probably move `Prompt` out of the `MatrixConsole` and into
    // `Display` where it's simply returned as a string, but it's more
    // cumbersome ATM since it needs to be positioned at the bottom edge.

    public static void Prompt(string input, string word)
    {
      int center = Console.WindowWidth / 2;

      Console.CursorLeft = 0;
      Console.CursorTop = Console.WindowHeight - 1;
      Console.BackgroundColor = ConsoleColor.DarkGreen;
      Console.ForegroundColor = ConsoleColor.Black;

      // Last input
      Console.Write(input[input.Length]);

      // Word input
      if (!string.IsNullOrEmpty(word) && input.Length > 0)
      {
        Console.CursorLeft = center;
        Console.Write(word);
        Console.CursorLeft = center;
        Console.ForegroundColor = ConsoleColor.Green;

        foreach (char letter in input)
        {
          if (word.IndexOf(letter) != -1) MatrixConsole.WriteOnTop(letter.ToString());
        }
      }
    }

  }

}

