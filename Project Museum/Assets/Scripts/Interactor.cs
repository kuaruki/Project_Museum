using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

interface IInteractable {
    public void Interact();
}
public class Interactor : MonoBehaviour
{

    //Jigsaw
    //public Transform InteractorSource;
    //public float InteractRange;

    //Safe
    public float InteractDistance = 15f;
    public LayerMask interactLayer;
    public GameObject PaintingPuzzleScript;

    void Start()
    {
        PaintingPuzzleScript.GetComponent<PaintingsScript>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Here is where we can put a little hand to tell the player that he is facing an interactable object


            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, InteractDistance, interactLayer))
            {
                if (hit.collider.CompareTag("Safe"))
                {
                    //Show the Safe UI
                    hit.collider.GetComponent<SafeScript>().ShowSafeCanvas();
                }
                else if (hit.collider.CompareTag("Jigsaw"))
                {
                    //Open Jigsaw Scene
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    SceneManager.LoadScene("Jigsaw2D");
                }
                else if (hit.collider.CompareTag("Cipher"))
                {
                    //Show the Cipher UI
                    hit.collider.GetComponent<CipherScript>().ShowCipherCanvas();
                }
                else if (hit.collider.CompareTag("PaintingsPuz"))
                {
                    PaintingPuzzleScript.GetComponent<PaintingsScript>().enabled = true;
                    //Run the Painting Puzzle
                    hit.collider.GetComponent<PaintingsScript>().MoveCamera();
                }
            }
        }
    }
}
