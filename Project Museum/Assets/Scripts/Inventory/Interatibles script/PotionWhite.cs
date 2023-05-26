using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionWhite : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "PotionWhite";
        }
    }

    public override void OnUse()
    {
        // do something with the object
        base.OnUse();
    }
}
