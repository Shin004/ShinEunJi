using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
   public float ActiveTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            Invoke("Activefalse", ActiveTime);
        }
    }

    void Activefalse()
    {
        gameObject.SetActive(false);
    }
}
