using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour{
    private ScoreHandler scoreHandler;
    private bool hasCollided = false;
    public float turnSpeed = 90f;
    private void Start(){
        scoreHandler = FindObjectOfType<ScoreHandler>();
        if (scoreHandler == null){
            Debug.LogError("ScoreHandler not found in the scene.");
        }
    }
    private void Update(){
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && !hasCollided){
            hasCollided = true;
            Destroy(gameObject);
            if (scoreHandler != null){
                scoreHandler.IncrementPlayerAmmo();
            }
            else{
                Debug.LogError("ScoreHandler not found.");
            }
        }
    }
}
