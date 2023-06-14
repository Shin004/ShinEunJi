using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public GameObject targetObject;

    void Start()
    {
        
    }


    void Update()
    {
        Vector3 targetPosition = targetObject.transform.position;
        Vector3 seekForce = Seek(targetPosition);


    }

    public Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desiredVelocity = (targetPos - transform.position).normalized * 10;
        return desiredVelocity;
    }
}
