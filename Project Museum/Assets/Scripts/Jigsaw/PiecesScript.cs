using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesScript : MonoBehaviour
{
    Vector3 RightPosition;
    public bool inRightPosition = false;
    public bool selected;
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3 (Random.Range(105f, 113f), Random.Range(-57f,-50.44f), RightPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, RightPosition) < 1f)//Returns the distance between to positions (useful for the wand puzzle)
        {
            if (!selected)
            {
                transform.position = RightPosition;
                inRightPosition = true;
            }
        } 
    }
}
