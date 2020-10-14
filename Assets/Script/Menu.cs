using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    GameOverUI gameOver;
    [SerializeField] GameObject tutorialUI;
    [SerializeField] GameObject scoreUI;
    [SerializeField] GameObject mainMenu;

    #region Singleton
    public static Menu Instance;
    private void Awake()
    {
        Instance  = this;
        return;
    }
    #endregion
    #region Properties
    public GameOverUI GameOver
    {
        get { return gameOver; }
    }
    public GameObject TutorialUI
    {
        get { return tutorialUI; }
    }
    public GameObject ScoreUI
    {
        get { return scoreUI; }
    }
    public GameObject MainMenu
    {
        get { return mainMenu; }
    }
    #endregion
    #region Behavior
    void Start()
    {
        gameOver = GameOverUI.Instance;
    }
    public void ShowGameOverMenu()
    {
        gameOver.gameObject.SetActive(true);
        tutorialUI.SetActive(false);
        scoreUI.SetActive(false);
        mainMenu.SetActive(false);
    }
    public void ShowMainMenu()
    {
        gameOver.gameObject.SetActive(false);
        tutorialUI.SetActive(false);
        scoreUI.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void ShowTutorial()
    {
        tutorialUI.SetActive(true);
        scoreUI.SetActive(false);
        mainMenu.SetActive(false);
        gameOver.gameObject.SetActive(false);
    }
    public void ShowScore()
    {
        gameOver.gameObject.SetActive(false);
        tutorialUI.SetActive(false);
        mainMenu.SetActive(false);
        scoreUI.SetActive(true);
    }

    public void SetScore(in int score)
    {
        scoreUI.GetComponentInChildren<Text>().text = score.ToString();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
