using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 using DialogueEditor;
public class DialogInteractable : BaseInteractable
{
    [SerializeField]
    private NPCConversation conversation;

    [SerializeField]
    private AINavigation myAI;
    private NPCConversation[] converstaions;
    private QuestReceiver questReceiver;

    private void Awake()
    {
        converstaions = GetComponentsInChildren<NPCConversation>();
        questReceiver = GetComponent<QuestReceiver>();
    }

    public override string GetActionName()
    {
        return "Talk";
    }

    public override void Interact(GameObject actor)
    {
        DisableAIMovementDuringDialog();
        ConversationManager.Instance.StartConversation(GetConversation());
    }

    private NPCConversation GetConversation()
    {
        switch (questReceiver.status)
        {
            case QuestReceiver.QuestStatus.none:
                return TryGetDialogBy("want");
                break;
            case QuestReceiver.QuestStatus.inprogress:
                return TryGetDialogBy("inprogress");
                break;
            case QuestReceiver.QuestStatus.done:
                return TryGetDialogBy("done");
                break;
            default:
                break;
        }


        return null;
    }

    private void DisableAIMovementDuringDialog()
    {
        myAI.TalkToAI();
    }

    private void EnableAIMovementAfterDialog()
    {
        myAI.EndTalkToAI();
    }

    private NPCConversation TryGetDialogBy(string suffix)
    {
        foreach (var item in converstaions)
        {
            if (item.name.EndsWith(suffix))
            {
                return item;
            }
        }
        Debug.LogWarning("COUDLNT FIND DIALOG ON " + gameObject.transform.root.name + " with suffix " + suffix);
        return null;
    }


}
