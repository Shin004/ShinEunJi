using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nohha : Enemy
{
    public GameObject WaterWave; //ÇÁ¸®ÆÕ °¡Á®¿È
    public float height;
 
    GameObject Shooting;

    float timeUntilChangeState;
    bool onShooting;
    bool onWaterWave;
    bool onRest;
    bool callWaterWave;


    protected override void Start()
    {
        base.Start(); //attacked(ÄðÅ¸ÀÓ), targetGameObject, rigid, spriteRend, animator ÃÊ±âÈ­
        Shooting = gameObject.transform.Find("ShootingWater").gameObject;
        Shooting.SetActive(false);
        timeUntilChangeState = 0f;
        onShooting = false;
        onWaterWave = false;
        callWaterWave = false;
        onRest = false;
    }

    protected override void Update()
    {
        base.Update();
        ChangeState();

        if (onShooting) Shooting.SetActive(true);
        else Shooting.SetActive(false);
        if (onWaterWave && !callWaterWave) Wave();
        if (onRest) Rest();
        timeUntilChangeState -= Time.deltaTime;
    }


    void Wave()
    {
        if (!onWaterWave) return;
        callWaterWave = true;

        Vector3 WavePosition = new Vector3(targetGameObject.transform.position.x, height, transform.position.z);
        Instantiate(WaterWave, WavePosition, transform.rotation);
        Invoke("Wave", 1);
    }

    void Rest()
    {

    }

    void ChangeState()
    {
        if (!onRest && timeUntilChangeState <= 0)
        {
            onRest = true;
            timeUntilChangeState = 1.5f;
            onWaterWave = false;
            callWaterWave = false;
            onShooting = false;
        }
        if (onRest && timeUntilChangeState <= 0)
        {
            onRest = false;
            timeUntilChangeState = 5f;
            int rand = Random.Range(0, 1);

            if (rand == 0)
            {
                onWaterWave = true;
                Debug.Log("onWaterWave");
            }
            else
            {
                onShooting = true;
                Debug.Log("onShooting");
            }
        }
    }
}
