using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmenManager : MonoBehaviour
{
    public GameObject[] omens;
    public int vItemCount = 0;
    float timer = 0;
    float spawnTime = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        bool spawn = false;
        if ((vItemCount * 0.1) > Random.Range(0.0f, 1.0f))
        {
            spawn = true;
        }

        if (timer >= spawnTime && spawn)
        {
            Instantiate(omens[0]);
            timer = 0;
        }
    }
}
