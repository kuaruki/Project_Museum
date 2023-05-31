using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1_Progress : MonoBehaviour
{
    public bool Jigsaw;
    public bool Safe;
    public bool Paintings;
    public GameObject OpenedLivrosPosition;
    void Start()
    {
        Jigsaw = false;
        Safe = false;  
        Paintings = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Jigsaw && Safe && Paintings) { //if all puzzles are solved
            //Opens door to Room 2
            transform.position = OpenedLivrosPosition.transform.position;
        }
    }
}
