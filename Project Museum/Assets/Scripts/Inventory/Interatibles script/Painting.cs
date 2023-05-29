using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : InventoryItemBase 
{
    public override string Name 
    {
        get 
        {
            return "Painting";
        }
    }

    public override void OnUse() 
    {
        // do something with the object
        base.OnUse();
    }
}
