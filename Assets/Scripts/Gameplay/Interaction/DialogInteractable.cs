using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 using DialogueEditor;
public class DialogInteractable : BaseInteractable
{
    private NPCConversation conversation;
    private void Awake()
    {
        conversation = GetComponent<NPCConversation>();
    }

    public override string GetActionName()
    {
        return "Talk";
    }

    public override void Interact()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }


}
