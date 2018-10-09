using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {

    public static int Roll(int sides)
    {
        return sides <= 0 ? 0 : Random.RandomRange(0, sides + 1);
    }
}
