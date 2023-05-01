using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

interface IInteractable {
    public void Interact();
}
public class Interactor : MonoBehaviour
{

    //Paintings
    private int paintOrder = 0;
    //if the player puts the correct painting in the correct place paintOrder++
    //if 3 paintings are down and paintOrder = 3
    //puzzle done
    Inventory inventory;
    [SerializeField] GameObject inventarioObject;
    ItemClickHandler clickHandler;
    [SerializeField] GameObject handlerDeCliques;

    //Safe
    public float InteractDistance = 15f;
    public LayerMask interactLayer;

    private void Awake()
    {
        //get the items from the inventory
        inventory = inventarioObject.GetComponent<Inventory>();
        clickHandler = handlerDeCliques.GetComponent<ItemClickHandler>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, InteractDistance, interactLayer))
        {
            //Here is where we can put a little hand to tell the player that he is facing an interactable object

            if (Input.GetKeyDown(KeyCode.E))
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
                //----------
                //Painting Puzzle
                //----------
                else if (hit.collider.CompareTag("PaintingPosition1"))
                {
                    //check if there's a painting in the place

                    //if there isn't...
                    //check if a painting is selected in the inventory
                    if (inventory.mItems.Count > 0)
                    {
                        foreach(IInventoryItem inventoryItem in inventory.mItems)
                        {
                            if (inventoryItem.OnUse())
                            {
                                clickHandler.GetComponent<IInventoryItem>();
                            }
                        }
                    }

                    //Snap painting to place
                    if (hit.transform.tag == "PaintingPosition1")
                    {
                        transform.position = hit.transform.position;
                    }
                }
                else if (hit.collider.CompareTag("PaintingPosition2"))
                {

                }else if (hit.collider.CompareTag("PaintingPosition3"))
                {

                }
                //----------
                // EOF Painting Puzzle
                //----------
            }
        }
    }
}
