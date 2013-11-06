
//
// *Display* is a bunch of artwork used for game-play.
//

// Module dependencies

using System;
using System.Linq;
using System.Collections.Generic;

namespace Matrix
{

  class Display
  {

    // Gameplay information and introduction

    public string Introduction()
    {
      string message = @"

                /:        `m/       .+/h/+d+//+y   `/--/:`    `-ss.          `/:.
    `sm+-      :yy/`     `hNN:      /.   hM.   ` `:No   /d      mm  `-smd.   :o`
      h/N-    :+:M+     `y:-MN`          hM.      `Ms..`.d      +N+`   :Ns`.+-
      h +No  -+ -M/    `d.  sMo         .dM:--    `My:oso/     .+M/     .hmm`
      h  oMs.+  +M/   `ds///+NMo:        hM.      `Mo .dh`      +N:    `/syym-
      h   -No   hM:   y+`````.oMm.       hM:.     `Mo   oNNy`   dd     `/-  +No
    `-m:`  `  `.+o   /o        hN:       dN/.     .Ny.`  ./:. `-dd-` `/+    `sdy-`
                  --`         `:sss/.   `-/`      `--                 :/-`


        Welcome to the Matrix. Type the falling words as fast as you can and
        don't let them reach the edge of your screen. The more letters you
        shoot of, the higher your score will be.

        Choose your pill!

        (b) Beginner
        (n) Novice
        (a) Advanced

      ";
      return message;
    }

    // TODO Need to properly suffix with level-number
    //    | I guess with a two-dimensional array of [Level] and [#]
    //    | compose the two in this method and return as string.

    public string Level(int level)
    {
      string message = String.Format(@"

      `7MMF'                                `7MM
        MM                                    MM
        MM         .gP'Ya `7M'   `MF'.gP'Ya   MM
        MM        ,M'   Yb  VA   ,V ,M'   Yb  MM
        MM      , 8M''''''   VA ,V  8M''''''  MM
        MM     ,M YM.    ,    VVV   YM.    ,  MM
      .JMMmmmmMMM  `Mbmmd'     W     `Mbmmd'.JMML. {0}

      ", level);
      return message;
    }

    public string GameOver(double accuracy, uint score)
    {
      string message = String.Format(@"

       .g8'''bgd
      .dP'     `M
      dM'       `  ,6'Yb.  `7MMpMMMb.pMMMb.  .gP'Ya       ,pW'Wq.`7M'   `MF'.gP'Ya `7Mb,od8
      MM          8)   MM    MM    MM    MM ,M'   Yb     6W'   `Wb VA   ,V ,M'   Yb  MM'
      MM.    `7MMF',pm9MM    MM    MM    MM 8M''''''     8M     M8  VA ,V  8M''''''  MM
      `Mb.     MM 8M   MM    MM    MM    MM YM.    ,     YA.   ,A9   VVV   YM.    ,  MM
        `'bmmmdPY `Moo9^Yo..JMML  JMML  JMML.`Mbmmd'      `Ybmd9'     W     `Mbmmd'.JMML.

        Accuracy: {0}%
            Score: {1}

      ", accuracy, score);
      return message;
    }

    // Cluster of raining letters with a word in it

    public string WordCluster(string word, int atLine)
    {
      Random random = new Random();
      int randomMaxWidth = random.Next(word.Length, word.Length * 2);
      string cluster = this.Cluster(word.Length, randomMaxWidth, 300);
      word = "{White}" + word;

      string[] newline = { Environment.NewLine };
      List<string> lines = cluster.Split(newline, StringSplitOptions.None).ToList();
      lines.Insert(lines.Count - atLine, word);

      string wordCluster = string.Join("\n", lines.ToArray());
      return wordCluster;
    }

    // Cluster of raining letters

    public string Cluster(int minWidth, int maxWidth, int Height)
    {
      Random random = new Random();
      int letterCount = random.Next(minWidth * Height, maxWidth * Height);
      string cluster = "";

      for (int i = 0; i < letterCount; i++)
      {
        char randomLetter = (char) random.Next(0x4e00, 0x4f80);
      }

      return cluster;
    }

  }

}

