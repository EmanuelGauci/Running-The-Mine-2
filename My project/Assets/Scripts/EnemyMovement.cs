using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour{
    public float minDistance = 10f, maxDistance = 20f; // Reference to distances
    public Transform playerTransform; // Reference to player
    public float followDistance = 5f, movementSpeed = 5f; // Reference to movement variables
    private NavMeshAgent navMeshAgent; // Reference to NavMeshAgent
    private ScoreHandler scoreHandler; // Reference to the ScoreHandler script
    private bool isLifeReductionActive = false; // Flag to indicate if life reduction is active
    public float lifeReductionInterval = 30f, lifeReductionTimer = 0f; // Handle life reduction timing
    public bool followingPlayer = false; // Check if enemy is following player
    public int enemyCount = 0; // Track number of enemies

    void Start(){
        navMeshAgent = GetComponent<NavMeshAgent>(); // Find the NavMeshAgent component in the scene
        scoreHandler = FindObjectOfType<ScoreHandler>(); // Find the ScoreHandler component in the scene
        TeleportToRandomPoint(); //disabled for testing purposes
    }
    void TeleportToRandomPoint(){
        NavMeshHit hit;
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
        randomDirection += transform.position;
        Vector3 playerPosition = playerTransform.position;
        while (Vector3.Distance(randomDirection, playerPosition) < minDistance){
            randomDirection = Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
            randomDirection += transform.position;
        }
        if (NavMesh.SamplePosition(randomDirection, out hit, maxDistance, NavMesh.AllAreas)){
            transform.position = hit.position;
        }
    }
    void StartLifeReductionTimer(){
        // Method to start the life reduction timer
        isLifeReductionActive = true;
        lifeReductionTimer = 0f;
    }
    void StopLifeReductionTimer(){
        // Method to stop the life reduction timer
        isLifeReductionActive = false;
    }
    void FollowPlayer(){
        // Method to make the enemy follow the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < followDistance){
            if (!isLifeReductionActive){
                StartLifeReductionTimer(); // Start reducing player lives if the enemy starts following
            }
            Vector3 playerDirection = playerTransform.position - transform.position;
            playerDirection.y = 0f;
            playerDirection.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
            transform.rotation = targetRotation;
            Vector3 targetPosition = playerTransform.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);
            navMeshAgent.SetDestination(playerTransform.position); // Set the destination to player's position
        }else{
            StopLifeReductionTimer(); // Stop reducing player lives if the enemy stops following
        }
        followingPlayer = distanceToPlayer < followDistance;
    }
    void Update(){
        FollowPlayer(); // Continuously check if the enemy should follow the player
        if (isLifeReductionActive){
            lifeReductionTimer += Time.deltaTime;
            if (lifeReductionTimer >= lifeReductionInterval){
                lifeReductionTimer = 0f;
                scoreHandler.PlayerLives -= 2;
            }
        }if (scoreHandler.EnemyHealth <= 0){
            Destroy(gameObject);
        }
    }
}