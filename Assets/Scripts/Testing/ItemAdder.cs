using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdder : MonoBehaviour
{
    public Inventory inv;

    public BaseItem addItem;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            inv.AddItem(addItem);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}
