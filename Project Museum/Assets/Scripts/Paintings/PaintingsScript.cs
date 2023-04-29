using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaintingsScript : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject CameraObject;
    private Vector3 initialCameraPosition;
    public Vector3 cameraTargetPosition;
    public Transform cameraTargetTransform;
    int check = 1;


    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(1))
        {
            playerObject.GetComponent<PlayerMovement>().enabled = true;
            CameraObject.GetComponent<PlayerCam>().enabled = true;
            if (check == 1)
            {
                //return camera to initial position
                CameraObject.transform.position = initialCameraPosition;
                Debug.Log("Camera Initial Position" + initialCameraPosition);
                check = 0;
            }
        }
    }

    public void MoveCamera()
    {
        //Pause player and camera mvmnt
        playerObject.GetComponent<PlayerMovement>().enabled = false;
        CameraObject.GetComponent<PlayerCam>().enabled = false;
        Debug.Log("Stopped movements");

        //Save initial camera position
        initialCameraPosition = CameraObject.transform.position;
        Debug.Log("Initial Camera Position" + initialCameraPosition);

        //move the "camera" to the set position (editor placeholder)
        CameraObject.transform.position = cameraTargetPosition;
        Debug.Log("Camera Target Position" + cameraTargetPosition);

        //Make the camera face the wall (also editor placeholder)
        CameraObject.transform.LookAt(cameraTargetTransform);
        Debug.Log("Camera Target Transform" + cameraTargetTransform);


        //Unlock the cursor and making it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Somehow incorporate Drag&Drop

        //Check sequence

    }
}