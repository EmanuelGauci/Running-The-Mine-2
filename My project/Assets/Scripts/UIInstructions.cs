using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInstructions : MonoBehaviour{
    [SerializeField] private Button playButton;
    private void Start(){
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }
    private void OnPlayButtonClicked(){
        SceneManager.LoadScene("TitleScreen");
    }
}