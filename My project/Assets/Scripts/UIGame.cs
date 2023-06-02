using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIGame : MonoBehaviour
{
    public TextMeshProUGUI ammoText, timeText; // Text related
    [SerializeField] private Slider LivesSlider, EnemyHealthSlider;
    [SerializeField] private Image overlayImage; // Reference to the overlay image
    private float playTime, totalTime = 180f; // Timer related
    private ScoreHandler scoreHandler;

    private void Start()
    {
        scoreHandler = FindObjectOfType<ScoreHandler>();
        playTime = totalTime;
        if (overlayImage != null)
        {
            overlayImage.gameObject.SetActive(false); // Set the overlay image initially inactive
        }
    }

    private void Update()
    {
        UpdateUIFields();
        UpdatePlayTime();
        CheckTimerActivation();
    }

    private void UpdateUIFields()
    {
        if (scoreHandler != null)
        {
            ammoText.text = "Power: " + scoreHandler.PlayerAmmo;
            // Update LivesSlider value
            if (LivesSlider != null)
            {
                LivesSlider.value = scoreHandler.PlayerLives;
            }
            // Update EnemyHealthSlider value
            if (EnemyHealthSlider != null)
            {
                EnemyHealthSlider.value = scoreHandler.EnemyHealth;
            }
        }
        UpdateTimeText();
    }

    private void UpdatePlayTime()
    {
        playTime -= Time.deltaTime;
        if (playTime <= 0f)
        {
            playTime = 0f;
        }
    }

    private void CheckTimerActivation()
    {
        if (playTime <= 0f && scoreHandler.EnemyHealth <= 0){
            SceneManager.LoadScene("WinScreen");
        }
        else if (playTime <= 0f)
        {
            GameManager.deathMessage = "Timer ran out!";
            SceneManager.LoadScene("DeathScreen");
        }
    }

    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(playTime / 60f);
            int seconds = Mathf.FloorToInt(playTime % 60f);
            timeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    public void ShowOverlayImage()
    {
        if (overlayImage != null)
        {
            overlayImage.gameObject.SetActive(true);
        }
    }
}
