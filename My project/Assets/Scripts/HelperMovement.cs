using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperMovement : MonoBehaviour{
    public Transform movePositionTransform;
    public float movementSpeed = 5f;
    public float maxDistanceFromPlayer = 10f;
    public float rotationSpeed = 90f; // Rotation speed in degrees per second
    private NavMeshAgent navMeshAgent;
    private ScoreHandler scoreHandler;
    private EnemyMovement[] enemyMovements;
    private bool isAnyEnemyFollowing = false;
    private int previousAmmoCount = 0; // Previous ammo count
    public ParticleSystem enemyCloseParticleSystem;
    public ParticleSystem enemyFollowingPlayerParticleSystem;
    public AudioSource mainSoundtrack;
    public AudioSource enemyFollowingSoundtrack;
    private int repetitionStopper = 0;

    private void Awake(){
        navMeshAgent = GetComponent<NavMeshAgent>();
        scoreHandler = FindObjectOfType<ScoreHandler>();
        enemyMovements = FindObjectsOfType<EnemyMovement>();
        enemyCloseParticleSystem.Stop();
        enemyFollowingPlayerParticleSystem.Stop();
        mainSoundtrack.Play();
        enemyFollowingSoundtrack.Stop();
    }
    private void Update(){
        Vector3 targetPosition = movePositionTransform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);
        navMeshAgent.SetDestination(transform.position);
        int currentAmmoCount = scoreHandler.PlayerAmmo;
        int ammoIncrease = currentAmmoCount - previousAmmoCount;
        DetectEnemyClose();
        if (isAnyEnemyFollowing && ammoIncrease > 0){
            int enemyHealthReduction = ammoIncrease; // Reduce enemy health based on the ammo increase
            scoreHandler.ReduceEnemyHealth(enemyHealthReduction);
        }
        previousAmmoCount = currentAmmoCount;
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // Rotate the helper continuously
    }
    private void DetectEnemyClose(){
        isAnyEnemyFollowing = false;
        foreach (EnemyMovement enemyMovement in enemyMovements){
            if (enemyMovement != null){
                float distanceToEnemy = Vector3.Distance(transform.position, enemyMovement.transform.position);
                if (distanceToEnemy < maxDistanceFromPlayer){
                    if (enemyMovement.followingPlayer){
                        enemyFollowingPlayerParticleSystem.Play();
                        enemyCloseParticleSystem.Stop();
                        isAnyEnemyFollowing = true;
                        PlayEnemyFollowingSoundtrack();
                        repetitionStopper ++;
                    }else if(repetitionStopper < 1){
                        enemyCloseParticleSystem.Play();
                        enemyFollowingPlayerParticleSystem.Stop();
                    }
                }else{
                    enemyFollowingPlayerParticleSystem.Stop();
                    enemyCloseParticleSystem.Stop();
                }
            }
        }
    }
    private void PlayEnemyFollowingSoundtrack(){
        if (!enemyFollowingSoundtrack.isPlaying){
            mainSoundtrack.Stop();
            enemyFollowingSoundtrack.Play();
        }
    }
}