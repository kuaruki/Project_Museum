using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class PiecesScript : MonoBehaviour
{
    Vector3 RightPosition;
    public bool inRightPosition = false;
    public bool selected;
    void Start()
    {
        RightPosition = transform.position;
        Debug.Log("Right Position" + RightPosition);
        transform.position = new Vector3 (Random.Range(105f, 113f), Random.Range(-57f,-50.44f), RightPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, RightPosition) < 1f) //"Distance" returns the distance between to positions (useful for the wand puzzle)
        {
            if (!selected)
            {
                if(inRightPosition == false) {
                    transform.position = RightPosition;
                    inRightPosition = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                }
            }
        } 
    }
}
