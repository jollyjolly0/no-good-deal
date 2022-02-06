using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvent : MonoBehaviour
{
    [SerializeField]
    public enum DialogEventType
    {
        test,
            test2
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestDialog()
    {
        Debug.Log("AWDAWDAWD");
    }

    public void TestDialog(DialogEventType t)
    {
        Debug.Log("event type event");
        if(t == DialogEventType.test)
        {
            Debug.Log("further test1");
        }
        else
        {
            Debug.Log("further test2");
        }
    }
}
