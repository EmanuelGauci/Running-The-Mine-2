using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour{
    public float movementSpeed = 10.0f;
    public float turnSpeed = 100.0f;
    private ScoreHandler scoreHandler;

    void Update(){
        float movement = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        transform.Translate(0, 0, movement);
        float rotation = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
        if (scoreHandler.PlayerLives <= 0){
            GameManager.deathMessage = "Enemy killed player";
            SceneManager.LoadScene("DeathScreen");
        }
    }
    private void Start(){
        scoreHandler = FindObjectOfType<ScoreHandler>();
    }
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Wall")){
            if (scoreHandler != null){
                GameManager.deathMessage = "Player hit wall";
                SceneManager.LoadScene("DeathScreen");
            }
        }
    }
}
