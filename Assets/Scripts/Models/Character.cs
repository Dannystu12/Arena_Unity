using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IAttack, Attackable {
    
    protected int hp;
    protected readonly int MAX_HP;
    private int armourClass;
    private int damageRoll;
    private bool lastAttackWasCrit;

    public Character(int hp, int armourClass, int damageRoll)
    {
        this.hp = hp;
        this.MAX_HP = hp;
        this.armourClass = armourClass;
        this.damageRoll = damageRoll;
    }

    public int GetHp()
    {
        return hp;
    }

    public int GetDamageRoll()
    {
        return damageRoll;
    }

    public int GetMAX_HP()
    {
        return MAX_HP;
    }

    public bool LastAttackWasCrit()
    {
        return lastAttackWasCrit;
    }

    public int GetArmourClass()
    {
        return armourClass;
    }

    public bool IsAlive()
    {
        return hp > 0;
    }

    public bool IsDead()
    {
        return !IsAlive();
    }

    public void TakeDamage(int damage)
    {
        damage = Mathf.Min(hp, damage);
        hp -= damage;
        if (hp == 0) Die();
    }

    public void Attack(Attackable attackable)
    {
        int hitRoll = Dice.Roll(20);
        if (hitRoll < attackable.GetArmourClass()) return;

        int damage = GetDamage();
        if (hitRoll == 20)
        {
            damage += GetDamage();
            lastAttackWasCrit = true;
        }
        else
        {
            lastAttackWasCrit = false;
        }

        attackable.TakeDamage(damage);
    }

    private int GetDamage()
    {
        return Dice.Roll(damageRoll);
    }

    public virtual void Die()
    {
    }

}
