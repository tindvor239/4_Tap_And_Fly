using UnityEngine;

[RequireComponent(typeof(Paralax))]
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Spawner spawner;
    [SerializeField] Vector3 startPosition;
    
    private ushort score;
    private ushort lastScore = 0;
    private Camera mainCamera;
    public enum GameState { GameOver, Ready, Play, Pause };
    private GameState state;
    Paralax paralax;
    #region Singleton
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
        return;
    }
    #endregion
    #region Properties
    public GameObject Player
    {
        get { return player; }
    }
    public GameState State
    {
        get { return state; }
    }
    public ushort Score
    {
        get => score;
    }
    #endregion
    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main;
        mainCamera.aspect = 860 / 800;
        paralax = Paralax.Instance;
        if(player != null)
        {
            paralax.Target = player.transform;
            startPosition = player.transform.position;
        }
        state = GameState.Pause;
    }
    private void Update()
    {
        paralax.Following();
        paralax.ScrollingSteady();
        paralax.ScrollingFarAway();
        switch(state)
        {
            case GameState.Ready:
                if(Input.GetMouseButtonDown(0))
                {
                    state = GameState.Play;
                }
                break;
            case GameState.Play:
                if (player != null)
                {
                    spawner.Spawn();
                    if (player.GetComponent<PlayerController>().Bird.IsAlive == false)
                        state = GameState.GameOver;
                }
                break;
            case GameState.GameOver:
                SetLastScore();

                break;
        }
    }

    private void SetLastScore()
    {
        if (score > lastScore)
        {
            lastScore = score;
        }
    }

    public void AddScore(ushort plusValue)
    {
        score += plusValue;
    }
    #region Button Behaviors
    public void LoadPlayMap()
    {
        state = GameState.Ready;
        player.transform.position = startPosition;
        PlayerController controller = player.GetComponent<PlayerController>();
        score = 0;
        controller.Bird.Alive();
        controller.Bird.ResetSpeed();
        spawner.DeleteAllObstacles();
    }
    public void LoadMainMenu()
    {
        state = GameState.Pause;
    }
    #endregion
}
