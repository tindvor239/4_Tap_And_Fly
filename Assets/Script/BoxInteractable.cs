using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteractable : Interactable
{
    [SerializeField] float width;
    [SerializeField] float height;
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        float distance = (player.position - gameObject.transform.position).sqrMagnitude;
        //print(gameObject.name + ":---Vector: " + gameObject.transform.position);
        //print("distance: " + distance + " of " + gameObject.name);
        if (distance < height/2)
        {
            isOverlapping = true;
        }
        else
        {
            isOverlapping = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1));
    }
}
