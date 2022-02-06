using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QuestScreen : MonoBehaviour 
{
    public BaseItem[] possibleQuestGoals;
    public BaseItem testBaseitem;

    public Quest quest;

    public InventoryQuestItemHandler inventoryQuest;

    public delegate void ItemEvent(BaseItem b);

    [SerializeField]
    private RectTransform[] rewardLocations;

    private BaseItem[] rewardIcons = new BaseItem[4];

    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescrip;

    [SerializeField]
    private GameObject questUI;

    public static QuestScreen instance;

    private QuestReceiver currentReceiver;

    private void Awake()
    {
        if(instance == null)
        {
            instance =this;
        }
        else
        {
            Debug.LogWarning("duplicate " + gameObject.name + "in scene");
            Destroy(this);
        }

    }
    private void Start()
    {
        //inventory.giveItemEvent += InventoryOfferItem;

        Instantiate(testBaseitem,transform);
    }



    Quest currentQuest;
    public void OpenQuestDialog(QuestReceiver receiver)
    {
        questUI.SetActive(true);
        GameState.SetState(GameState.State.QuestGiving);

        inventoryQuest.StartNewQuestDialog(this);

        currentQuest = new Quest();
        rewardIcons = new BaseItem[4];

        currentReceiver = receiver;



        SetGoal(possibleQuestGoals[0]);
    }

    public void AcceptQuest()
    {

    }
    public void CloseQuestDialog()
    {
        questUI.SetActive(false);
    }

    

    public bool AddReward(BaseItem b)
    {

        for (int i = 0; i < 4; i++)
        {
            if (currentQuest.rewards[i] == null)
            {
                currentQuest.rewards[i] = b;

                var g = Instantiate(b.gameObject, rewardLocations[i]);
                g.transform.localPosition = Vector2.zero;
                rewardIcons[i] = g.GetComponent<BaseItem>();

                QuestChanged();

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
            if (currentQuest.rewards[i] == b)
            {

                currentQuest.rewards[i] = null;

                BaseItem g = rewardIcons[i];
                rewardIcons[i] = null;
                GameObject.Destroy(g.gameObject);

                return;
            }
        }
        QuestChanged();
    }

    public bool ContainsItem(BaseItem b)
    {
        for (int i = 0; i < 4; i++)
        {
            if (currentQuest.rewards[i] == b)
            {
                return true;
            }
        }
        return false;
    }
    public void SetGoal(BaseItem b)
    {
        currentQuest.goal = b;

        questName.text = "Quest for the " + b.itemScriptableObject.itemName;
        questDescrip.text = b.itemScriptableObject.questDescription;


        foreach (var item in possibleQuestGoals)
        {
            item.GetComponent<InventoryElementVisuals>().SetSelectOutline(false);
        }

        b.GetComponent<InventoryElementVisuals>().SetSelectOutline(true);

        QuestChanged();

    }

    private void QuestChanged()
    {
        currentReceiver.QuestChanged(currentQuest);
    }




}
