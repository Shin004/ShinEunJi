using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAni : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        Ani();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Ani()
    {
        animator.SetTrigger("start");
    }
}
