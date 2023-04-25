using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CipherScript : MonoBehaviour
{
    public Canvas Cipher_Canvas;
    public GameObject playerObject;
    public GameObject CameraObject;
    public bool CipherCorrect;

    private string input;
    //THE ACTUAL CIPHER
    private string cipher = "Boas";

    // Start is called before the first frame update
    void Start()
    {
        Cipher_Canvas.enabled = false;
        CipherCorrect = false;
        
    }

    public void ShowCipherCanvas()
    {
        Cipher_Canvas.enabled = true;

        // disable both player movement and camera movement
        playerObject.GetComponent<PlayerMovement>().enabled = false;
        CameraObject.GetComponent<PlayerCam>().enabled = false;

        //unlocking the mouse cursor and making it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButton(1))
        {
            //locking the cursor and making it not visible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // enable both player movement and camera movement
            playerObject.GetComponent<PlayerMovement>().enabled = true;
            CameraObject.GetComponent<PlayerCam>().enabled = true;

            Cipher_Canvas.enabled = false;
        }


        //-------------------------------
        //CHECKING IF THE CODE IS CORRECT
        //THE CODE IS: 420
        
        if (input == cipher)
        {
            CipherCorrect = true;
            /*if (Input.GetKey("enter"))
            {
                //locking the cursor and making it not visible
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                // enable both player movement and camera movement
                playerObject.GetComponent<PlayerMovement>().enabled = true;
                CameraObject.GetComponent<PlayerCam>().enabled = true;

                Cipher_Canvas.enabled = false;

                gameObject.layer = 0;
            }*/
        }
        //-------------------------------
    }

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
    }

    public void UnlockCipher()
    {

        //If the safe was opened
        if (CipherCorrect == true)
        {
            
            //locking the cursor and making it not visible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // enable both player movement and camera movement
            playerObject.GetComponent<PlayerMovement>().enabled = true;
            CameraObject.GetComponent<PlayerCam>().enabled = true;

            Cipher_Canvas.enabled = false;

            gameObject.layer = 0;
        }
    }
}
