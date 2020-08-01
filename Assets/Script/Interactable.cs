using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isOverlapping;
    public Transform player;

    float hitDelay = 0.0f;
    GameManager gameManager;
    // Start is called before the first frame update
    void Interact()
    {
        hitDelay -= Time.deltaTime;
        if (hitDelay <= 0.0f)
        {
            if (isOverlapping == true)
            {
                Debug.Log("Is Overlapping!! with " + gameObject.name);
                gameManager.gameState = GameManager.GameState.GameOver;
                Debug.Log("Player is " + gameManager.gameState);
                hitDelay = 1.0f;
            }
        }
    }

    public virtual void Start()
    {
        gameManager = GameManager.instance;
        player = gameManager.player.transform;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Interact();
    }
}
