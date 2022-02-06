using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    protected Inventory owningInventory;

    public void Setup(Inventory i )
    {
        owningInventory = i;
    }

    public virtual void Use()
    {
        Debug.Log("using "+ name);
    }

}
