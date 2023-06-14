using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ThirdMiddleBoss : Enemy
{ //시전할 스킬 랜덤값으로 고름 List SetSkill = new List { "Knife", "Forward", "Summon", "Telepo" }; public int Knife_Pattern; //나이프 패턴, 접근 가능

    //몬스터 소환프리팹
    [SerializeField] protected GameObject Fly_1;
    [SerializeField] protected GameObject Knife;
    [SerializeField] protected GameObject MagicCircle;
    [SerializeField] protected GameObject tell;

    [SerializeField] protected Transform BossPos;
    [SerializeField] protected Transform PlayerPos;
    [SerializeField] protected Transform BasePos;
    //[SerializeField] protected ParticleSystem ps;

    int CurrentPos = 1;         //중보스에 현위치를 알려줌
    Transform SummonPos;    //몬스터 소환시 기점으로 하는 포스
    public SpriteRenderer rend;    //중보스 

    //Skill : 0 = 칼 던지기스킬, 1 = 돌진, 2 = 소환, 3 = 텔포        

    public Rigidbody2D riged;

    float ForWardTime = 0;      //돌진 진행하는 시간
    int BeforeSkill;            //중복이 되면 안되는 스킬이 있기 때문에 그걸 판단하는 정수 
    int SetSkillNum;
    float ForwardSpeed = 180;    //돌진 스피드  
    protected bool cooltime = false;
    protected Vector3 pos;                //초기화 변수

    protected float Summoncurtime = 20;
    protected float Summoncooltime = 30;

    //protected float retelepotime;

    Vector3 m_offset;

    //------------------------------------
    bool DashOn = false;
    bool SummonOn = false;
    bool KnifeOn = false;
    bool start = false;


    float timeUntilChangeState;
    bool onRest = false;


    bool First = true;

    float Sumontime = 20;
    bool aniF = true;



    override protected void Start()
    {
        base.Start();
        rend = GetComponent<SpriteRenderer>();
        riged = GetComponent<Rigidbody2D>();

        Invoke("Teleport", 2); //중앙에있던 보스를 텔포로 위치 잡기
        Invoke("OnStart", 2);
        InvokeRepeating("OnActive", 5, 3);

    }

    override protected void Update()
    {
        base.Update();
        ChangeState();

        if ((targetGameObject.transform.position - transform.position).magnitude <= mag && BossPlay)
        {
            if (start)
            {
                if (DashOn) ForwardSKill();
                if (KnifeOn)
                {
                    KnifeOn = false;
                    KnifeSkill();
                    Invoke("KnifeSkill", 6);
                    Invoke("Teleport", 8);

                }
                if (SummonOn)
                {
                    SummonSkill();
                    if (timeUntilChangeState <= 2)
                    {
                        Teleport();
                    }
                }
                Summoncurtime -= Time.deltaTime;
                timeUntilChangeState -= Time.deltaTime;
            }


        }

        //---------------------------------

    }


    private void LateUpdate()
    {
        if (HP <= 0)
        {
            //animator.SetTrigger("다이");
            Die.transform.position = transform.position;
            Die.Play();
            Invoke("HP300", 2f);
            //Invoke("다이", 2f);

        }
    }


    void KnifeSkill()
    {
        Debug.Log("나이프");
        Instantiate(Knife, new Vector2(BasePos.position.x, BasePos.position.y + 45), transform.rotation);
    }

    void SummonSkill()
    {
        SummonOn = false;
        Debug.Log("기본 소환");
        SummonSetPos();
        Instantiate(MagicCircle, pos, transform.rotation);
        Instantiate(Fly_1, pos, transform.rotation);

    }
    //-------------------------------------------------------
    public void ForwardSKill()
    {

        if (ForWardTime < 2f)
        {
            if (rend.flipX) //오른쪽 돌진
            {
                transform.Translate(transform.right * ForwardSpeed * Time.deltaTime);
            }
            if (!rend.flipX) //왼쪽 돌진
            {
                transform.Translate(transform.right * -1 * ForwardSpeed * Time.deltaTime);
            }
        }
        else
        {
            DashOn = false;
            riged.velocity = Vector2.zero;   //동작 정지
            ForWardTime = 0;  //다시 0           
            aniF = true;
            Teleport();
            timeUntilChangeState = 0;
        }
        ForWardTime += Time.deltaTime;  //시간증가
    }
    //-----------------------------------------------------------
    void Teleport()
    {
        //파티클
        Debug.Log("텔포");
        int a;
        a = CurrentPos;

        if (a == 0)
        {
            //오른쪽텔포
            //ps.Play();
            transform.position = new Vector2(BasePos.position.x + 90, BasePos.position.y);
            rend.flipX = false;
            CurrentPos = 1;
        }
        else
        {
            //왼쪽텔포
            //ps.Play();
            transform.position = new Vector2(BasePos.position.x - 90, BasePos.position.y);
            rend.flipX = true;
            CurrentPos = 0;
        }
    }
    //-----------------------------------------------------------------------

    void ChangeState()
    {
        if (!onRest && timeUntilChangeState <= 0)
        {
            timeUntilChangeState = 1.5f;
            onRest = true;
            DashOn = false;
            SummonOn = false;
            KnifeOn = false;
        }
        if (onRest && timeUntilChangeState <= 0)
        {
            onRest = false; int rand = Random.Range(0, 3);

            if (rand == 0)
            {
                KnifeOn = true;
                timeUntilChangeState = 10f;
            }
            else if (rand == 1)
            {
                //DashOn = true;
                Invoke("DaOn", 0.8f);
                animator.SetTrigger("대시");
                timeUntilChangeState = 4f;
            }
            else if (rand == 2)
            {
                if (Summoncurtime <= 0)
                {
                    Summoncurtime = 20;
                    SummonOn = true;
                    timeUntilChangeState = 5;
                }
            }

        }
    }

    //--------------------------------------------------------------
    void SummonSetPos()
    {
        int x = Random.Range(-60, 61);
        int y = Random.Range(15, 22);
        if (x > -20 && x < 20)
        {
            x = Random.Range(-60, 61);
        }

        pos = new Vector3(PlayerPos.position.x + x, PlayerPos.position.y + y);
    }

    void OnActive()
    {
        gameObject.SetActive(true);
    }

    void OnDash()
    {
        DashOn = true;
    }
    void OnStart()
    {
        start = true;
    }

    void Shake()
    {
        Debug.Log("shake");
        Vector3 root = transform.position;
        for (float cool = 10f; cool >= 0;)
        {
            float randx = Random.Range(-1f, 1f);
            float randy = Random.Range(-1f, 1f);

            m_offset = new Vector2(transform.position.x + randx, transform.position.y + randy);
            transform.position = m_offset;

            cool -= Time.deltaTime;
        }
        transform.position = root;

    }

    void DieAni()
    {
        //애니가 끝나고, 경험치 획득후(파티클 획득)
        GameObject.Find("Canvas").transform.Find("튜토리얼").transform.Find("스킬").transform.Find("설명 텍스트").gameObject.SetActive(true);
        Invoke("delaytutorial", 1f);
        SaveManager.Instance._playerData.killedBoss1 = true;  //이거 키면 게임 오브젝트 삭제
    }

    void delaytutorial()
    {
        GameObject.Find("Canvas").transform.Find("튜토리얼").transform.Find("스킬").transform.Find("스킬2").gameObject.SetActive(true);
    }

    void Boss3Color()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    void DaOn()
    {
        DashOn = true;
    }
    void HP300()
    {
        HP = 0;
        DieAni();
    }
}