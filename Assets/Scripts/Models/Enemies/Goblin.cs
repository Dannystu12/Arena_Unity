using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy {

    [SerializeField] const int health = 12;
    [SerializeField] const int armouClass = 9;
    [SerializeField] const int damageRoll = 10;

    public Goblin()
        : base(health, armouClass, damageRoll)
    {

    }
}
