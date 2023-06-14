using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDeatroy : MonoBehaviour
{
    float time = 0;
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        Invoke("OnDestroy", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0.8f) 
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time / 0.8f);
        }
        time += Time.deltaTime;
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
