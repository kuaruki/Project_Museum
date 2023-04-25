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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            /*
             * Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                    interactObj.Interact();
            }*/


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
            }
        }
    }
}
