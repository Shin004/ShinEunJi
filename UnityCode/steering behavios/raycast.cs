using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/*
 * using UnityEngine;

public class SteeringBehaviors2 : MonoBehaviour
{
    public Transform target;
    public float seekRange = 10f;
    public float speed = 10f;

    private BoxCollider2D feelerCollider;

    private void Start()
    {
        CreateFeelerCollider();
    }

    private void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned.");
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= seekRange)
        {
            // Seek 범위 내에 있을 때
            Seek();
        }
    }

    private void Seek()
    {
        // 타겟 위치로 향하는 방향 벡터를 구합니다.
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // 주어진 속도로 이동합니다.
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CreateFeelerCollider()
    {
        // BoxCollider2D 생성 및 설정
        feelerCollider = gameObject.AddComponent<BoxCollider2D>();
        feelerCollider.isTrigger = true;
        feelerCollider.size = new Vector2(2f, 2f); // 촉수의 크기를 조정해야 합니다. 필요에 따라 값을 변경하세요.

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Debug.Log("Wall detected at position: " + other.transform.position);
        }
    }
}
*/
