using UnityEngine;

public class SteeringBehaviors : MonoBehaviour
{
    public Transform target;
    public float seekRange = 10f;
    public float speed = 10f;
    public float wallAvoidanceForce = 10f;
    public float wallAvoidanceDistance = 1f;

    private void FixedUpdate()
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
            seekRange += 5;
        }
        else
        {
            Back();
            if (seekRange > 11)
            {
                seekRange -= 5;
            }
        }

        WallAvoidance();

        // 벽 회피 힘을 적용합니다.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void Seek()
    {
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    private void Back()
    {
        Vector3 direction = transform.position - target.position;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    private void WallAvoidance()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, wallAvoidanceDistance);

        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            Vector3 avoidanceForce = hit.normal * wallAvoidanceForce;
            transform.Rotate(0f, 0f, avoidanceForce.x * Time.deltaTime);
        }
    }
}