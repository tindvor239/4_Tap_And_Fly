using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] SpawnObstacle up;
    [SerializeField] SpawnObstacle down;
    [SerializeField] GameObject obstacleStorage;
    [SerializeField] float spawnDelay;
    float currentSpawnTime;
    SpawnState state;
    private void Start()
    {
        currentSpawnTime = spawnDelay;
        state = SpawnState.none;
    }
    public void Spawn()
    {
        currentSpawnTime -= Time.deltaTime;
        if(currentSpawnTime <= 0)
        {
            state = (SpawnState)Random.Range(0, 2);
            switch(state)
            {
                case SpawnState.up:
                    SpawnUp();
                    break;
                case SpawnState.down:
                    SpawnDown();
                    break;
                case SpawnState.dual:
                    SpawnDual();
                    break;
            }
        }
    }
    public void DeleteAllObstacles()
    {
        for(sbyte i = 0; i < obstacleStorage.transform.childCount; i++)
        {
            Destroy(obstacleStorage.transform.GetChild(i).gameObject);
        }
    }
    void SpawnUp()
    {
        up.IsSpawning = true;
        currentSpawnTime = spawnDelay;
        state = SpawnState.none;
    }

    void SpawnDown()
    {
        down.IsSpawning = true;
        currentSpawnTime = spawnDelay;
        state = SpawnState.none;
    }

    void SpawnDual()
    {
        up.IsSpawning = true;
        down.IsSpawning = true;
        currentSpawnTime = spawnDelay;
        state = SpawnState.none;
    }

}
enum SpawnState { up, down, dual, none};
