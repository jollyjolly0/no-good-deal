using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    float timer = 0;
    public OmenManager omenManager;
    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 6)
        {
            omenManager.vItemCount += 1;
            timer = 0;
        }
    }
}
