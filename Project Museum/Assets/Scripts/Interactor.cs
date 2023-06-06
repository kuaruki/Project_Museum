using UnityEngine;
using UnityEngine.SceneManagement;

interface IInteractable {
    public void Interact();
}
public class Interactor : MonoBehaviour
{

    //Paintings
    //private int paintOrder = 0;
    //if the player puts the correct painting in the correct place paintOrder++
    //if 3 paintings are down and paintOrder = 3
    //puzzle done

    public Camera mainCamera;
    public Camera JigsawCamera;
    public Camera WandCamera;

    public GameObject playerObject;
    private NoteScript NoteScript;

    public LayerMask interactLayer;
    public LayerMask PaintingsLayer;
    public LayerMask PotionsLayer;
    public LayerMask TargetableLayer;

    //Canvases
    //[SerializeField] private GameObject InventoryCanvas;
    [SerializeField] private GameObject JigsawCanvas;
    [SerializeField] private GameObject WandCanvas;
    [SerializeField] private GameObject Tutorial;
    public Canvas InteractHand;
    public Canvas CenterDot;
    public GameObject Note1Canvas;
    public GameObject ScrollCanvas;

    //Livros Door
    public GameObject LivrosDoor;
    public Vector3 LivrosDoorClosedPosition;
    public GameObject OpenedLivrosPosition;
    public bool isLivrosDoorOpen;

    //Lab Door
    public GameObject LabDoor;
    public Quaternion LabDoorInitialRotation;
    public GameObject OpenedLabDoorPosition;
    public bool isLabDoorOpen;

    //Hall Door
    public GameObject HallDoor;
    public Quaternion HallDoorInitialRotation;
    public GameObject OpenedHallDoorPosition;
    public bool isHallDoorOpen;

    //Reception Door
    public GameObject ReceptionDoor;
    public Quaternion ReceptionDoorInitialRotation;
    public GameObject OpenedReceptionDoorPosition;
    public bool isReceptionDoorOpen;

    //Final Door
    public GameObject FinalDoor;
    public Quaternion FinalDoorInitialRotation;
    public GameObject OpenedFinalDoorRotation;
    public bool isFinalDoorOpen;


    //AUDIO STUFF
    public AudioSource DoorOpen;
    public AudioSource DoorClose;
    public AudioSource SlidingOpen;
    public AudioSource SlidingClose;
    public AudioSource Correct;
    public AudioSource Wrong;

    //Simple Variables
    public int PotionsInPlace; //Each Potion script increments this -------- 4 total
    public int PaintingsInPlace; //Each Painting script increments this ---- 3 total

    private float InteractDistance = 7f;


    void Start()
    {
        mainCamera.enabled = true;
        JigsawCamera.enabled = false;
        WandCamera.enabled = false;
        InteractHand.enabled = false; //disable interact hand 
        CenterDot.enabled = false; //enable the center dot
        Note1Canvas.SetActive(false);
        Tutorial.SetActive(true);
        ScrollCanvas.SetActive(false);
        WandCanvas.SetActive(false);

        NoteScript = FindObjectOfType<NoteScript>();


        //Hall
        isHallDoorOpen = false;
        HallDoorInitialRotation = HallDoor.transform.rotation;
        //Reception
        isReceptionDoorOpen = false;
        ReceptionDoorInitialRotation = ReceptionDoor.transform.rotation;
        //Lab
        isLabDoorOpen = false;
        LabDoorInitialRotation = LabDoor.transform.rotation;
        //FinalDoor
        isFinalDoorOpen = false;
        FinalDoorInitialRotation = FinalDoor.transform.rotation;
        //Livros
        isLivrosDoorOpen = false;
        LivrosDoorClosedPosition = LivrosDoor.transform.position;
        Debug.Log("Closed Position saved on Interactor Start" + LivrosDoor.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, InteractDistance, interactLayer) || Physics.Raycast(ray, out hit, InteractDistance, PaintingsLayer) || Physics.Raycast(ray, out hit, InteractDistance, PotionsLayer) || Physics.Raycast(ray, out hit, InteractDistance, TargetableLayer))
        {
            //Here is where we can put a little hand to tell the player that he is facing an interactable object
            InteractHand.enabled = true;//enable interact hand 
            CenterDot.enabled = false;//disable center dot

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.collider.CompareTag("Safe"))
                {
                    //Show the Safe UI
                    hit.collider.GetComponent<SafeScript>().ShowSafeCanvas();
                }
                //----------
                //Jigsaw Puzzle
                //----------
                else if (hit.collider.CompareTag("Jigsaw"))
                {
                    //switch to the puzzle camera
                    mainCamera.enabled = false;
                    JigsawCamera.enabled = true;
                    JigsawCanvas.SetActive(true);
                    //Set Inventory Canvas off
                    //InventoryCanvas.SetActive(false);

                    //Unlock Cursor
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    // disable both player movement and camera movement
                    playerObject.GetComponent<PlayerMovement>().enabled = false;
                    mainCamera.GetComponent<PlayerCam>().enabled = false;
                }
                else if (hit.collider.CompareTag("Cipher"))
                {
                    //Show the Cipher UI
                    hit.collider.GetComponent<CipherScript>().ShowCipherCanvas();
                }
                else if (hit.collider.CompareTag("WandPuzzle"))
                {
                    //switch to the puzzle camera
                    mainCamera.enabled = false;
                    WandCamera.enabled = true;

                    //Activate the Canvas
                    WandCanvas.SetActive(true);

                    //Set Inventory Canvas off
                    //InventoryCanvas.SetActive(false);

                    //Unlock Cursor
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    // disable both player movement and camera movement
                    playerObject.GetComponent<PlayerMovement>().enabled = false;
                    mainCamera.GetComponent<PlayerCam>().enabled = false;
                }



