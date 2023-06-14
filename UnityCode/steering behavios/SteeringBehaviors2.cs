using UnityEngine;

public class SteeringBehaviors2 : MonoBehaviour
{
    public Transform gameobjectPos;
    Transform OriginalPos;

    public Transform target;
    public float seekRange = 10f; // 타겟을 추적하기 시작하는 거리
    public float speed = 10f; // 이동 속도
    public float wallAvoidanceForce = 10f; // 벽 회피 힘의 강도
    public float feelerLength = 1.5f; // Feelers의 길이
    public float feelerAngle = 45f; // Feelers의 각도
    public float maxRotationSpeed = 180f; // 최대 회전 속도

    private CircleCollider2D circleCollider; // 원형 콜라이더
    private PolygonCollider2D feelerCollider; // 폴리곤 콜라이더
    private Vector3 avoidanceForce = Vector3.zero; // 회피 힘

    public float circleWallAvoidanceForce = 5f; // 원형 벽 회피 힘의 강도

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

        // 움직임이 있다면 Feelers를 업데이트하고 벽 회피를 진행합니다.
        if (transform.hasChanged)
        {
            CreateFeelerCollider();
            WallAvoidance();
            CircleWallAvoidance();
            transform.hasChanged = false;
        }

        // 벽 회피 힘을 적용합니다.
        transform.Translate(avoidanceForce * Time.deltaTime);
    }

    private void Seek()
    {
        // 타겟 위치로 향하는 방향 벡터를 구합니다.
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // 회전할 각도를 구합니다.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 회전합니다.
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // 폴리곤 콜라이더의 회전도 동일하게 설정합니다.
        Quaternion colliderRotation = Quaternion.LookRotation(Vector3.forward, direction);
        feelerCollider.transform.rotation = colliderRotation;

        // 전방으로 이동합니다.
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void Back()
    {
        Vector3 direction = OriginalPos.position - transform.position;

        // 회전할 각도를 구합니다.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 회전합니다.
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // 폴리곤 콜라이더의 회전도 동일하게 설정합니다.
        Quaternion colliderRotation = Quaternion.LookRotation(Vector3.forward, direction);
        feelerCollider.transform.rotation = colliderRotation;

        // 전방으로 이동합니다.
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

        // PolygonCollider2D 생성 및 설정
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
        // CircleCollider2D가 이미 생성되어 있는지 확인합니다.
        if (circleCollider == null)
        {
            // CircleCollider2D 생성 및 설정
            circleCollider = gameObject.AddComponent<CircleCollider2D>();
            circleCollider.radius = 2f; // 반지름 설정
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
                // 벽과 객체 사이의 가장 가까운 점을 계산
                Vector3 closest = collider.ClosestPoint(transform.position);
                float distance = Vector3.Distance(transform.position, closest);

                // 가장 가까운 벽과 충돌하는 Feelers를 찾기
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = closest;
                }
            }
        }

        if (closestDistance < feelerLength)
        {
            // 벽 회피 힘을 계산
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
                // 원형 벽과 객체가 충돌하는지 확인합니다.
                if (circleCollider.IsTouching(collider))
                {
                    // 벽과의 방향 벡터를 계산합니다.
                    Vector3 direction = collider.transform.position - transform.position;

                    // 회피 힘을 계산합니다.
                    Vector3 avoidanceForce = direction.normalized * circleWallAvoidanceForce;

                    // 회피 힘을 적용합니다.
                    transform.Translate(avoidanceForce * Time.deltaTime);
                }
            }
        }
    }
}
