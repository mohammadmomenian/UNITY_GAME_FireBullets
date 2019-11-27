using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public GameObject monster;
    public float spawnDelay;

    bool canSpawn;

	void Start () {
        canSpawn = true;
	}
	
	void Update () {
		if (canSpawn)
        {
            StartCoroutine("SpawnMonster");
        }
	}

    IEnumerator SpawnMonster()
    {
        Instantiate(monster, transform.position, Quaternion.identity); // spawns the jumping fish
        canSpawn = false;

        yield return new WaitForSeconds(spawnDelay);

        canSpawn = true;
    }
}
