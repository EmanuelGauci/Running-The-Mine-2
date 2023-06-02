using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIWin : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Image overlayImage;

    private void Start(){
        playButton.onClick.AddListener(OnPlayButtonClicked);
        homeButton.onClick.AddListener(OnHomeButtonClicked);
        DisplayFinalScore();
        overlayImage.gameObject.SetActive(false);
    }
    private void OnPlayButtonClicked(){
        overlayImage.gameObject.SetActive(true);
        SceneManager.LoadScene("GoodGraphics");
    }
    private void OnHomeButtonClicked(){
        overlayImage.gameObject.SetActive(true);
        SceneManager.LoadScene("TitleScreen");
    }
    private void DisplayFinalScore(){
        int finalScore = GameManager.finalScore;
        finalScoreText.text = "Score: " + finalScore.ToString();
    }
}
