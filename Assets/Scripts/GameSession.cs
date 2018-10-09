using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    #region Singleton
    public static GameSession instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] GameObject player;
    public GameObject GetPlayer()
    {
        return player;
    }
}
