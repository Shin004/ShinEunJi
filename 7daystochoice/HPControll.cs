using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPControll : MonoBehaviour
{
    public GameObject[] Hp;
    public GameObject Steminer;
    int currentHP = 2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(currentHP>GameManager.Heart)
        {
            currentHP = GameManager.Heart;
            if (GameManager.Heart < 2)
            {
                Hp[0].SetActive(false);
            }
            if (GameManager.Heart < 1)
            {
                Hp[1].SetActive(false);
            }
            Steminer.SetActive(true);
        }
    }
}
