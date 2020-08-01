using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject[] obstacles;
    public bool isSpawning;
    public int randomObstacleIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (obstacles != null)
        {
            if (isSpawning == true)
            {
                Spawn();
            }
        }
    }

    public void Spawn()
    {
        GameObject newObstacle;
        newObstacle = Instantiate(obstacles[randomObstacleIndex]);
        print(gameObject.name);
        print(randomObstacleIndex);
        newObstacle.transform.position = transform.position;
        isSpawning = false;
    }
}
