using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : InventoryItemBase {
    public override string Name {
        get {
            return "Wand";
        }
    }

    public override void OnUse() {
        // do something with the object
        base.OnUse();
    }
}
