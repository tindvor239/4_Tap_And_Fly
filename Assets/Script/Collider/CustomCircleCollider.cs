using UnityEngine;

public class CustomCircleCollider : CustomCollider
{
    [SerializeField] private float radius;
    #region Properties
    public float Radius
    {
        get => radius;
    }
    #endregion
    #region Draw Circle
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
    protected override void DrawCollider()
    {
        base.DrawCollider();
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    #endregion
    }
