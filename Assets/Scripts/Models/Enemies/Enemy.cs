using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    
   

    public Enemy(int hp, int armourClass, int damageRoll)
        : base(hp, armourClass, damageRoll)
    {
    }

    public override void Die()
    {
        base.Die();
        GameSession.instance.AddKill();
    }
}
