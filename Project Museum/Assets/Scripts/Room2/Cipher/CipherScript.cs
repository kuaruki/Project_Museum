using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CipherScript : MonoBehaviour
{
    public GameObject Cipher_Canvas;
    public GameObject playerObject;
    public GameObject CameraObject;
    public GameObject LabDoor;


    public bool CipherCorrect;


    public Button button;

    private string input;
    //THE ACTUAL CIPHER
    private string cipher = "Boas";

    // Start is called before the first frame update
    void Start()
    {
        Cipher_Canvas.SetActive(false);
        CipherCorrect = false;
        
    }

    public void ShowCipherCanvas()
    {
        Cipher_Canvas.SetActive(true);

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
        if (Cipher_Canvas.activeInHierarchy) {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                //locking the cursor and making it not visible
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                // enable both player movement and camera movement
                playerObject.GetComponent<PlayerMovement>().enabled = true;
                CameraObject.GetComponent<PlayerCam>().enabled = true;

                Cipher_Canvas.SetActive(false);
            }
        }


        //-------------------------------
        //CHECKING IF THE CIPHER IS CORRECT
        //THE CIPHER IS: Boas (see line 18)
        
        if (input == cipher)
        {
            CipherCorrect = true;
            /*if (Input.GetKeyUp(KeyCode.Return))
            {
                Debug.Log("Entered Enter");
                UnlockCipher();
            }*/
        }
        //-------------------------------
    }

    public void ReadStringInput(string s)
    {
        input = s;
        //Debug.Log(input);
        if (Input.GetKeyDown(KeyCode.Return)) {
            Debug.Log(s + "Through Enter Key");
            //locking the cursor and making it not visible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // enable both player movement and camera movement
            playerObject.GetComponent<PlayerMovement>().enabled = true;
            CameraObject.GetComponent<PlayerCam>().enabled = true;

            Cipher_Canvas.SetActive(false);

            gameObject.layer = 0;

            LabDoor.GetComponent<Room2_Progress>().Cipher = true;
            Debug.Log("Cipher Puzzle is Done" + LabDoor.GetComponent<Room2_Progress>().Cipher);
        }
    }

    public void UnlockCipher()
    {
        Debug.Log(input + "Through Button");
        //If the cipher is cracked
        if (CipherCorrect == true)
        {
            
            //locking the cursor and making it not visible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // enable both player movement and camera movement
            playerObject.GetComponent<PlayerMovement>().enabled = true;
            CameraObject.GetComponent<PlayerCam>().enabled = true;

            Cipher_Canvas.SetActive(false);

            gameObject.layer = 0;

            LabDoor.GetComponent<Room2_Progress>().Cipher = true;
            Debug.Log("Cipher Puzzle is Done" + LabDoor.GetComponent<Room2_Progress>().Cipher);
        }
    }
}