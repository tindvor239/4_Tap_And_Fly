using UnityEngine;
public class CustomBoxCollider : CustomCollider
{
    [SerializeField] Transform[] points = new Transform[4];
    [SerializeField] Vector2 Offset;
    private Vector2 center;
    private Line[] lines = new Line[4];
    #region Properties
    public Transform[] Points
    {
        get => points;
    }
    public float Width
    {
        get => Mathf.Abs(points[0].position.x - points[1].position.x);
    }
    public float Height
    {
        get => Mathf.Abs(points[0].position.y - points[2].position.y);
    }
    #endregion

    // Update is called once per frame
    protected virtual void Update()
    {
        center = (Vector2)transform.position + Offset;
        lines[0] = new Line(points[0].position, points[1].position);
        lines[1] = new Line(points[1].position, points[2].position);
        lines[2] = new Line(points[2].position, points[3].position);
        lines[3] = new Line(points[3].position, points[0].position);
        if (target != null)
        {
            if (IsCollide())
            {
                Debug.Log("Collide bitch");
            }
        }
    }
    public override bool IsCollide()
    {
        if (target is CustomBoxCollider)
        {
            CustomBoxCollider boxCollider = (CustomBoxCollider)target;
            return CollideWithRectangle(boxCollider);
        }
        else
        {
            CustomCircleCollider circleCollider = (CustomCircleCollider)target;
            return CollideWithCircle(circleCollider);
        }
    }

    private bool CollideWithRectangle(CustomBoxCollider rectangle)
    {
        // get every line of target rectangle.
        for (int i = 0; i < lines.Length; i++)
        {
            Line current = lines[i];
            // check every line of target rectangle cross with every line of this rectangle.
            for (int j = 0; j < rectangle.lines.Length; j++)
            {
                Line target = rectangle.lines[j];
                Vector2? crossPoint = current.getCrossPoint(target);
                // if has cross point.
                if (crossPoint != null)
                {
                    // check that cross point is in target rectangle and this rectangle.
                    if (IsCrossPointInCollider((Vector2)crossPoint) && rectangle.IsCrossPointInCollider((Vector2)crossPoint))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private bool CollideWithCircle( CustomCircleCollider circle)
    {
        // calculate the absolute values of the x and y difference between the center of the circle and the center of the rectangle.
        float circleDistanceX = Mathf.Abs(circle.transform.position.x - center.x);
        float circleDistanceY = Mathf.Abs(circle.transform.position.y - center.y);
        // get circle distance to check where the circle is far away enough from the rectangle.
        if (circleDistanceX > (Width / 2 + circle.Radius))
            return false;
        if (circleDistanceY > (Height / 2 + circle.Radius))
            return false;
        // get circle distance to check where the circle is close enough from the rectangle.
        if (circleDistanceX <= Width / 2)
            return true;
        if (circleDistanceY <= Height / 2)
            return true;
        // when the circle is intersect the corner of rectangle
        // calculate distance from the center of the circle and the corner.
        float cornerDistanceSquared = (circleDistanceX - Width / 2) * (circleDistanceX - Width / 2) + (circleDistanceY - Height / 2) * (circleDistanceY - Height / 2);
        // return true when smaller circle radius
        return (cornerDistanceSquared <= circle.Radius * circle.Radius);
    }

    private bool IsCrossPointInCollider(Vector2 point)
    {
        Debug.DrawLine(center, point);
        if (point.x >= points[0].position.x && point.x <= points[3].position.x)
        {
            if(point.y >= points[3].position.y && point.y <= points[0].position.y)
            {
                return true;
            }
        }
        //Debug.Log("Cross point not found");
        return false;
    }
    #region Draw A Square
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
    protected override void DrawCollider()
    {
        Gizmos.DrawWireCube((Vector2)transform.position + Offset, new Vector2(Width, Height));
    }
    #endregion
}
public class Line
{
    private float? x, y;
    #region Properties
    public float? X { get => x; }
    public float? Y { get => y; }
    #endregion
    public Line(Vector2 A, Vector2 B)
    {
        if(B.y == A.y)
        {
            y = A.y;
        }
        else
        {
            x = A.x;
        }
    }
    public Vector2? getCrossPoint(Line B)
    {
        if(B != null)
        {
            if(y.HasValue && B.x.HasValue)
            {
                return new Vector2((float)B.x, (float)y);
            } else if (x.HasValue && B.y.HasValue)
            {
                return new Vector2((float)x, (float)B.y);
            }
        }
        return null;
    }

}
