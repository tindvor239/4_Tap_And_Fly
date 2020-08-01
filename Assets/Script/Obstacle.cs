using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    GameManager gameManager;
    GameObject player;
    Camera mainCamera;

    bool isScore = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        player = gameManager.player;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        AddScore();
    }

    void AddScore()
    {
        if (isScore == false)
        {
            if (transform.position.x <= player.transform.position.x)
            {
                gameManager.score++;
                isScore = true;
            }
        }

        switch (gameManager.gameState)
        {
            case GameManager.GameState.GetReady:
                DestroyImmediate(gameObject);
                break;
            case GameManager.GameState.OnPlaying:
                Vector2 viewPosition = mainCamera.WorldToScreenPoint(gameObject.transform.position);
                if (viewPosition.x < -0.1f)
                {
                    DestroyImmediate(gameObject);
                }
                break;
        }
    }
}
