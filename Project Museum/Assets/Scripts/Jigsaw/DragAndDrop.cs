using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectedPiece;
    int OIL = 1;

    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private Camera JigsawCamera;
    [SerializeField]
    private GameObject JigsawCanvas;
    [SerializeField]
    private GameObject InventoryCanvas;
    [SerializeField]
    private GameObject JigsawBoard;

    public int PiecesInPlace;

    void Start()
    {
        JigsawCanvas.SetActive(false);
        PiecesInPlace = 0;
    }

    void Update(){

        if (JigsawCanvas.activeInHierarchy) {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                //locking the cursor and making it not visible
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                //switch to the puzzle camera
                mainCamera.enabled = true;
                JigsawCamera.enabled = false;
                // enable both player movement and camera movement
                playerObject.GetComponent<PlayerMovement>().enabled = true;
                mainCamera.GetComponent<PlayerCam>().enabled = true;

                JigsawCanvas.SetActive(false);
                InventoryCanvas.SetActive(true);
            }
        

            if (Input.GetMouseButtonDown(0)) //Puzzle piece selected
            {
                RaycastHit2D hit = Physics2D.Raycast(JigsawCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
                if (hit.transform.CompareTag("Puzzle"))
                {
                    if (!hit.transform.GetComponent<PiecesScript>().inRightPosition) //this if makes it so that the pieces stay in place after being put in the right place
                    {  
                        SelectedPiece = hit.transform.gameObject;
                        SelectedPiece.GetComponent<PiecesScript>().selected = true;
                        SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL; // makes a piece in front of all the others
                        OIL++;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if(SelectedPiece != null) {
                    SelectedPiece.GetComponent<PiecesScript>().selected = false;
                    SelectedPiece = null;
                }
            }
            if(SelectedPiece != null)
            {
                Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);

            }
        }
    }

    public void IsSolved() {
        if(PiecesInPlace == 16) {
            Debug.Log("SOLVED");

            //switch to the puzzle camera
            mainCamera.enabled = true;
            JigsawCamera.enabled = false;

            //locking the cursor and making it not visible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;


            // enable both player movement and camera movement
            playerObject.GetComponent<PlayerMovement>().enabled = true;
            mainCamera.GetComponent<PlayerCam>().enabled = true;

            //Set Inventory Canvas on
            InventoryCanvas.SetActive(true);

            //Make the jigsaw board not interactible after puzzle completion
            JigsawBoard.layer = 0;

            JigsawCanvas.SetActive(false);
        }
        else {
            //Feedback to player saying its incomplete
        }
    }
}
