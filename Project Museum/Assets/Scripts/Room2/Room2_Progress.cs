using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2_Progress : MonoBehaviour {
    public bool Cipher;
    public bool Wand;
    public bool Potions;

    public Interactor interactor;

    public bool HasPlayed = false;
    public GameObject OpenedLabDoorPosition;
    [SerializeField] private Camera MainCamera;

    void Start() 
    {
        Cipher = false;
        Wand = false;
        Potions = false;

        interactor = GetComponent<Interactor>();
    }

    // Update is called once per frame
    void Update() {

        if(MainCamera.GetComponent<Interactor>().PotionsInPlace == 4) {
            Potions = true;
        }

        if (Cipher && Wand && Potions) { //if all puzzles are solved
            //Opens door to Room 3
            MainCamera.GetComponent<Interactor>().isLabDoorOpen = true; //atualiza isLabDoorOpen on the interactor script to true
            transform.rotation = OpenedLabDoorPosition.transform.rotation;
            gameObject.layer = 0;
            if (!HasPlayed) {
                interactor.DoorOpen.Play();
                HasPlayed = true;
            }
        }
    }
}

