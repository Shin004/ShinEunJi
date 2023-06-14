using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWater : MonoBehaviour
{
    float X;
    float Y;
    float speed;
    Vector3 Pos;
    public Transform PlayerPos;
    void Start()
    {
        PlayerPos = GameObject.Find("Player").transform;
        speed = Random.Range(40, 70);
        Invoke("OnDestroy", 3);
        X = Random.Range(-500, 500);
        Y = Random.Range(-150, 20);
        Pos = new Vector3(transform.position.x + X, transform.position.y + Y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Y < -10 && Y > -80)
        {
            Y = Random.Range(-500, 200);
            Pos = new Vector3(transform.position.x + X, transform.position.y + Y, 1);
        }
        if (X < 110 && Y > -110)
        {
            X = Random.Range(-500, 500);
            Pos = new Vector3(PlayerPos.position.x + X, PlayerPos.position.y + Y, 1);
        }
        transform.position = Vector3.MoveTowards(transform.position, Pos, speed * Time.deltaTime);
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
