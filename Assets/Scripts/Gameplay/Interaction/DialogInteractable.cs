using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 using DialogueEditor;
public class DialogInteractable : BaseInteractable
{
    [SerializeField]
    private NPCConversation conversation;

    public override string GetActionName()
    {
        return "Talk";
    }

    public override void Interact()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }


}
