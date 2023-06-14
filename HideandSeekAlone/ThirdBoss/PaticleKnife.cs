using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleKnife : MonoBehaviour
{
    public GameObject paticle;
    GameObject targetGameObject;

    Rigidbody2D rg;
    Transform ThrBoss;
    public ParticleSystem Ps;
    public Transform PlayerPos;
    float AddTime = 3;  //추격 끝내고 날라가는 시간
    float sp = 300;
    Vector2 direction;
    bool tr = true;
    bool PsStart = true;
    Vector3 lookDir;

    Transform Reset;


    float PlayPos;
    float mainers;

    bool move = true;
    public GameObject Pss;

    void Start()
    {
        //Ps.Stop();
        ThrBoss = GameObject.Find("ThirdBoss").transform;
        PlayerPos = GameObject.Find("Player").transform;
        Invoke("OnDestroy", 7f);
        Destroy(gameObject, 15f);
        Reset = transform;

        rg = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AddTime >= 1&&move)   //추격 함수
        {
            Vector3 dir = PlayerPos.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);


        }
        if (AddTime <= 1)
        {
            if(tr)
            {
                //direction = (PlayerPos.position - transform.position).normalized;  //마지막 추격pos 값
                direction = PlayerPos.position;
                tr = false; 
            }
            
        }
        if (AddTime <= 0)
        {
            //transform.Translate(direction * sp * Time.deltaTime);
            transform.position = Vector3.MoveTowards(Reset.position, direction, sp * Time.deltaTime);
            
        }
        if (AddTime < -1)
        {
            if (PsStart)
            {
                PsStart = false;
                Pss.SetActive(true);
            }

        }
        AddTime -= Time.deltaTime;



    }
    private void OnDestroy()
    {
        rg.velocity = Vector2.zero;
    }
}
