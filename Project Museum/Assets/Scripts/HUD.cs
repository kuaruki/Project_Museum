using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;

    public GameObject MessagePanel;

    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += Inventory_ItemRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach(Transform slot in inventoryPanel)
        {
            //Border... Image
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            //We found the empty slot
            if(!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                // Store a reference to the item
                itemDragHandler.Item = e.Item;

                break;
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            Debug.Log(e.Item);//We found the item in the UI
            if (itemDragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDragHandler.Item = null;

                break;
            }
        }
    }

    // opens the message panel when item can be picked up
    public void OpenMessagePanel(string text)
    {
        MessagePanel.SetActive(true);

        // the string text can be used to change and add other messages as well 
    }

    // closes the message panel when item can't be picked up
    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }
}
