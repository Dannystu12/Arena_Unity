using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour {

    [SerializeField] float attackSpeed = 1f;
    [SerializeField] float attackCooldown = 0f;

    Character thisCharacter;

    private void Start()
    {
        thisCharacter = GetComponent<Character>();
    }

    private void Update()
    {
        attackCooldown = Mathf.Max(0, attackCooldown - Time.deltaTime);
    }


    public void Attack(Character otherCharacter)
    {
        if(attackCooldown == 0)
        {
            thisCharacter.Attack(otherCharacter);
            attackCooldown = attackSpeed;
        }
        
    }
}
