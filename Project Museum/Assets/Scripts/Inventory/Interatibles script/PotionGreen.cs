using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionGreen : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "PotionGreen";
        }
    }

    public override void OnUse()
    {
        // do something with the object
        base.OnUse();
    }
}
