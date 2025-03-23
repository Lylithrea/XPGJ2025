using TMPro;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    public int highScore { get; private set; }
    
    
    public TextMeshProUGUI highScoreText;
    public GameObject highscorePanel;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void SaveScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    public void SetScore()
    {
        highscorePanel.SetActive(true);
        highScoreText.text = "Points: " + highScore.ToString();
    }
}
