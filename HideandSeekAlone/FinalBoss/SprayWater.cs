using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayWater : MonoBehaviour
{
    [SerializeField] GameObject Big_Water;
    [SerializeField] GameObject Summon_Monster;
    [SerializeField] GameObject MagicCircle;
    [SerializeField] Transform Base;
    SpriteRenderer rend;
    Vector3 Pos;
    int a = 0;

    public Transform PlayerPos;

    bool Skill_SprayWater = false;
    bool Skill_Teleport = false;
    bool Skill_Summon = true;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Pos = new Vector3(Base.position.x + 4, Base.position.y + 55, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Skill_SprayWater)
        {
            Water_Teleport();
        }
        if(Skill_Teleport)
        {
            Teleport();
        }
        if(Skill_Summon)
        {
            Summon();
        }
        
    }
    void Set_BigWter()
    {
        if (a==0)
        {
            Instantiate(Big_Water, Pos, transform.rotation);
            a++;
        }

    }

    void Water_Teleport()   //물폭탄 소환을 하기 위한 텔포
    {
        Skill_SprayWater = false;  //스킬 재 동작 금지
        gameObject.SetActive(false);
        transform.position = new Vector2(Base.position.x, Base.position.y);
        rend.flipX = false;
        Invoke("OnActive", 1f);
        Invoke("Set_BigWter", 1.5f);

    }

    void Summon()
    {
        gameObject.SetActive(false);
        Skill_Summon = false;
        Vector2 a = new Vector2(Base.position.x, Base.position.y - 10);
        Instantiate(MagicCircle, a, transform.rotation);
        Invoke("Summon_boss", 1f);
        Invoke("OnActive", 11f);
    }




    void Teleport()    //텔포
    {
        gameObject.SetActive(false);
        Skill_Teleport = false;
        float X = Random.Range(-50, 50);

        transform.position = new Vector2(PlayerPos.position.x + X, Base.position.y);
        Invoke("OnActive", 1.5f);
    }
    void OnActive()
    {
        gameObject.SetActive(true);
    }

    void Summon_boss()
    {
        Instantiate(Summon_Monster, Base.position, transform.rotation);
    }
}
