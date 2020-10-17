using UnityEngine;

public abstract class CustomCollider : MonoBehaviour
{
    [SerializeField] protected CustomCollider target;
    public CustomCollider Target { get => target; set => target = value; }
    public virtual bool IsCollide()
    { return false; }

    #region DrawCollider
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        DrawCollider();
    }
    protected virtual void DrawCollider()
    {

    }
    #endregion
}
