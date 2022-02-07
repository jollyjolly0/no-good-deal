using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemScriptableObject", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public string questDescription;
    public int experience = 10;
    public int value = 10;
}
