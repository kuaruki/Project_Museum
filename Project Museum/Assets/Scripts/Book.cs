using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : InventoryItemBase
{

    public override string Name
    {
        get
        {
            return "Book";
        }
    }

    public override void OnUse()
    {
        // do something with the object
        base.OnUse();
    }
}
