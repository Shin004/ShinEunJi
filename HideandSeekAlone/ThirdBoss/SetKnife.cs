using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetKnife : ThirdMiddleBoss
{
    bool KnifeOn = true;
    override protected void Start()
    {
        
    }

    // Update is called once per frame
    override protected void Update()
    {
        if (KnifeOn)
        {
            KnifeOn = false;
            KnifeSkill();
            Invoke("KnifeSkill", 6);
            //Invoke("Teleport", 5);

        }

    }

    void KnifeSkill()
    {
        Debug.Log("나이프");


        Instantiate(Knife, transform);  //대각 위

    }

}
