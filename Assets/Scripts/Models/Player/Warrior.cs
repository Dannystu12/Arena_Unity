using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player {

    [SerializeField] const int baseHp = 50;
    [SerializeField] const int armourClass= 17;
    [SerializeField] const int damageRoll = 8;

    public Warrior()
        : base(baseHp, armourClass, damageRoll)
    {
    }
}
