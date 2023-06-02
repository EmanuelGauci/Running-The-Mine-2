using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    private GameObject currentInstance;

    public void SpawnPrefab()
    {
        currentInstance = Instantiate(prefabToInstantiate, transform.position, transform.rotation);
    }
}
