using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;


public class SafeScript : MonoBehaviour
{

    public GameObject Safe_Canvas;
    public GameObject playerObject;
    public GameObject CameraObject;
    public GameObject LivrosDoor;
    [SerializeField] private GameObject OpenedPosition;

    private int number01 = 1;
    private int number02 = 1;
    private int number03 = 1;

    public Text textNumber01;
    public Text textNumber02;
    public Text textNumber03;

    public bool Opened;
    public float doorOpenAngle = 90f;
    public float doorClosedAngle = 0f;
    public float smooth = 2f; //This is the speed of the rotation

    private void Start()
    {
        Safe_Canvas.SetActive(false);
        Opened = false; 
    }
    public void ShowSafeCanvas()
    {
        Safe_Canvas.SetActive(true);
        
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
        if (Safe_Canvas.activeInHierarchy) {
            if (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButton(1)) {
                //locking the cursor and making it not visible
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                // enable both player movement and camera movement
                playerObject.GetComponent<PlayerMovement>().enabled = true;
                CameraObject.GetComponent<PlayerCam>().enabled = true;

                Safe_Canvas.SetActive(false);
            }
        }

        //-------------------------------
        //CHECKING IF THE CODE IS CORRECT
        //THE CODE IS: 420
        if (number01 == 4 && number02 == 2 && number03 == 0)
        {
            Opened = true;

        }
        //-------------------------------
        
    }


    //This function is called when the button is clicked
    public void UnlockSafe()
    {
        //TO:DO
        //Open the Safe

        //If the safe was opened
        if (Opened == true)
        {

            //locking the cursor and making it not visible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // enable both player movement and camera movement
            playerObject.GetComponent<PlayerMovement>().enabled = true;
            CameraObject.GetComponent<PlayerCam>().enabled = true;


            //opens the safe door (doesn't work)
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(-90f, -180f, 0f));
            gameObject.transform.position = OpenedPosition.transform.position;


            gameObject.layer = 0; //sets the object to the layer 0 making it not interactable anymore

            Safe_Canvas.SetActive(false);
            LivrosDoor.GetComponent<Room1_Progress>().Safe = true;
            Debug.Log("Safe is complete = " + LivrosDoor.GetComponent<Room1_Progress>().Safe);
            CameraObject.GetComponent<Interactor>().Correct.Play();
        }
        else {//else send a visual cue that it's incorrect
            CameraObject.GetComponent<Interactor>().Wrong.Play();
        }
    }

    public void Increase(int _number)
    {
        if (_number == 1)
        {
            number01++;
            textNumber01.text = number01.ToString();
            if (number01 > 9)
            {
                number01 = 0;
                textNumber01.text = number01.ToString();
            }
        }
        else if (_number == 2)
        {
            number02++;
            textNumber02.text = number02.ToString();
            if (number02 > 9)
            {
                number02 = 0;
                textNumber02.text = number02.ToString();
            }
        }
        else if (_number == 3)
        {
            number03++;
            textNumber03.text = number03.ToString();
            if (number03 > 9)
            {
                number03 = 0;
                textNumber03.text = number03.ToString();
            }
        }
    }
    public void Decrease(int _number)
    {
        if (_number == 1)
        {
            number01--;
            textNumber01.text = number01.ToString();
            if (number01 < 0)
            {
                number01 = 9;
                textNumber01.text = number01.ToString();
            }
        }
        else if (_number == 2)
        {
            number02--;
            textNumber02.text = number02.ToString();
            if (number02 < 0)
            {
                number02 = 9;
                textNumber02.text = number02.ToString();
            }
        }
        else if (_number == 3)
        {
            number03--;
            textNumber03.text = number03.ToString();
            if (number03 < 0)
            {
                number03 = 9;
                textNumber03.text = number03.ToString();
            }
        }
    }
}