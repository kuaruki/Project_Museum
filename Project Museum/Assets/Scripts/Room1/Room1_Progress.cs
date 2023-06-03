using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1_Progress : MonoBehaviour
{
    public bool Jigsaw;
    public bool Safe;
    public bool Paintings;
    public GameObject OpenedLivrosPosition;
    [SerializeField] private Camera MainCamera;
    void Start()
    {
        Jigsaw = false;
        Safe = false;  
        Paintings = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(MainCamera.GetComponent<Interactor>().PaintingsInPlace == 3) {
            Paintings = true;
        }
        
        if (Jigsaw && Safe && Paintings) { //if all puzzles are solved
            //Opens door to Room 2
            transform.position = OpenedLivrosPosition.transform.position;
            gameObject.layer = default;
        }
    }
}
