using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoalSelecter : MonoBehaviour, IItemClickHandler
{
    public void HandleItemUse(BaseItem b)
    {
        Debug.Log("selecting " + b.gameObject.name);
    }
}
