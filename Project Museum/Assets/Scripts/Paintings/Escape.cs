using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private Camera paintingsCamera;

    // Update is called once per frame
    void Update()
    {
        
        if (!playerObject.GetComponent<PlayerMovement>().enabled) {
            if (Input.GetKey(KeyCode.Escape)) {
                Debug.Log("ESCAPE");

                //locking the cursor and making it not visible
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                //switch to the main camera
                mainCamera.enabled = true;
                paintingsCamera.enabled = false;

                // enable both player movement and camera movement
                playerObject.GetComponent<PlayerMovement>().enabled = true;
                mainCamera.GetComponent<PlayerCam>().enabled = true;
            }

        }
    }
}
