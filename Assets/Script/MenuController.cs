using UnityEngine;

[RequireComponent(typeof(Menu))]
public class MenuController : MonoBehaviour
{
    Menu menu;
    GameManager gameManager;
    #region Properties
    public Menu Menu
    {
        get { return menu; }
    }
    #endregion
    void Start()
    {
        menu = Menu.Instance;
        gameManager = GameManager.Instance;
    }
    void Update()
    {
        if(menu != null)
        {
            switch(gameManager.State)
            {
                case GameManager.GameState.GameOver:
                    menu.ShowGameOverMenu();
                    menu.GameOver.ShowGameOverUI(gameManager.Score);
                    break;
                case GameManager.GameState.Ready:
                    menu.ShowTutorial();
                    break;
                case GameManager.GameState.Play:
                    menu.ShowScore();
                    menu.SetScore(gameManager.Score);
                    break;
                case GameManager.GameState.Pause:
                    menu.ShowMainMenu();
                    break;
            }
        }
    }
}
