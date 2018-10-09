using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    public Player(int hp, int armourClass, int damageRoll)
        :base(hp, armourClass, damageRoll)
    {   
    }

    public override void Die()
    {
        GameSession.instance.KillPlayer();
        Destroy(gameObject);
    }
}
