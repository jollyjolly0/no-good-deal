
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestReceiver : MonoBehaviour
{

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
    }

    public void FinishQuest()
    {
        Debug.Log("finishing quest");
    }


}
