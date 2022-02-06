
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReceiver : MonoBehaviour
{
    public enum QuestStatus
    {
        none,
        inprogress,
        done,
    }

    public QuestStatus status = QuestStatus.none;

    public QuestLiveUpdateDialog liveDialog;

    Quest currentQuest;






    public void StartQuest()
    {
        Debug.Log("starting quest");

        QuestScreen.instance.OpenQuestDialog(this);

        

    }


    public void QuestChanged(Quest q)
    {
        int likefactor = AssessQuest(q);

        DialogueEditor.ConversationManager.Instance.HijackText(liveDialog.GetDialog(likefactor));
    }

    public void SetQuest(Quest q)
    {
        currentQuest = q;
        DialogueEditor.ConversationManager.Instance.EndConversation();
        status = QuestStatus.inprogress;
    }

    public void FinishQuest()
    {
        status = QuestStatus.done;
    }
    
    private int AssessQuest(Quest q)
    {
        return Random.Range(0,5);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DialogueEditor.ConversationManager.Instance.ForceEnd();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FinishQuest();
        }
    }



}
