using UnityEngine;
public class CustomBoxCollider : CustomCollider
{
    [SerializeField] Transform target;
    Vector2[] notePoints = new Vector2[4];
    static Vector2[] fixPoints = { new Vector2(-0.08f, 0.08f), new Vector2(0.08f, 0.08f), new Vector2(-0.08f, -0.08f), new Vector2(0.08f, -0.08f) };
    private float leftSlope, rightSlope, upSlope, downSlope;
    #region Properties
    public Vector2[] FixPoints
    {
        get => fixPoints;
    }
    public Vector2[] NotePoints
    {
        get => notePoints;
        set => notePoints = value;
    }

    public float LeftSlope { get => leftSlope; }
    public float RightSlope { get => rightSlope; }
    public float UpSlope { get => upSlope; }
    public float DownSlope { get => downSlope; }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get m of straight lines
        leftSlope = EquationOfStraightLine(notePoints[0], notePoints[2]);
        rightSlope = EquationOfStraightLine(notePoints[1], notePoints[3]);
        upSlope = EquationOfStraightLine(notePoints[0], notePoints[1]);
        downSlope = EquationOfStraightLine(notePoints[2], notePoints[3]);
        if (target != null)
        {
            CustomCollider customCollider = target.GetComponent<CustomCollider>();
            if(target.position.x < transform.position.x)
            {
                // if target collider is square then get left straight line of this object distance with right line of target object.
                if(customCollider is CustomBoxCollider)
                {
                    //Debug.Log(string.Format(" name: {0}, is box collider", target.name));
                    
                }
                else
                {
                    Debug.Log(string.Format(" name: {0}, is other collider", target.name));
                }
                // if target collider is radius then get left straight line of this object distance with radius of target object.
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        DrawSquare();
    }
    private void DrawSquare()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(notePoints[0], notePoints[1]);
        Gizmos.DrawLine(notePoints[0], notePoints[2]);
        Gizmos.DrawLine(notePoints[3], notePoints[2]);
        Gizmos.DrawLine(notePoints[3], notePoints[1]);
    }

    //y - y1 = m(x - x1)
    private float EquationOfStraightLine(Vector2 a, Vector2 b)
    {
        // y = m(x - x1) + y1
        float m = a.y - b.y / a.x - b.x;
        return m;
    }

    private bool IsColliderOverlapping(CustomCollider thisCollider, CustomCollider targetCollider)
    {
        float x, y;
        bool result = false;
        if(targetCollider is CustomBoxCollider && thisCollider is CustomBoxCollider)
        {
            CustomBoxCollider thisBoxCollider = (CustomBoxCollider)thisCollider;
            CustomBoxCollider targetBoxCollider = (CustomBoxCollider)targetCollider;
            //y = ax + b.
            //y = a'x + b'.
            // simulate if left
            //y = thisBoxCollider.LeftSlope * x + thisBoxCollider.NotePoints[2].y;
            //y = targetBoxCollider.RightSlope * x + targetBoxCollider.NotePoints[2].y;
            x = (targetBoxCollider.NotePoints[2].y + thisBoxCollider.NotePoints[2].y) / (thisBoxCollider.LeftSlope - targetBoxCollider.RightSlope);
        }
        return result;
    }
}
