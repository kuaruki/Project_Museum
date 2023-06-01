using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2_Progress : MonoBehaviour {
    public bool Cipher;
    public bool Wand;
    public bool Potions;
    public GameObject OpenedLabDoorPosition;
    public GameObject LabDoor;
    public GameObject MainCamera;
    void Start() {
        Cipher = false;
        Wand = false;
        Potions = false;
    }

    // Update is called once per frame
    void Update() {

        if(MainCamera.GetComponent<Interactor>().PotionsInPlace == 4) {
            Potions = true;
        }

        if (Cipher && Wand && Potions) { //if all puzzles are solved
            //Opens door to Room 2
            transform.rotation = OpenedLabDoorPosition.transform.rotation;
            LabDoor.layer = default;
        }
    }
}

