using UnityEngine;

public class Column : Obstacle
{
    bool canAddScore = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (player.position.x >= transform.position.x)
        {
            if (canAddScore == false)
            {
                gameManager.AddScore(1);
                player.GetComponent<PlayerController>().Bird.Scoring();
                canAddScore = true;
            }
        }
        if (Vector3.Distance(transform.position, player.position) >= 10 && transform.position.x < player.position.x)
        {
            Destroy(gameObject);
        }
    }
}
