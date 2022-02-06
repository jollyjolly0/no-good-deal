using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryClickSelector : MonoBehaviour , IItemClickHandler
{

    Inventory inventory;
    InventoryQuestItemHandler questInventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
        questInventory = GetComponent<InventoryQuestItemHandler>();
    }
    public void HandleItemUse(BaseItem b)
    {
        if(GameState.IsState(GameState.State.QuestGiving))
        {
            questInventory.HandleClick(b);
        }
        else
        {
            inventory.HandleItemUse(b);
        }
    }

}
