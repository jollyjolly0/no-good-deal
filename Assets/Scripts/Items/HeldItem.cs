using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : BaseItem
{
    public Sprite holdSprite;



    
    public override void Use()
    {
        owningInventory.EquipItem(this);
    }
}
