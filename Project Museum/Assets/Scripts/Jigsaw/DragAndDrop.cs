using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectedPiece;
    int OIL = 1;

    void Start()
    {
        
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            SceneManager.LoadScene("Museum");
        }

        if (Input.GetMouseButtonDown(0)) //Puzzle piece selected
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
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
