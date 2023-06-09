using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class PiecesScript : MonoBehaviour
{
    Vector3 RightPosition;
    public bool inRightPosition = false;
    public bool selected;

    [SerializeField]
    private GameObject MainCamera;

    void Start()
    {
        RightPosition = transform.position;
        Debug.Log("Right Position" + RightPosition);
        transform.position = new Vector3 (Random.Range(104f, 110f), Random.Range(-56.5f, -50f), RightPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, RightPosition) < 1f) //"Distance" returns the distance between to positions (snaps the piece to place)
        {
            if (!selected)
            {
                if(inRightPosition == false) {
                    transform.position = RightPosition;
                    inRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;

                    //Increment PiecesInPlace
                    MainCamera.GetComponent<DragAndDrop>().PiecesInPlace++;
                    Debug.Log(MainCamera.GetComponent<DragAndDrop>().PiecesInPlace);

                    //Pieces have the inRightPosition each
                    //Maybe there's a way to count all that
                    //or
                    //Maybe put them all inside a gameobject 
                }
            }
        } 
    }
}
