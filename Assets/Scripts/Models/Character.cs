using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IAttack, Attackable {
    
    protected int hp;
    protected readonly int MAX_HP;
    private int armourClass;
    private int damageRoll;
    private bool lastAttackWasCrit;
    [SerializeField] float deathDelay = 5f;

    public event System.Action<int, int, int, bool> OnHealthChanged;
    public event System.Action OnDeath;

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

    public void TakeDamage(int damage, bool crit)
    {
        if (IsAlive())
        {
            damage = Mathf.Min(hp, damage);
            hp -= damage;
            if (OnHealthChanged != null) OnHealthChanged(MAX_HP, hp, damage, crit);
            if (IsDead()) Die();
        }
    }

    public void Attack(Attackable attackable)
    {
        int hitRoll = Dice.Roll(20);
        if (hitRoll < attackable.GetArmourClass())
        {
            attackable.TakeDamage(0, lastAttackWasCrit);
            return;
        }

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

        attackable.TakeDamage(damage, lastAttackWasCrit);
    }

    private int GetDamage()
    {
        return Dice.Roll(damageRoll);
    }

    public virtual void Die()
    {
        if (OnDeath != null) OnDeath();
        Destroy(gameObject, deathDelay);
    }

}
