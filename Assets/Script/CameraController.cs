using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    GameObject target;

    public float xOffset;
    void Start()
    {
        gameManager = GameManager.Instance;
        target = gameManager.Player;
        xOffset = transform.position.x - target.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Following();
    }

    void Following()
    {
        transform.position = new Vector3(target.transform.position.x + xOffset, transform.position.y, transform.position.z);
    }
}
