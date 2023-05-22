using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Vase";
        }
    }

    public override void OnUse()
    {
        // do something with the object
        base.OnUse();
    }
}