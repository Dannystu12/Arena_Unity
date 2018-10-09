using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour {

    [SerializeField] float attackSpeed = 1f;
    [SerializeField] float attackCooldown = 0f;
    [SerializeField] float damageDelay = 0.6f;
    [SerializeField] float combatCooldown = 5f;
    float lastAttackTime;

    public bool InCombat { get; private set; }

    Character thisCharacter;


    public event System.Action OnAttack;

    private void Start()
    {
        thisCharacter = GetComponent<Character>();

    }

    private void Update()
    {
        attackCooldown = Mathf.Max(0, attackCooldown - Time.deltaTime);

        if((Time.time - lastAttackTime) > combatCooldown)
        {
            InCombat = false;
        }

    }


    public void Attack(Character otherCharacter)
    {
        if(attackCooldown == 0)
        {
            StartCoroutine(DoDamage(otherCharacter, damageDelay));

            if (OnAttack != null) OnAttack();
            attackCooldown = attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
        
    }

    IEnumerator DoDamage(Character otherCharacter, float delay)
    {
        yield return new WaitForSeconds(delay);
        thisCharacter.Attack(otherCharacter);
        if(otherCharacter.IsDead())
        {
            InCombat = false;
        }
    }
}
