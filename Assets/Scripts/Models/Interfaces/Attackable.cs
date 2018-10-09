using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Attackable
{
    int GetArmourClass();
    bool IsAlive();
    bool IsDead();
    void TakeDamage(int damage);
}
