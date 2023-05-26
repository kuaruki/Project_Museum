using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionRed : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "PotionRed";
        }
    }

    public override void OnUse()
    {
        // do something with the object
        base.OnUse();
    }
}
