using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy {

    [SerializeField] const int healthRoll = 8;
    [SerializeField] const int armouClass = 9;
    [SerializeField] const int damageRoll = 10;

    public Goblin()
        : base(Dice.Roll(healthRoll) + 4, armouClass, damageRoll)
    {

    }
}
