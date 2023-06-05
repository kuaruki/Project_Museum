using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2_Progress : MonoBehaviour {
    public bool Cipher;
    public bool Wand;
    public bool Potions;

    public bool HasPlayed = false;
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
            MainCamera.GetComponent<Interactor>().isLabDoorOpen = true;
            transform.rotation = OpenedLabDoorPosition.transform.rotation;
            LabDoor.layer = 0;
            if(!HasPlayed) {
                MainCamera.GetComponent<Interactor>().DoorOpen.Play();
                HasPlayed = true;
            }
        }
        
    }
    public void OpenDoor() {
        
    }
}

