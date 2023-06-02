using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSpawn : MonoBehaviour{
    public GameObject prefabToInstantiate;
    public float spawnInterval = 30f;
    private GameObject currentInstance;

    private void Start(){
        if (prefabToInstantiate == null){
            enabled = false;
            return;
        }
        if (spawnInterval <= 0f){
            spawnInterval = 30f;
        }
        SpawnPrefab();
        InvokeRepeating("ReplacePrefab", spawnInterval, spawnInterval);
    }
    private void SpawnPrefab(){
        currentInstance = Instantiate(prefabToInstantiate, transform.position, transform.rotation);
    }
    private void ReplacePrefab(){
        Destroy(currentInstance);
        SpawnPrefab();
    }
}
