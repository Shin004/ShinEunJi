using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleRotation : MonoBehaviour
{
    int rot;
    void Start()
    {
        rot = Random.Range(0, 46);
        InvokeRepeating("rota", 0, 0.5f);
        Invoke("OnDestroy", 10);
    }

    void Update()
    {
        rot = Random.Range(0, 46);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }

    void rota()
    {
        transform.Rotate(0, 0, rot);
    }
}
