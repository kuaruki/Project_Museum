using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        //Add name item
        get 
        {
            return "Box";
        }
    }

    //Add _Image
    public Sprite _Image;

    public Sprite Image
    {
        //Add sprite item
        get
        {
            return _Image;
        }
    }


    public void OnPickup()
    {
        //Add gameObject
        gameObject.SetActive(false);
    }
}
