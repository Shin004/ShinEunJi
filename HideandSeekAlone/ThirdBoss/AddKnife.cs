using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddKnife : MonoBehaviour
{
    private ThirdMiddleBoss script;
    SpriteRenderer rend;  //보스에 스프라이트렌더
    SpriteRenderer Rend;
    public float speed = 130;
    bool add = false;
    public Transform Base;
    Vector2 direction;

    Vector3 target;
    void Start()
    {
        script = GameObject.Find("Player").GetComponent<ThirdMiddleBoss>();
        rend = GameObject.Find("ThirdBoss").GetComponent<SpriteRenderer>();
        Rend = GetComponent<SpriteRenderer>();
        Invoke("OnDestroy", 5);

        int a = Random.Range(1, 3);  // 1=대각 위, 2 = 대각아래
        if(a==1)
        {
            Rend.flipX = true;
            Pos();
            direction = (target - transform.position).normalized;

            if (!rend.flipX)
            {
                transform.rotation = Quaternion.Euler(0, 0, 70);
            }
            if (rend.flipX)
            {
                transform.rotation = Quaternion.Euler(0, 0, 115);
            }
        }
        if(a==2)
        {
            Pos();
            Vector3 ab = new Vector3(transform.position.x, transform.position.y + 40, 1);
            transform.position = ab;
            direction = (target - transform.position).normalized;

            if(!rend.flipX)
            {
                transform.rotation = Quaternion.Euler(0, 0, 115);
            }
            if(rend.flipX)
            {
                transform.rotation = Quaternion.Euler(0, 0, 70);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if(add)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        Invoke("Forward", 1f);
        
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    void Forward()
    {
        add = true;
    }

    void Pos()  //좌표
    {
        if(rend.flipX)
        {
            target = new Vector3(Base.position.x + 70, Base.position.y + 20, 1);
        }
        if(!rend.flipX)
        {
            target = new Vector3(Base.position.x - 70, Base.position.y + 20, 1);
        }
    }
}