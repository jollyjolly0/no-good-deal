using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryQuestItemHandler : MonoBehaviour
{
    private QuestScreen currentQuest;
    public void StartNewQuestDialog(QuestScreen q)
    {
        currentQuest = q;
    }



    public void HandleClick(BaseItem b)
    {

        if (currentQuest.ContainsItem(b))
        {
            b.GetComponent<InventoryElementVisuals>().SetSelectOutline(false);
            currentQuest.RemoveReward(b);
        }
        else
        {
            b.GetComponent<InventoryElementVisuals>().SetSelectOutline(true);
            currentQuest.AddReward(b);
        }

    }
}
