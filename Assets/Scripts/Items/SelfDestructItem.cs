using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructItem : BaseItem
{
    public override void Use()
    {

        owningInventory.RemoveItem(this);

    }
}
