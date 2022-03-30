using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject spawnPoints;
    public GameObject enemyType;
    public float spawnDelay;
    float nextSpawn;
    Transform[] spawnPointTransforms;


    // Start is called before the first frame update
    void Start()
    {
        spawnPointTransforms = spawnPoints.GetComponentsInChildren<Transform>();

        nextSpawn = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextSpawn & spawnPointTransforms.Length > 1)
        {
            int index = Random.Range(1, spawnPointTransforms.Length - 1);
            Instantiate(enemyType, spawnPointTransforms[index]);
            Debug.Log(spawnPointTransforms[index].position);
            nextSpawn = Time.time + spawnDelay;
        }

    }
}
