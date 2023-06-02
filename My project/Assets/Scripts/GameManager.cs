using UnityEngine;

public class GameManager : MonoBehaviour{
    public static string deathMessage;
    public static int finalScore;
    private ScoreHandler scoreHandler;
    
    private void Start(){
        scoreHandler = FindObjectOfType<ScoreHandler>();
    }
    private void Update(){
        if (scoreHandler != null){
            finalScore = scoreHandler.PlayerAmmo;
        }
    }
}
