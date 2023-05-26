using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPurple : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "PotionPurple";
        }
    }

    public override void OnUse()
    {
        // do something with the object
        base.OnUse();
    }
}
