using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Gem";
        }
    }

    public override void OnUse()
    {
        // do something with the object
        base.OnUse();
    }
}
