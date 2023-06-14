using UnityEngine;

public class Health : MonoBehaviour
{
    public Transform minYObject; // 최소 y 위치를 제한할 오브젝트
    public Transform maxYObject; // 최대 y 위치를 제한할 오브젝트
    public float minSpeed = 1f; // 체력 이동의 최소 속도
    public float maxSpeed = 3f; // 체력 이동의 최대 속도
    public float minPauseTime = 2f; // 멈춰있는 최소 시간
    public float maxPauseTime = 6f; // 멈춰있는 최대 시간
    public float maxRange;

    private float currentSpeed; // 현재 체력 이동 속도
    private bool isMoving = false; // 체력 바의 움직임 여부
    private float pauseTimer; // 멈춰있는 시간을 측정하는 타이머
    private float destination; // 움직임의 도착지점

    public float slowSpeed;

    private void Start()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed); // 속도를 랜덤으로 설정
        SetDestination(); // 도착지점 설정
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveHealthBar();
        }
        else
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0f)
            {
                ResumeMovement();
            }
        }
    }

    private void MoveHealthBar()
    {
        // 체력 이동 로직을 구현합니다.
        // 예시로 체력 바를 상하로 이동시킵니다.
        float distanceToDestination = Mathf.Abs(transform.position.y - destination);
        float speedMultiplier = Mathf.Lerp(0.2f, 2.5f, Mathf.Clamp01(distanceToDestination / 150f) * slowSpeed);
        transform.Translate(Vector3.up * currentSpeed * speedMultiplier * Time.deltaTime);

        // 도착지점에 도착하면 움직임을 멈추도록 설정합니다.
        if ((currentSpeed > 0f && transform.position.y >= destination) ||
            (currentSpeed < 0f && transform.position.y <= destination))
        {
            StopMovement();
        }
    }

    private void StopMovement()
    {
        isMoving = false; // 움직임을 멈추도록 설정합니다.
        pauseTimer = Random.Range(minPauseTime, maxPauseTime); // 멈춰있는 시간을 랜덤으로 설정합니다.
    }

    private void ResumeMovement()
    {
        isMoving = true; // 움직임을 재개하도록 설정합니다.
        SetDestination(); // 도착지점을 새로 설정합니다.
    }

    private void SetDestination()
    {
        float minY = minYObject.position.y; 
        float maxY = maxYObject.position.y;

        // 도착지점을 랜덤으로 선택합니다.
        float minDestination = Mathf.Max(transform.position.y - Random.Range(2f,maxRange), minY);
        float maxDestination = Mathf.Min(transform.position.y + Random.Range(2f,maxRange), maxY);

        int a = Random.Range(0, 2);
        if(a == 0)
        {
            destination = minDestination;
        }
        else
        {
            destination = maxDestination;
        }

        if (destination < minY)
        {
            destination = minY;
        }
        if(destination > maxY)
        {
            destination = maxY;
        }


        // 현재 위치가 도착지점보다 위에 있는 경우, 아래로 움직이도록 설정
        if (transform.position.y > destination)
        {
            currentSpeed = -Mathf.Abs(currentSpeed);
        }
        // 현재 위치가 도착지점보다 아래에 있는 경우, 위로 움직이도록 설정
        else
        {
            currentSpeed = Mathf.Abs(currentSpeed);
        }
    }
}