using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public BaseItem goal;

    public BaseItem[] rewards;

    public Quest()
    {
        rewards = new BaseItem[4];
    }
    /*
    public bool AddReward(BaseItem b)
    {
        for (int i = 0; i < 4; i++)
        {
            if (rewards[i] == null)
            {
                rewards[i] = b;
                return true;
            }
        }
        return false;
    }


    public void RemoveReward(BaseItem b)
    {
        if (!ContainsItem(b)) { return; }

        for (int i = 0; i < 4; i++)
        {
            if(rewards[i] == b)
            {
                rewards[i] = null;
                return;
            }
        }
    }

    public bool ContainsItem(BaseItem b)
    {
        for (int i = 0; i < 4; i++)
        {
            if(rewards[i] == b)
            {
                return true;
            }
        }
        return false;
    }
    public void SetGoal(BaseItem b)
    {
        goal = b;
    }
    */
}
