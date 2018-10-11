using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    [SerializeField] float loadDelay = 1f;
    [SerializeField] GameObject player;
    [SerializeField] int killsBeforeRestore = 10;
    private int kills = 0;

    SceneLoader sceneLoader;
    Spawner spawner;
    Announcer announcer;

    #region Singleton
    public static GameSession instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        spawner = FindObjectOfType<Spawner>();
        announcer = FindObjectOfType<Announcer>();
        announcer.PlayStart();
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void KillPlayer()
    {
        sceneLoader.LoadNextSceneAfter(loadDelay);
        announcer.PlayEnd();
    }

    public int GetKills()
    {
        return kills;
    }

    public void AddKill()
    {
        kills++;
        spawner.UpdateWait();
        if (Random.Range(0, 2) == 1) announcer.PlayRandom();
        if (kills % killsBeforeRestore == 0) RestorePlayerHealth();
    }

    private void RestorePlayerHealth()
    {
        Character playerCharacter = player.GetComponent<Character>();
        playerCharacter.RestoreHealth();
    }
}
