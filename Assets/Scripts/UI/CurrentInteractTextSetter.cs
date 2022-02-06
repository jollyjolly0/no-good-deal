using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CurrentInteractTextSetter : MonoBehaviour
{
    [SerializeField]
    private StringEvent actionChanged;
    [SerializeField]
    private TMPro.TextMeshProUGUI currentInteractText;

    private void Awake()
    {
        actionChanged.Fired += OnInteractableChanged;
    }

    void OnInteractableChanged(object sender, string s)
    {
        currentInteractText.text = s;
    }
}