                //-----------------
                //-----------------
                //----- Doors -----
                //-----------------
                //-----------------

                //----------
                //Livros Door
                //----------
                //else if (hit.collider.CompareTag("LivrosDoor")) {
                //    if (!isLivrosDoorOpen) {//Door Closed
                //        LivrosDoor.transform.position = OpenedLivrosPosition.transform.position;
                //        LivrosDoor.layer = 0;
                //        isLivrosDoorOpen = true;
                //    }
                //}
                //----------
                //Lab Door
                //----------
                //else if (hit.collider.CompareTag("LabDoor")) {
                //    if (!isLabDoorOpen) {//Door closed
                //        LabDoor.transform.rotation = OpenedLabDoorPosition.transform.rotation;
                //        DoorOpen.Play();
                //        LabDoor.layer = 0;
                //        isLabDoorOpen = true;
                //    }
                //}
                //----------
                //Hall Door
                //----------
                else if (hit.collider.CompareTag("HallDoor")) {
                    if (!isHallDoorOpen) {//Door closed
                        HallDoor.transform.rotation = OpenedHallDoorPosition.transform.rotation;
                        DoorOpen.Play();
                        HallDoor.layer = 0;
                        isHallDoorOpen = true;
                    }
                }
                //----------
                //Reception Door
                //----------
                else if (hit.collider.CompareTag("ReceptionDoor")) {
                    if (!isReceptionDoorOpen) {//Door closed
                        ReceptionDoor.transform.rotation = OpenedReceptionDoorPosition.transform.rotation;
                        DoorOpen.Play();
                        ReceptionDoor.layer = 0;
                        isReceptionDoorOpen = true;
                    }
                }
                //----------
                //Final Door
                //----------
                else if (hit.collider.CompareTag("FinalDoor")) {
                    if (!isFinalDoorOpen) {//Door closed
                        FinalDoor.transform.rotation = OpenedFinalDoorRotation.transform.rotation;
                        DoorOpen.Play();
                        FinalDoor.layer = 0;
                        isFinalDoorOpen = true;
                    }
                }


                //-----------------
                //-----------------
                //----- Notes -----
                //-----------------
                //-----------------

                else if (hit.collider.CompareTag("Note1")) {
                    NoteScript noteScript = hit.collider.GetComponent<NoteScript>();

                    Note1Canvas.SetActive(true);
                    if (noteScript != null) {
                        noteScript.GetNoteImage();
                        noteScript.UIImageScaler();
                    }
                    //Unlock Cursor
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    // disable both player movement and camera movement
                    playerObject.GetComponent<PlayerMovement>().enabled = false;
                    mainCamera.GetComponent<PlayerCam>().enabled = false;
                }
                //-----------------
                //-----------------
                //----- END GAME -----
                //-----------------
                //-----------------

