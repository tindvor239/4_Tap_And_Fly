using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Image medal;
    [SerializeField] Sprite[] medalRanks;
    [SerializeField] GameObject newScore;
    [SerializeField] Text highScore;
    [SerializeField] Text currentScore;
    #region Singleton
    public static GameOverUI Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    #region Behavior
    public void ShowGameOverUI(int score)
    {
        print(score);
        currentScore.text = score.ToString();
        SetHighScore(score);
        SetPlayerRank(score);
    }
    void SetHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("highScore");
        if (score >= highScore)
        {
            newScore.SetActive(true);
            highScore = score;
            this.highScore.text = highScore.ToString();
            PlayerPrefs.SetInt("highScore", highScore);
        }
        else
        {
            this.highScore.text = PlayerPrefs.GetInt("highScore").ToString();
            newScore.SetActive(false);
        }
    }
    void SetPlayerRank(int score)
    {
        if (score < 10)
        {
            medal.sprite = medalRanks[0];
        }
        else if (score >= 20 && score < 50)
        {
            medal.sprite = medalRanks[1];
        }
        else
        {
            medal.sprite = medalRanks[2];
        }
    }
    #endregion
}
