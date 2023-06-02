using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreHandler : MonoBehaviour{
    public int PlayerAmmo { get; private set; }
    public int PlayerLives { get; set; }
    public int EnemyHealth { get; set; }
    private BossSpawn bossSpawn; // Reference to the BossSpawn script

    private void Start(){
        PlayerLives = 400;
        EnemyHealth = 250;
        bossSpawn = FindObjectOfType<BossSpawn>(); // Find the BossSpawn script in the scene
    }
    public void IncrementPlayerAmmo(){
        PlayerAmmo++;
    }
    public void ReducePlayerAmmo(int amount){
        PlayerAmmo -= amount;
    }
    public void ReduceEnemyHealth(int amount){
        EnemyHealth -= (amount + 5);
        if (EnemyHealth <= 0 && bossSpawn != null){
            bossSpawn.SpawnPrefab(); // Spawn the boss prefab using the BossSpawn script
        }
    }
}
