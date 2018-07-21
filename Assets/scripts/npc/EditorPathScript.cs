using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPathScript : MonoBehaviour {
    public Color rayColor = Color.white; // an ray color
    public List<Transform> pathObject = new List<Transform>();
    Transform[] theArray;

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        theArray = GetComponentsInChildren<Transform>();
        pathObject.Clear();

        foreach (Transform waypoint in theArray)
        {
            if (waypoint != this.transform)
            {
                pathObject.Add(waypoint); // if not similar, add to list
            }
        }

        for (int i = 0; i < pathObject.Count; i++)
        {
            Vector3 currentPos = pathObject[i].position;
            if (i > 0)
            {
                Vector3 previous = pathObject[i - 1].position;
                Gizmos.DrawLine(previous, currentPos);
                Gizmos.DrawSphere(currentPos, 0.3f);
            }
        }
    }
}
