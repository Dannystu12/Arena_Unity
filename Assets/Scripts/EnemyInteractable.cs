using UnityEngine;
using UnityEngine.AI;


public class EnemyInteractable : Interactable
{

    GameSession gameSession;
    Character thisCharacter;

    private void Start()
    {
        gameSession = GameSession.instance;
        thisCharacter = GetComponent<Character>();
    }


    public override void Interact()
    {
        base.Interact();
        CharacterCombat playerCharacter = gameSession.GetPlayer().GetComponent<CharacterCombat>();
        if (playerCharacter != null)
        {
            playerCharacter.Attack(thisCharacter);
        }
    }

}