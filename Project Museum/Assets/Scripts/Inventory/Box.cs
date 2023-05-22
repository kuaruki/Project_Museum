using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : InventoryItemBase
{
    public override string Name
    {
        get 
        {
            return "Box";
        }
    }

    public override void OnUse()
    {
        // do something with the object
        base.OnUse();
    }
}
