using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Transform player;
    GameManager gameManager;
    bool canAddScore = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        player = gameManager.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x >= transform.position.x)
        {
            if(canAddScore == false)
            {
                player.GetComponent<PlayerController>().Bird.Score += 1;
                canAddScore = true;
            }
        }
        if(Vector3.Distance(transform.position, player.position) >= 10 && transform.position.x < player.position.x)
        {
            Destroy(gameObject);
        }
    }
}
