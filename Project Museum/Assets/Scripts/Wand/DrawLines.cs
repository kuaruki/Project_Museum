using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawLines : MonoBehaviour {
    // Camera component, reads world space equivalent mouse positions
    public Camera cam = null;

    // LineRenderer component to draw lines
    public LineRenderer lineRenderer = null;

    // Mouse position
    private Vector3 mousePos;
    private Vector3 pos;
    private Vector3 previousPos;
    [SerializeField]
    private GameObject Sphere;

    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject WandCanvas;
    [SerializeField]
    private Camera WandCamera;
    [SerializeField]
    private GameObject Door;
    [SerializeField]
    private GameObject InventoryCanvas;

    public string CollidedTag;
    public bool colliding;
    public bool IsRight;
    //Here put an array with all the collided Tags
    [SerializeField]
    private List<string> CollidedTagsList = new List<string>();
    //The right sequence
    [SerializeField]
    private List<string> RightSequence = new List<string> { "Point 1", "Point 5", "Point 6", "Point 7", "Point 8", "Point 2", "Point 3", "Point 4" };

    // List to store mouse positions to draw lines
    public List<Vector3> linePositions = new List<Vector3>();

    public float minimumDistance = 0.05f;
    public float distance = 0f;

    private void Start() {
        Sphere.GetComponent<Renderer>().enabled = false;
        colliding = false;
        IsRight = false;
        WandCanvas.SetActive(false);
    }
    void Update() {
        if (WandCanvas.activeInHierarchy) {

            //Exit puzzle
            if (Input.GetKeyUp(KeyCode.Escape)) {
                //locking the cursor and making it not visible
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                //switch to the puzzle camera
                mainCamera.enabled = true;
                WandCamera.enabled = false;
                // enable both player movement and camera movement
                playerObject.GetComponent<PlayerMovement>().enabled = true;
                mainCamera.GetComponent<PlayerCam>().enabled = true;

                WandCanvas.SetActive(false);
                WandCanvas.SetActive(true);
            }

            // When user first right clicks the mouse
            if (Input.GetMouseButtonDown(0)) {
                // Clearing the list so everytime we click draws a new set of lines
                linePositions.Clear();
                Sphere.GetComponent<Collider>().enabled = true;


                mousePos = Input.mousePosition;

                // Mouse position z axis positions always is positive
                mousePos.z = 10;

                // Converting screen space mouse positions into world space mousepositions
                pos = cam.ScreenToWorldPoint(mousePos);

                // Saving previous mouse position
                previousPos = pos;

                // Storing mouse positions into the list array
                linePositions.Add(pos);

                // Clearing the list so everytime we click draws a new set of Collided tags
                CollidedTagsList.Clear();
            }
            // Condition to check if user holding the mouse
            else if (Input.GetMouseButton(0)) {
                mousePos = Input.mousePosition;
                // Mouse position z axis positions always is positive
                mousePos.z = 10;

                // Converting screen space mouse positions into world space mousepositions
                pos = cam.ScreenToWorldPoint(mousePos);

                //Attach the sphere to the mouse
                Sphere.transform.position = pos;

                distance = Vector3.Distance(pos, previousPos);

                // Check to avoid adding duplicate vector values into the list
                if (distance >= minimumDistance) {
                    // Saving previous mouse position
                    previousPos = pos;

                    // Storing mouse positions into the list array
                    linePositions.Add(pos);

                    // Adding mouse positions to line renderer component to draw lines on these points
                    lineRenderer.positionCount = linePositions.Count;
                    lineRenderer.SetPositions(linePositions.ToArray());
                }
            }
            else if (Input.GetMouseButtonUp(0)) {
                Sphere.GetComponent<Collider>().enabled = false;
                //lineRenderer.enabled = false;
                CollidedTagsList.Clear();
                linePositions.Clear();
            }

            //If there's collision add to the CollidedTagsList (list of collided tags). Only adds unique strings from list.
            if (colliding == true) {
                if (!CollidedTagsList.Contains(CollidedTag)) { //if the tag is already in the list it won't be added
                    CollidedTagsList.Add(CollidedTag);
                }
            }
            //
            //
            //This here checks if the sequence is right
            //------|
            //------V
            if (IsRight == false) {
                if (CompareStringLists(RightSequence, CollidedTagsList) == true) {
                    IsRight = true;
                    Debug.Log("LESSGUUU BOIII YOU GOT IT");
                    //Solved();
                }
            }
        }
    }

    ////Compares the right sequence with the players sequence
    //public static bool CompareStringLists(List<string> list1, List<string> list2) {
    //    if (list1.Count != list2.Count) {
    //        return false;
    //    }

    //    for (int i = 0; i < list1.Count; i++) {
    //        if (list1[i] != list2[i]) {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    public static bool CompareStringLists<T>(List<T> list1, List<T> list2) {
        if (list1.Count != list2.Count) {
            return false;
        }

        HashSet<T> set1 = new HashSet<T>(list1);
        HashSet<T> set2 = new HashSet<T>(list2);

        return set1.SetEquals(set2);
    }

    public void Solved() 
    {
        if (IsRight) {
            Debug.Log("Wand SOLVED");

            //switch to the puzzle camera
            mainCamera.enabled = true;
            WandCamera.enabled = false;

            //locking the cursor and making it not visible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;


            // enable both player movement and camera movement
            playerObject.GetComponent<PlayerMovement>().enabled = true;
            mainCamera.GetComponent<PlayerCam>().enabled = true;

            //Set Inventory Canvas on
            InventoryCanvas.SetActive(true);

            //Make the jigsaw board not interactible after puzzle completion
            //JigsawBoard.layer = 0;

            Door.GetComponent<MeshRenderer>().enabled = false;
            //Door.GetComponent<Collider>().enabled = false;
            Door.layer = default;
            //Mudar de layer

            WandCanvas.SetActive(false);
        }
        else {//Not Solved
            //Feedback to the player
        }
    }
}