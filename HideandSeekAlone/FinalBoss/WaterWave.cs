using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWave : MonoBehaviour
{
    public float speed;
    bool state1;
    bool state2;
    bool state3;
    public float timeToState2;
    public float timeToState3;
    public float timeToDestroy;

    Rigidbody2D rb;

    //  stop/up/stop

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state1 = true;
        //timeToState2 = 0.7f;
        //timeToDestroy = 0.5f;
    }

    void Update()
    {
        if (state1)
        {
            gameObject.tag = "Untagged";
            rb.velocity = Vector2.zero;
            timeToState2 -= Time.deltaTime;
            if (timeToState2 <= 0)
            {
                state1 = false;
                state2 = true;
            }
        }
        if (state2)
        {
            gameObject.tag = "Enemy";
            rb.velocity = new Vector2(0, speed);
            //transform.Translate(new Vector2(0, speed));
            timeToState3 -= Time.deltaTime;
            if (timeToState3 <= 0)
            {
                state2 = false;
                state3 = true;
            }
        }
        if (state3)
        {
            rb.velocity = Vector2.zero;
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy <= 0)
            {
                state3 = false;
                Destroy(gameObject);
            }
        }
    }
}
