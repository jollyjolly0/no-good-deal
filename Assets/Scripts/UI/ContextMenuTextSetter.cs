using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ContextMenuTextSetter : MonoBehaviour
{
    [SerializeField]
    private StringEvent actionChanged;
    [SerializeField]
    private TMPro.TextMeshProUGUI contextText;

    private void Awake()
    {
        actionChanged.Fired += OnInteractableChanged;
    }

    void OnInteractableChanged(object sender, string s)
    {
        contextText.text = s;
    }
}
