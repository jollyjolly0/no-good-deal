using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmenManager : MonoBehaviour
{
    public GameObject [] omens;
    float timer = 0;
    public int vItemCount = 0;
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
        if (Random.Range(0.0f, 1.0f) > (0.5 + vItemCount * 0.1))
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
