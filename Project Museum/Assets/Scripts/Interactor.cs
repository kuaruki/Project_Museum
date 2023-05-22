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
    //  puzzle done

    public Camera mainCamera;
    public Camera paintingsCamera;
    public GameObject playerObject;
    public GameObject DragDropScript1;
    public GameObject DragDropScript2;
    public GameObject DragDropScript3;
    public Canvas InteractHand;
    public Canvas CenterDot;

    //Safe
    public float InteractDistance = 15f;
    public LayerMask interactLayer;


    //Save position of player between scenes
    [SerializeField]
    private FloatSO position_x;
    [SerializeField]
    private FloatSO position_y;
    [SerializeField]
    private FloatSO position_z;

    void Start()
    {
        mainCamera.enabled = true;
        paintingsCamera.enabled = false;
        DragDropScript1.GetComponent<DragDrop>().enabled = false;
        DragDropScript2.GetComponent<DragDrop>().enabled = false;
        DragDropScript3.GetComponent<DragDrop>().enabled = false;
        InteractHand.enabled = false;//disable interact hand 
        CenterDot.enabled = false;//enable the center dot

        //Start in players last pos... maybe?
        playerObject.transform.position = new Vector3(position_x.Value_X, position_y.Value_Y, position_z.Value_Z);
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
                else if (hit.collider.CompareTag("Jigsaw"))
                {
                    //Unlock cursor
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    //Save players pos
                    position_x.Value_X = playerObject.transform.position.x;
                    position_y.Value_Y = playerObject.transform.position.y;
                    position_z.Value_Z = playerObject.transform.position.z;
                    
                    //Open Jigsaw Scene
                    SceneManager.LoadScene("Jigsaw2D");
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
                }
                //----------
                // EOF Painting Puzzle
                //----------
                else if (hit.collider.CompareTag("WandPuzzle"))
                {
                    //Open Wand Scene
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    //Save players pos
                    position_x.Value_X = playerObject.transform.position.x;
                    position_y.Value_Y = playerObject.transform.position.y;
                    position_z.Value_Z = playerObject.transform.position.z;

                    SceneManager.LoadScene("Wand");
                }
            }
        }//might not be like this
        else {
            //else disable interact Hand
            InteractHand.enabled = false;
            //and enable center Dot
            CenterDot.enabled = true;
        }
    }
}
