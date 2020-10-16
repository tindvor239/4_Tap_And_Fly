using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomBoxCollider))]
public class BoxColliderInspector : Editor
{
    private CustomBoxCollider customBoxCollider;
    private void OnEnable()
    {
        customBoxCollider = (CustomBoxCollider)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        CreatePoints();
    }

    private void CreatePoints()
    {
        Vector3[] notePoints = new Vector3[4];
        for (sbyte count = 0; count < customBoxCollider.NotePoints.Length; count++)
        {
            string label = "";
            if (count == 0)
                label = "a";
            else if (count == 1)
                label = "b";
            else if (count == 2)
                label = "c";
            else
                label = "d";
            customBoxCollider.NotePoints[count] = (Vector2)customBoxCollider.transform.position + (customBoxCollider.FixPoints[count] * customBoxCollider.transform.localScale);
            notePoints[count] = customBoxCollider.NotePoints[count];
            Handles.Label(customBoxCollider.NotePoints[count], label);
        }
    }

    private void Orbiting()
    {

    }
}
