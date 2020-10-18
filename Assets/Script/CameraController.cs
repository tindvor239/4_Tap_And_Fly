using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;
    private GameObject target;

    public float xOffset;

    public bool IsTargetInView
    {
        get => IsInCameraView(target);
    }
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

    private bool IsInCameraView(GameObject target)
    {
        Camera camera = GetComponent<Camera>();
        Vector2 cameraPoint = camera.WorldToViewportPoint(target.transform.position);
        return cameraPoint.x > 0 && cameraPoint.x < 1 && cameraPoint.y > 0 && cameraPoint.y < 1;
    }
}
