using UnityEngine;
using UnityEngine.AI;


public class EnemyInteractable : Interactable
{
    [SerializeField] GameObject targetIndicatorProjector;
    [SerializeField] AudioClip focusSfx;
    GameSession gameSession;
    Character thisCharacter;
    AudioSource audioSource;


    private void Start()
    {
        gameSession = GameSession.instance;
        thisCharacter = GetComponent<Character>();
        audioSource = GetComponent<AudioSource>();
        targetIndicatorProjector.SetActive(false);
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

    public override void OnFocused(Transform playerTransform)
    {
        base.OnFocused(playerTransform);
        hasInteracted = true;
        targetIndicatorProjector.SetActive(true);
        audioSource.PlayOneShot(focusSfx);
    }

    public override void OnDefocused()
    {
        base.OnDefocused();
        targetIndicatorProjector.SetActive(false);
    }

}