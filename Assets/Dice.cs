using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Dice
{
    private static readonly System.Random rnd = new System.Random();

    static int RollDice()
    {
        return rnd.Next(1, 7);
    }
}
