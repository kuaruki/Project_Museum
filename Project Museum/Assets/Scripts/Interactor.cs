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
    public Camera paintingsCamera;
    public Camera JigsawCamera;
    public Camera WandCamera;
    public Camera LivrosCamera;

    public GameObject playerObject;
    public GameObject DragDropScript1;
    public GameObject DragDropScript2;
    public GameObject DragDropScript3;
    [SerializeField]
    private GameObject InventoryCanvas;
    public Canvas InteractHand;
    public Canvas CenterDot;

    [SerializeField]
    private GameObject JigsawCanvas;
    [SerializeField]
    private GameObject WandCanvas;

    public GameObject Note1;
    public GameObject Note1Canvas;

    public GameObject LivrosDoor;
    public GameObject OpenedLivrosPosition;


    public GameObject LabDoor;
    public GameObject OpenedLabDoorPosition;
    public bool isLabDoorOpen;

    public GameObject HallDoor;
    public GameObject OpenedHallDoorPosition;
    public bool isHallDoorOpen;
    public GameObject ReceptionDoor;
    public GameObject OpenedReceptionDoorPosition;
    public bool isReceptionDoorOpen;

    public int PotionsInPlace; //Each Potion script increments this

    //Safe
    public float InteractDistance = 15f;
    public LayerMask interactLayer;


    void Start()
    {
        mainCamera.enabled = true;
        paintingsCamera.enabled = false;
        JigsawCamera.enabled = false;
        LivrosCamera.enabled = false;
        paintingsCamera.GetComponent<Escape>().enabled = false; 
        DragDropScript1.GetComponent<DragDrop>().enabled = false;
        DragDropScript2.GetComponent<DragDrop>().enabled = false;
        DragDropScript3.GetComponent<DragDrop>().enabled = false;
        InteractHand.enabled = false;//disable interact hand 
        CenterDot.enabled = false;//enable the center dot
        Note1Canvas.SetActive(false);

        
        isHallDoorOpen = false;
        isReceptionDoorOpen = false;
        isLabDoorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, InteractDistance, interactLayer))
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
                    InventoryCanvas.SetActive(false);

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
                //----------
                //Painting Puzzle
                //----------
                else if (hit.collider.CompareTag("PaintingsPuz"))
                {
                    //switch to the puzzle camera
                    mainCamera.enabled = false;
                    paintingsCamera.enabled = true;

                    //Enable the script and disable this one
                    DragDropScript1.GetComponent<DragDrop>().enabled = true;
                    DragDropScript2.GetComponent<DragDrop>().enabled = true;
                    DragDropScript3.GetComponent<DragDrop>().enabled = true;

                    //this.enabled = false;

                    //Unlock Cursor
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    // disable both player movement and camera movement
                    playerObject.GetComponent<PlayerMovement>().enabled = false;
                    mainCamera.GetComponent<PlayerCam>().enabled = false;

                    paintingsCamera.GetComponent<Escape>().enabled = true;
                }
                //----------
                // EOF Painting Puzzle
                //----------
                else if (hit.collider.CompareTag("WandPuzzle"))
                {
                    //switch to the puzzle camera
                    mainCamera.enabled = false;
                    WandCamera.enabled = true;

                    //Activate the Canvas
                    WandCanvas.SetActive(true);

                    //Set Inventory Canvas off
                    InventoryCanvas.SetActive(false);

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
                else if (hit.collider.CompareTag("LivrosDoor")) {
                    LivrosDoor.transform.position = OpenedLivrosPosition.transform.position;
                    LivrosDoor.layer = 0;
                }
                //----------
                //Lab Door
                //----------
                else if (hit.collider.CompareTag("LabDoor")) {
                    if (!isLabDoorOpen) { //Door closed
                        LabDoor.transform.rotation = OpenedLabDoorPosition.transform.rotation;
                        LabDoor.layer = 0;
                        isLabDoorOpen = true;
                    }
                }
                //----------
                //Hall Door
                //----------
                else if (hit.collider.CompareTag("HallDoor")) {
                    if (!isHallDoorOpen) {//Door closed
                        HallDoor.transform.rotation = OpenedHallDoorPosition.transform.rotation;
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
                        ReceptionDoor.layer = 0;
                        isReceptionDoorOpen = true;
                    }
                }


                //-----------------
                //-----------------
                //----- Notes -----
                //-----------------
                //-----------------

                //----------
                //First Note
                //----------
                else if (hit.collider.CompareTag("Note1")) 
                {
                    Note1Canvas.SetActive(true);

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


            }
        }//might not be like this
        else 
        {
            //else disable interact Hand
            InteractHand.enabled = false;
            //and enable center Dot
            CenterDot.enabled = true;
        }
    }
}
