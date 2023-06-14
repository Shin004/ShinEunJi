using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLight : FlyingMonster
{
    public GameObject Lightwave;
    float cooltime = 5;
    float curtime;
    //float time = 0;

    override protected void Start()
    {
        base.Start();
        curtime = 2;
        DieStage = 7;
    }

    override protected void Update()
    {

        if (HP <= 0)
        {
            transform.GetChild(0).tag = "Untagged";

            rigid.gravityScale = 300f;
            animator.SetTrigger("´ÙÀÌ");
            spriteRend.color = new Color(0.8f, 0.8f, 0.8f);
            Invoke("DIE", 1.7f);
            if (One)
            {
                GiveValue.expCurrent += Enhance_value;
                One = false;
            }
        }
        //-----------------------------------------------



        UpdateTarget();

        if(curtime <= 0&&SetOn)
        {
            
            Instantiate(Lightwave, pos.position, transform.rotation);
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;


        if (DiecurStage.Stage != DieStage && DiecurStage.Stage != 5)
        {

            Destroy(gameObject);
        }

    }
    void OnSetActive()
    {
        gameObject.SetActive(true);
    }
    public override void DIE()
    {
        base.DIE();
    }
}
