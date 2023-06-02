using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
{
    private ScoreHandler scoreHandler;
    private bool hasCollided = false;
    private NavMeshAgent bossAgent;
    private GameObject player;
    private float rotationSpeed = 200f;

    private void Start()
    {
        scoreHandler = FindObjectOfType<ScoreHandler>();
        if (scoreHandler == null)
        {
            Debug.LogError("ScoreHandler not found in the scene.");
        }
        bossAgent = GetComponent<NavMeshAgent>();
        if (bossAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found.");
        }
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found.");
        }
        UIGame uiGame = FindObjectOfType<UIGame>();
        if (uiGame != null)
        {
            uiGame.ShowOverlayImage();
        }
    }

    private void Update()
    {
        if (bossAgent != null && player != null)
        {
            bossAgent.SetDestination(player.transform.position);
        }
        
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasCollided)
        {
            hasCollided = true;
            Destroy(gameObject);
            if (scoreHandler != null)
            {
                Debug.Log("Player Touched Boss");
                GameManager.deathMessage = "Boss Touched player";
                SceneManager.LoadScene("DeathScreen");
            }
            else
            {
                Debug.LogError("ScoreHandler not found.");
            }
        }
    }
}
