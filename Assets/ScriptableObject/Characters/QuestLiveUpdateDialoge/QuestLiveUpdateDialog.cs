using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New QuestLiveUpdateDialog", menuName = "Dialog/QuestLiveUpdateDialog", order = 9999)]
public class QuestLiveUpdateDialog : ScriptableObject
{
    [SerializeField] private string hateQuest;
    [SerializeField] private string dislikeQuest;
    [SerializeField] private string nuetralQuest;
    [SerializeField] private string likeQuest;
    [SerializeField] private string loveQuest;

    public string GetDialog(int index)
    {
        if(index == 0) { return hateQuest; }
        if(index == 1) { return dislikeQuest; }
        if(index == 2) { return nuetralQuest; }
        if(index == 3) { return likeQuest; }
        if(index == 4) { return loveQuest; }

        return "INVALID INDEX";
    }
}
