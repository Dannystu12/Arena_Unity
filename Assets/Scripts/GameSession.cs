using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    [SerializeField] float loadDelay = 1f;
    [SerializeField] GameObject player;

    SceneLoader sceneLoader;

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
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void KillPlayer()
    {
        sceneLoader.LoadNextSceneAfter(loadDelay);
    }
}
