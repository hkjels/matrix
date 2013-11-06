
//
// *Welcome to the matrix*. This is the main entry-point for the game. It
// will instantiate the `Game`-class with whatever preferences needed and
// get the game started.
//

// Module dependencies

using System;

namespace Matrix
{

  sealed class Matrix
  {

    public static void Main()
    {
      World world = new World();
      Game game = new Game(world);
      game.Start();
    }

  }

}

