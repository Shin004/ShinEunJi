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
            // Seek ���� ���� ���� ��
            Seek();
        }
    }

    private void Seek()
    {
        // Ÿ�� ��ġ�� ���ϴ� ���� ���͸� ���մϴ�.
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // �־��� �ӵ��� �̵��մϴ�.
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CreateFeelerCollider()
    {
        // BoxCollider2D ���� �� ����
        feelerCollider = gameObject.AddComponent<BoxCollider2D>();
        feelerCollider.isTrigger = true;
        feelerCollider.size = new Vector2(2f, 2f); // �˼��� ũ�⸦ �����ؾ� �մϴ�. �ʿ信 ���� ���� �����ϼ���.

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
