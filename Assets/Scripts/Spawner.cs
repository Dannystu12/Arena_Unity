using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] GameObject[] enemies;
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] float spawnMaxWait;
    [SerializeField] float spawnMinWait;
    [SerializeField] int startWait;

    // Use this for initialization
    void Start () {
        StartCoroutine(WaitAndSpawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            int randEnemy = Random.Range(0, enemies.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            GameObject enemy = Instantiate(enemies[randEnemy]);
            enemy.transform.position = spawnPoints[randSpawnPoint].transform.position;
            yield return new WaitForSeconds(Random.Range(spawnMinWait, spawnMaxWait));
        }
    }
}
