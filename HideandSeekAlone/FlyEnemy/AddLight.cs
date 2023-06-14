using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLight : MonoBehaviour
{
    protected GameObject targetGameObject;
    public float speed;
    Vector2 direction;
    void Start()
    {
        Invoke("deastroy",2f);
        targetGameObject = GameObject.FindWithTag("Player");
        direction = (targetGameObject.transform.position - transform.position).normalized; //방향 설정
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    void deastroy()
    {
        Destroy(gameObject);
    }
}
