using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player {

    [SerializeField] const int hp = 50;
    [SerializeField] const int armourClass= 17;
    [SerializeField] const int damageRoll = 8;

    public Warrior()
        : base(hp, armourClass, damageRoll)
    {
    }
}
