using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpray : MonoBehaviour
{
    [SerializeField] GameObject Small_Water;
    float time1 = 3;
    float time2 = 3;
    void Start()
    {
        Invoke("OnDestroy", 20);
    }

    // Update is called once per frame
    void Update()
    {
        if(time1 <= 0)
        {
            SetWater1();
        }
        if (time2 <= 0)
        {
            SetWater2();
        }
        time1 -= Time.deltaTime;
        time2 -= Time.deltaTime;
    }

    void SetWater1()
    {
        int Num = Random.Range(6, 10);
        for (int i =0; i < Num;i++)
        {
            Instantiate(Small_Water, transform.position, transform.rotation);
            
        }
        SetTime1();
    }
    void SetWater2()
    {
        int Num = Random.Range(5, 10);
        for (int i = 0; i < Num; i++)
        {
            Instantiate(Small_Water, transform.position, transform.rotation);

        }
        SetTime2();
    }
    
    void SetTime1()
    {
        time1 = Random.Range(1.0f, 1.4f);
    }
    void SetTime2()
    {
        time2 = Random.Range(0.6f, 1.0f);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }


}
