using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoalSelecter : MonoBehaviour, IItemClickHandler
{
    QuestScreen questScreen;

    private void Awake()
    {
        questScreen = GetComponentInParent<QuestScreen>();
    }
    public void HandleItemUse(BaseItem b)
    {
        Debug.Log("selecting " + b.gameObject.name);
        questScreen.SetGoal(b);
    }
}
