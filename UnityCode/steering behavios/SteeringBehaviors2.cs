using UnityEngine;

public class SteeringBehaviors2 : MonoBehaviour
{
    public Transform gameobjectPos;
    Transform OriginalPos;

    public Transform target;
    public float seekRange = 10f; // Ÿ���� �����ϱ� �����ϴ� �Ÿ�
    public float speed = 10f; // �̵� �ӵ�
    public float wallAvoidanceForce = 10f; // �� ȸ�� ���� ����
    public float feelerLength = 1.5f; // Feelers�� ����
    public float feelerAngle = 45f; // Feelers�� ����
    public float maxRotationSpeed = 180f; // �ִ� ȸ�� �ӵ�

    private CircleCollider2D circleCollider; // ���� �ݶ��̴�
    private PolygonCollider2D feelerCollider; // ������ �ݶ��̴�
    private Vector3 avoidanceForce = Vector3.zero; // ȸ�� ��

    public float circleWallAvoidanceForce = 5f; // ���� �� ȸ�� ���� ����

    private void Start()
    {
        OriginalPos = gameobjectPos;
        CreateFeelerCollider();
        CreateCircleCollider();
    }

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
            // Seek ���� ���� ���� ��
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

        // �������� �ִٸ� Feelers�� ������Ʈ�ϰ� �� ȸ�Ǹ� �����մϴ�.
        if (transform.hasChanged)
        {
            CreateFeelerCollider();
            WallAvoidance();
            CircleWallAvoidance();
            transform.hasChanged = false;
        }

        // �� ȸ�� ���� �����մϴ�.
        transform.Translate(avoidanceForce * Time.deltaTime);
    }

    private void Seek()
    {
        // Ÿ�� ��ġ�� ���ϴ� ���� ���͸� ���մϴ�.
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // ȸ���� ������ ���մϴ�.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ȸ���մϴ�.
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // ������ �ݶ��̴��� ȸ���� �����ϰ� �����մϴ�.
        Quaternion colliderRotation = Quaternion.LookRotation(Vector3.forward, direction);
        feelerCollider.transform.rotation = colliderRotation;

        // �������� �̵��մϴ�.
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void Back()
    {
        Vector3 direction = OriginalPos.position - transform.position;

        // ȸ���� ������ ���մϴ�.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ȸ���մϴ�.
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // ������ �ݶ��̴��� ȸ���� �����ϰ� �����մϴ�.
        Quaternion colliderRotation = Quaternion.LookRotation(Vector3.forward, direction);
        feelerCollider.transform.rotation = colliderRotation;

        // �������� �̵��մϴ�.
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CreateFeelerCollider()
    {
        
        
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxRotationSpeed * Time.deltaTime);

        
        Destroy(feelerCollider);

        // PolygonCollider2D ���� �� ����
        feelerCollider = gameObject.AddComponent<PolygonCollider2D>();
        feelerCollider.isTrigger = true;

        Vector2[] points = new Vector2[5];

        //feelerAngle = 45;
        Vector3 firstPoint = Quaternion.Euler(0, 0, -feelerAngle) * direction * feelerLength;
        points[0] = firstPoint;

        points[1] = Quaternion.Euler(0, 0, 0) * direction * (feelerLength - 0.2f);

        Vector3 secondPoint = Quaternion.Euler(0, 0, feelerAngle) * direction * feelerLength;
        points[2] = secondPoint;

        points[3] = Vector2.zero;

        points[4] = firstPoint;

        feelerCollider.points = points;
    }

    private void CreateCircleCollider()
    {
        // CircleCollider2D�� �̹� �����Ǿ� �ִ��� Ȯ���մϴ�.
        if (circleCollider == null)
        {
            // CircleCollider2D ���� �� ����
            circleCollider = gameObject.AddComponent<CircleCollider2D>();
            circleCollider.radius = 2f; // ������ ����
            circleCollider.isTrigger = true;
        }
    }

    private void WallAvoidance()
    {
        avoidanceForce = Vector3.zero;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, feelerLength);
        float closestDistance = float.MaxValue;
        Vector3 closestPoint = Vector3.zero;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Wall"))
            {
                // ���� ��ü ������ ���� ����� ���� ���
                Vector3 closest = collider.ClosestPoint(transform.position);
                float distance = Vector3.Distance(transform.position, closest);

                // ���� ����� ���� �浹�ϴ� Feelers�� ã��
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = closest;
                }
            }
        }

        if (closestDistance < feelerLength)
        {
            // �� ȸ�� ���� ���
            Vector3 direction = transform.position - closestPoint;
            avoidanceForce = direction.normalized * wallAvoidanceForce;
        }
    }

    private void CircleWallAvoidance()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Wall"))
            {
                // ���� ���� ��ü�� �浹�ϴ��� Ȯ���մϴ�.
                if (circleCollider.IsTouching(collider))
                {
                    // ������ ���� ���͸� ����մϴ�.
                    Vector3 direction = collider.transform.position - transform.position;

                    // ȸ�� ���� ����մϴ�.
                    Vector3 avoidanceForce = direction.normalized * circleWallAvoidanceForce;

                    // ȸ�� ���� �����մϴ�.
                    transform.Translate(avoidanceForce * Time.deltaTime);
                }
            }
        }
    }
}
