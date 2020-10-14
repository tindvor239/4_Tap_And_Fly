using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicBody : MonoBehaviour
{
    float gravity = 9.8f;
    [SerializeField] float mass;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameManager.State)
        {
            case GameManager.GameState.Ready:
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.GameOver:
                break;
            default:
                transform.position += Vector3.down * gravity * mass * Time.deltaTime;
                break;
        }
    }
}
