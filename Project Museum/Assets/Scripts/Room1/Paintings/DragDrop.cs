using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    Vector3 offset;
    public string destinationTag = "DropArea";
    public string destinationTag2 = "DropArea2";
    public string destinationTag3 = "DropArea3";

    public Camera mainCamera;
    public Camera paintingsCamera;
    public GameObject playerObject;
    public GameObject LivrosDoor;

    private int key = 0;


    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
        Debug.Log(transform.position);
    }

    void OnMouseUp()
    {
        var rayOrigin = paintingsCamera.transform.position;
        var rayDirection = MouseWorldPosition() - paintingsCamera.transform.position;
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag.Equals(destinationTag) || hitInfo.transform.tag.Equals(destinationTag2) || hitInfo.transform.Equals(destinationTag3))
            {
                transform.position = hitInfo.transform.position;    
            }
        }
        transform.GetComponent<Collider>().enabled = true;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = paintingsCamera.WorldToScreenPoint(transform.position).z;
        return paintingsCamera.ScreenToWorldPoint(mouseScreenPos);
    }

    private void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            CheckPositions();
        }

        if(key == 3) {
            //Puzzle is done
            //Go back

            //locking the cursor and making it not visible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // enable both player movement and camera movement
            playerObject.GetComponent<PlayerMovement>().enabled = true;
            mainCamera.GetComponent<PlayerCam>().enabled = true;

            //PaintingsCanvas.SetActive(false);

            gameObject.layer = 0;

            LivrosDoor.GetComponent<Room1_Progress>().Paintings = true;
            Debug.Log("Paintings is complete = " + LivrosDoor.GetComponent<Room1_Progress>().Paintings);
        }
    }


    private void CheckPositions()
    {
        int HITS1 = 0;
        int HITS2 = 0;
        int HITS3 = 0;


        Ray ray = paintingsCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 1000f);
        foreach (RaycastHit hit in hits) 
        {

            //
            //CHECK FIRST PAINTING
            //
            if (hit.collider.CompareTag("Painting1"))
            {
                HITS1++;
            }
            if (hit.collider.CompareTag("DropArea"))
            {
                HITS1++;
            }

            //
            //CHECK SECOND PAINTING
            //
            if (hit.collider.CompareTag("Painting2")) {
                HITS2++;
            }
            if (hit.collider.CompareTag("DropArea2")) {
                HITS2++;
            }

            //
            //CHECK THIRD PAINTING
            //
            if (hit.collider.CompareTag("Painting3")) {
                HITS3++;
            }
            if (hit.collider.CompareTag("DropArea3")) {
                HITS3++;
            }
        }//End of foreach

        if (HITS1 == 2)
        {
            key++;
            Debug.Log("Raycast HIT both Painting1 and DropArea");

        }
        if (HITS2 == 2) {
            key++;
            Debug.Log("Raycast HIT both Painting2 and DropArea2");

        }
        if (HITS3 == 2) {
            key++;
            Debug.Log("Raycast HIT both Painting3 and DropArea3");

        }


        ////
        ////CHECK SECOND PAINTING
        ////
        //Ray ray2 = paintingsCamera.ScreenPointToRay(Input.mousePosition);
        //RaycastHit[] hits2 = Physics.RaycastAll(ray2, 1000f);
        //foreach (RaycastHit hit2 in hits2)
        //{
        //    if (hit2.collider.CompareTag("Painting2")) {
        //        HITS2++;
        //    }
        //    if (hit2.collider.CompareTag("DropArea2")) {
        //        HITS2++;
        //    }
        //}
        //if (HITS2 == 2)
        //{
        //    key++;
        //    Debug.Log("Raycast HIT both Painting1 and DropArea");

        //}

        ////
        ////CHECK THIRD PAINTING
        ////

        //Ray ray3 = paintingsCamera.ScreenPointToRay(Input.mousePosition);
        //RaycastHit[] hits3 = Physics.RaycastAll(ray3, 1000f);
        //foreach (RaycastHit hit3 in hits3)
        //{
        //    if (hit3.collider.CompareTag("Painting3"))
        //    {
        //        HITS3++;
        //    }
        //    if (hit3.collider.CompareTag("DropArea3"))
        //    {
        //        HITS3++;
        //    }
        //}
        //if (HITS3 == 2)
        //{
        //    key++;
        //    Debug.Log("Raycast HIT both Painting1 and DropArea");

        //}
    }

    //private void OnCollisionEnter2D(Collision2D collision) {
    //    if (collision.transform.tag == "DropArea") {
    //        Debug.Log("Collided with DropArea");
    //        if(collision.gameObject.tag == "Painting1") {
    //            key++;
    //        }
    //    }
    //    if (collision.transform.tag == "DropArea2") {
    //        Debug.Log("Collided with DropArea2");
    //        if (collision.gameObject.tag == "Painting2") {
    //            key++;
    //        }
    //    }
    //    if (collision.transform.tag == "DropArea3") {
    //        Debug.Log("Collided with DropArea3");
    //        if (collision.gameObject.tag == "Painting3") {
    //            key++;
    //        }
    //    }
    //}

}