                else if (hit.collider.CompareTag("FinalScroll")) {

                    ScrollCanvas.SetActive(true);
                    
                    //Unlock Cursor
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    // disable both player movement and camera movement
                    playerObject.GetComponent<PlayerMovement>().enabled = false;
                    mainCamera.GetComponent<PlayerCam>().enabled = false;
                }

                //------------------------
                //Potions - Not Working...
                //------------------------
                else if (hit.collider.CompareTag("PurplePotion")) 
                {
                    //Here is where we can put a little hand to tell the player that he is facing an interactable object
                    InteractHand.enabled = true;//enable interact hand 
                    CenterDot.enabled = false;//disable center dot
                }
                else if (hit.collider.CompareTag("WhitePotion")) 
                {
                    //Here is where we can put a little hand to tell the player that he is facing an interactable object
                    InteractHand.enabled = true;//enable interact hand 
                    CenterDot.enabled = false;//disable center dot
                }
                else if (hit.collider.CompareTag("RedPotion")) 
                {
                    //Here is where we can put a little hand to tell the player that he is facing an interactable object
                    InteractHand.enabled = true;//enable interact hand 
                    CenterDot.enabled = false;//disable center dot
                }
                else if (hit.collider.CompareTag("GreenPotion")) 
                {
                    //Here is where we can put a little hand to tell the player that he is facing an interactable object
                    InteractHand.enabled = true;//enable interact hand 
                    CenterDot.enabled = false;//disable center dot
                }

                //----------
                //Paintings
                //---------
                else if (hit.collider.CompareTag("Painting1")) {
                    //Here is where we can put a little hand to tell the player that he is facing an interactable object
                    InteractHand.enabled = true;//enable interact hand 
                    CenterDot.enabled = false;//disable center dot
                }
                else if (hit.collider.CompareTag("Painting2")) {
                    //Here is where we can put a little hand to tell the player that he is facing an interactable object
                    InteractHand.enabled = true;//enable interact hand 
                    CenterDot.enabled = false;//disable center dot
                }
                else if (hit.collider.CompareTag("Painting3")) {
                    //Here is where we can put a little hand to tell the player that he is facing an interactable object
                    InteractHand.enabled = true;//enable interact hand 
                    CenterDot.enabled = false;//disable center dot
                }


            }
        }//might not be like this
        else 
        {
            //else disable interact Hand
            InteractHand.enabled = false;
            //and enable center Dot
            CenterDot.enabled = true;
        }


        if (Note1Canvas.activeInHierarchy) {
            if (Input.GetKey(KeyCode.Escape)) {
                //Unlock Cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Note1Canvas.SetActive(false);

                // enable both player movement and camera movement
                playerObject.GetComponent<PlayerMovement>().enabled = true;
                mainCamera.GetComponent<PlayerCam>().enabled = true;
            }
        }
        if (ScrollCanvas.activeInHierarchy) {
            if (Input.GetKey(KeyCode.Escape)) {
                //Unlock Cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ScrollCanvas.SetActive(false);

                // enable both player movement and camera movement
                playerObject.GetComponent<PlayerMovement>().enabled = true;
                mainCamera.GetComponent<PlayerCam>().enabled = true;
            }
        }
        
        if (Tutorial.activeInHierarchy) {
            //Lock Cursor
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            // disable both player movement and camera movement
            playerObject.GetComponent<PlayerMovement>().enabled = false;
            mainCamera.GetComponent<PlayerCam>().enabled = false;
            
            if (Input.GetKey(KeyCode.Escape)) {
                //Unlock Cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Tutorial.SetActive(false);

                // disable both player movement and camera movement
                playerObject.GetComponent<PlayerMovement>().enabled = true;
                mainCamera.GetComponent<PlayerCam>().enabled = true;
            }
        }
        if(PotionsInPlace == 4) {
            //Door Open Sound
            DoorOpen.Play();
        }
    }

    public void ExitTutorial() {
        //Unlock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Tutorial.SetActive(false);

        // disable both player movement and camera movement
        playerObject.GetComponent<PlayerMovement>().enabled = true;
        mainCamera.GetComponent<PlayerCam>().enabled = true;
    }
}
