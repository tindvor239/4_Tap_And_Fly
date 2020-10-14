using UnityEngine;
using System;

public class Paralax : MonoBehaviour
{
    [SerializeField] MeshRenderer[] farAwayObjects;
    [SerializeField] MeshRenderer[] landObjects;
    [SerializeField] Transform followingObject;
    [SerializeField] float xOffset;
    public static Paralax Instance;
    #region Singleton
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    #region Properties
    public Transform Target
    { get; set; }
    public Transform FollwingObject
    {
        get { return followingObject; }
    }

    public float XOffset
    {
        get { return xOffset; }
    }
    #endregion
    #region Behavior
    public void ScrollingFarAway()
    {
        if(farAwayObjects != null && Target != null)
        {
            for(sbyte i = 0; i < farAwayObjects.Length; i++)
            {
                farAwayObjects[i].material.mainTextureOffset = new Vector2(Target.position.x / (100 * (i + 1)), 0);
            }
        }
        else if(farAwayObjects != null)
        {
            for (sbyte i = 0; i < farAwayObjects.Length; i++)
            {
                farAwayObjects[i].material.mainTextureOffset += new Vector2(farAwayObjects[i].transform.position.x / (600 * (i + 1)), 0);
            }
        }
    }
    public void ScrollingSteady()
    {
        if (landObjects != null && Target != null)
        {
            for (sbyte i = 0; i < landObjects.Length; i++)
            {
                landObjects[i].material.mainTextureOffset = new Vector2(Target.position.x / 20, 0);
            }
        }
        else if(landObjects != null)
        {
            for (sbyte i = 0; i < landObjects.Length; i++)
            {
                landObjects[i].material.mainTextureOffset += new Vector2(landObjects[i].transform.position.x / 40, 0);
            }
        }
    }
    public void Following()
    {
        if(followingObject != null && Target != null)
        {
            followingObject.position = new Vector3(xOffset + Target.position.x, followingObject.position.y, followingObject.position.z);
        }
    }
    #endregion
}
