using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    // Camera component, reads world space equivalent mouse positions
    public Camera cam = null;

    // LineRenderer component to draw lines
    public LineRenderer lineRenderer = null;

    // Mouse position
    private Vector3 mousePos;
    private Vector3 pos;
    private Vector3 previousPos;

    // List to store mouse positions to draw lines
    public List<Vector3> linePositions = new List<Vector3>();

    public float minimumDistance = 0.05f;
    public float distance = 0f;

    void Update()
    {
        // When user first right clicks the mouse
        if (Input.GetMouseButtonDown(0))
        {
            // Clearing the list so everytime we click draws a new set of lines
            linePositions.Clear();

            mousePos = Input.mousePosition;
            // Mouse position z axis positions always is positive
            mousePos.z = 10;

            // Converting screen space mouse positions into world space mousepositions
            pos = cam.ScreenToWorldPoint(mousePos);

            // Saving previous mouse position
            previousPos = pos;

            // Storing mouse positions into the list array
            linePositions.Add(pos);
        }
        // Condition to check if user holding the mouse
        else if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            // Mouse position z axis positions always is positive
            mousePos.z = 10;

            // Converting screen space mouse positions into world space mousepositions
            pos = cam.ScreenToWorldPoint(mousePos);

            distance = Vector3.Distance(pos, previousPos);

            // Check to avoid adding duplicate vector values into the list
            if (distance >= minimumDistance)
            {
                // Saving previous mouse position
                previousPos = pos;

                // Storing mouse positions into the list array
                linePositions.Add(pos);

                // Adding mouse positions to line renderer component to draw lines on these points
                lineRenderer.positionCount = linePositions.Count;
                lineRenderer.SetPositions(linePositions.ToArray());
            }
        }
    }
}
