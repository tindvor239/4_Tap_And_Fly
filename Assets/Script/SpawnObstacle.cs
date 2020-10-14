using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    [SerializeField] GameObject obstacleStorage;
    private bool isSpawning;
    private sbyte randomObstacleIndex;
    public bool IsSpawning
    {
        get { return isSpawning; }
        set { isSpawning = value; }
    }

    private void Update()
    {
        Spawn();
    }

    void GetRandomeIndex()
    {
        randomObstacleIndex = (sbyte)Random.Range(0, obstacles.Length - 1);
    }
    void Spawn()
    {
        if(isSpawning)
        {
            GetRandomeIndex();
            GameObject newObstacle;
            newObstacle = Instantiate(obstacles[randomObstacleIndex]);
            newObstacle.transform.position = transform.position;
            newObstacle.transform.parent = obstacleStorage.transform;
            isSpawning = false;
        }
    }
}
