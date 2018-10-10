using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour {

    [SerializeField] AudioClip[] commentary;
    [SerializeField] AudioClip start;
    [SerializeField] AudioClip end;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
    }

    public void PlayStart()
    {
        audioSource.PlayOneShot(start);
    }

    public void PlayEnd()
    {
        audioSource.PlayOneShot(end);
    }

    public void PlayRandom()
    {
        int index = Random.Range(0, commentary.Length);
        audioSource.PlayOneShot(commentary[index]);
    }
}
