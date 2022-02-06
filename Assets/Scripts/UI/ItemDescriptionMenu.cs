using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDescriptionMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menuToEnable;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private TextMeshProUGUI itemTitle;

    [SerializeField]
    private Image itemImg;

    public static ItemDescriptionMenu instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        menuToEnable.SetActive(false);
    }
    public void CloseMenu()
    {
        menuToEnable.SetActive(false);
    }

    public void OpenMenu(string itemName, string description, Sprite imageFile)
    {
        menuToEnable.SetActive(true);
        descriptionText.text = "<b>Description: </b>" + description;
        itemTitle.text = itemName;
        itemImg.sprite = imageFile;
    }
}
