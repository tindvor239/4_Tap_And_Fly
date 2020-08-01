using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    GameObject player;

    public float xOffset;
    void Start()
    {
        gameManager = GameManager.instance;
        player = gameManager.player;
        xOffset = transform.position.x - player.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Following();
    }

    void Following()
    {
        switch (gameManager.gameState)
        {
            case GameManager.GameState.GameOver:
                break;
            default:
                transform.position = new Vector3(player.transform.position.x + xOffset, transform.position.y, transform.position.z);
                break;
        }
    }
}
