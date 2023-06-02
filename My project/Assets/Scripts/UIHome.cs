using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIHome : MonoBehaviour{
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Image overlayImage;

    private void Start(){
        playButton.onClick.AddListener(OnPlayButtonClicked);
        instructionsButton.onClick.AddListener(OnInstructionsButtonClicked);
        overlayImage.gameObject.SetActive(false);
    }
    private void OnPlayButtonClicked(){
        overlayImage.gameObject.SetActive(true);
        SceneManager.LoadScene("GoodGraphics");
    }
    private void OnInstructionsButtonClicked(){
        SceneManager.LoadScene("Instructions");
    }
}
