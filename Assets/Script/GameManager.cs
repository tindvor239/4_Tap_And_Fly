using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] SpawnObstacle[] spawnObstacles;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] GameObject background;

    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject onPlaying;
    [SerializeField] GameObject gameMenu;
    [SerializeField] Image medal;
    [SerializeField] Text currentScore;
    [SerializeField] Text newScore;
    [SerializeField] Text newHighScore;
    [SerializeField] RawImage newScoreImage;
    [SerializeField] Sprite noobMedal;
    [SerializeField] Sprite averageMedal;
    [SerializeField] Sprite superiorMedal;

    //Resolution Values.
    [SerializeField] Canvas mainCanvas;
    [SerializeField] GameObject gameScene;
    float currentWidth;
    float defaultHeight = 800f;
    float currentHeight;
    float defaultWidth = 1491.842f;
    Vector3 defaultScale;
    Vector3 currentScale;
    /**End**/

    //Bird fly out the scene.
    float flyOutDelay = 0.5f;
    public bool isOutSide;
    /**End**/
    [SerializeField] Vector3 startPosition;
    Camera mainCamera;

    public GameObject player;
    public enum GameState { GameMenu, GameOver, GetReady, OnPlaying };
    public GameState gameState = GameState.OnPlaying;

    public int score = 0;
    int highScore;

    int spawnCase;
    int doubleSpawn = 2;
    int eachSpawn = 0;
    int limitSpawnLength = 10;

    float spawnValueDelay;
    float minSpawnDelayTime = 2f;
    float maxSpawnDelayTime = 5f;
    int maxSpawnRange = 1;

    int lastScore;

    float backgroundFixedOffset = 18.62f;
    float outSceneOffSet = -0.5f;

    #region Singleton
    public static GameManager instance;
    void Awake()
    {
        instance = this;
        return;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        defaultScale = gameScene.transform.localScale;
        gameState = GameState.GameMenu;
        startPosition = player.transform.position;
        mainCamera = Camera.main;
        backgrounds = new GameObject[2];
        backgrounds[0] = background;
        LoadBackground();
        if (PlayerPrefs.GetInt("highScore", highScore) != 0)
        {
            highScore = PlayerPrefs.GetInt("highScore", highScore);
        }
        else
        {
            PlayerPrefs.SetInt("highScore", 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        BackgroundLoop();
        currentWidth = GetCanvasWidth();
        currentHeight = GetCanvasHeight();
        if (defaultWidth != currentWidth)
        {
            ResolutionChanged();
        }

        switch (gameState)
        {
            case GameState.GameOver:
                OnGameOver();
                newScore.text = score.ToString();
                newHighScore.text = highScore.ToString();
                SetPlayerRank();
                StorgeHighScore();

                if (startPosition.x <= player.transform.position.x) //when restart replace the bird not expose background blank space.
                {
                    startPosition = new Vector3(player.transform.position.x, startPosition.y, startPosition.z);
                }
                break;

            case GameState.OnPlaying:
                OnPlaying();
                Vector2 viewPosition = mainCamera.WorldToViewportPoint(player.transform.position);
                if (viewPosition.y >= 1.0f)
                {
                    isOutSide = true;
                }
                flyOutDelay -= Time.deltaTime;

                if (flyOutDelay <= 0.0f)
                {
                    isOutSide = false;
                    flyOutDelay = 0.5f;
                }
                SpawnManager();
                LevelHarder();
                currentScore.text = score.ToString();
                break;

            case GameState.GetReady:
                score = 0;
                OnGetReady();
                if (Input.GetMouseButtonDown(0))
                {
                    tutorial.SetActive(false);
                    gameState = GameState.OnPlaying;
                }
                break;
            case GameState.GameMenu:
                OnGameMenu();
                break;
            default:
                break;
        }
    }

    private void OnGameOver()
    {
        onPlaying.SetActive(false);
        tutorial.SetActive(false);
        gameMenu.SetActive(false);
        gameOver.SetActive(true);
    }

    private void OnGetReady()
    {
        onPlaying.SetActive(false);
        tutorial.SetActive(true);
        gameMenu.SetActive(false);
        gameOver.SetActive(false);
    }

    private void OnGameMenu()
    {
        onPlaying.SetActive(false);
        tutorial.SetActive(false);
        gameMenu.SetActive(true);
        gameOver.SetActive(false);
    }

    private void OnPlaying()
    {
        tutorial.SetActive(false);
        gameMenu.SetActive(false);
        gameOver.SetActive(false);
        onPlaying.SetActive(true);
    }

    private void StorgeHighScore()
    {
        if (score >= highScore)
        {
            newScoreImage.gameObject.SetActive(true);
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        else
        {
            newScoreImage.gameObject.SetActive(false);
        }
    }

    private void SetPlayerRank()
    {
        if (score < 10)
        {
            medal.sprite = noobMedal;
        }
        else if (score >= 20 && score < 50)
        {
            medal.sprite = averageMedal;
        }
        else
        {
            medal.sprite = superiorMedal;
        }
    }

    void SpawnManager()
    {

        if (spawnObstacles != null)
        {
            spawnValueDelay -= Time.deltaTime;
            if (spawnValueDelay <= 0f)
            {
                spawnCase = Random.Range(eachSpawn, doubleSpawn);
                print("Spawn Case: " + spawnCase);
                switch (spawnCase)
                {
                    case 0:
                        SpawnEach();
                        break;
                    case 1:
                        SpawnDouble();
                        break;
                    default:
                        break;
                }

            }
        }
        else
        {
            print("spawn obstacles is null, are you missing something?");
        }
    }

    void SpawnDouble()
    {
        spawnObstacles[0].randomObstacleIndex = Random.Range(0, spawnObstacles[0].obstacles.Length);
        //print("Down:" + spawnObstacles[0].randomObstacleIndex);
        //print("in SpawnDouble");
        spawnObstacles[1].randomObstacleIndex = Random.Range(0, limitSpawnLength - spawnObstacles[1].obstacles.Length);
        //print("Up:" + spawnObstacles[1].randomObstacleIndex);
        spawnObstacles[0].isSpawning = true;
        spawnObstacles[1].isSpawning = true;
        spawnValueDelay = Random.Range(minSpawnDelayTime, maxSpawnDelayTime);
    }

    void SpawnEach()
    {
        int randomIndex = Random.Range(0, maxSpawnRange);
        print(randomIndex);
        spawnObstacles[randomIndex].isSpawning = true;
        spawnValueDelay = Random.Range(minSpawnDelayTime, maxSpawnDelayTime);
    }

    void BackgroundLoop()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            Vector2 viewPosition = mainCamera.WorldToViewportPoint(backgrounds[i].transform.position);
            if (viewPosition.x <= outSceneOffSet)
            {
                backgrounds[i].transform.position = new Vector3(backgrounds[i].transform.position.x + backgroundFixedOffset * 2, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
            }
        }
    }

    void LoadBackground()
    {
        GameObject clone = Instantiate(background) as GameObject;
        clone.transform.parent = gameScene.transform;
        clone.transform.localScale = background.transform.localScale;
        clone.transform.position = new Vector3(backgroundFixedOffset, background.transform.position.y, background.transform.position.z);
        backgrounds[1] = clone;
    }

    public void StartGame()
    {
        gameState = GameState.GetReady;
        player.transform.position = startPosition;
    }

    public void BackMainMenu()
    {
        ReloadLevel();
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void LevelHarder()
    {
        if (lastScore < score)
        {
            lastScore = score;
            if (maxSpawnDelayTime > 1.0f)
            {
                maxSpawnDelayTime -= 0.1f;
            }
            if (minSpawnDelayTime > 0.1f)
            {
                minSpawnDelayTime -= 0.1f;
            }
        }
    }

    float GetCanvasWidth()
    {
        return mainCanvas.GetComponent<RectTransform>().rect.width;
    }

    float GetCanvasHeight()
    {
        return mainCanvas.GetComponent<RectTransform>().rect.height;
    }

    void ResolutionChanged()
    {
        if (defaultWidth > currentWidth)
        {
            float resolutionWidthOffset = defaultWidth / currentWidth;
            float resolutionHeightOffset = defaultHeight / currentHeight;
            currentScale = new Vector3 (defaultScale.x / resolutionWidthOffset, defaultScale.y / resolutionHeightOffset);

            for (int i = 0; i < spawnObstacles.Length; i++)
            {
                spawnObstacles[i].GetComponent<FollowPlayer>().xOffset /= resolutionWidthOffset;
            }
            mainCamera.GetComponent<FollowPlayer>().xOffset /= resolutionWidthOffset;
            outSceneOffSet *= resolutionWidthOffset;

        }
        else
        {
            float resolutionWidthOffset = currentWidth / currentWidth;
            float resolutionHeightOffset = currentHeight / currentHeight;
            currentScale = new Vector3 (defaultScale.x * resolutionWidthOffset, defaultScale.y * resolutionHeightOffset);

            for (int i = 0; i < spawnObstacles.Length; i++)
            {
                spawnObstacles[i].GetComponent<FollowPlayer>().xOffset /= resolutionWidthOffset;
            }
            mainCamera.GetComponent<FollowPlayer>().xOffset /= resolutionWidthOffset;
            outSceneOffSet /= resolutionWidthOffset;

        }
        defaultWidth = currentWidth;
    }
}
