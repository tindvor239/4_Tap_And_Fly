using UnityEngine;

[RequireComponent(typeof(CustomCollider))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] new CustomCollider collider;
    [SerializeField] protected Transform player;
    [SerializeField] protected GameManager gameManager;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        player = gameManager.Player.transform;
        collider = GetComponent<CustomCollider>();
        collider.Target = player.gameObject.GetComponent<CustomCollider>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(collider.IsCollide())
        {
            player.GetComponent<PlayerController>().Bird.Die();
        }
    }
}
