using UnityEngine;
using UnityEngine.EventSystems;

public class HealthBar : MonoBehaviour
{
    public float forceMagnitude = 10f; // 적용할 힘의 크기

    private Rigidbody2D targetRigidbody; // 힘이 가해질 오브젝트의 Rigidbody2D 컴포넌트

    private void Start()
    {
        targetRigidbody = GetComponentInParent<Rigidbody2D>(); // 현재 오브젝트의 부모 오브젝트에서 Rigidbody2D를 찾습니다.
    }


    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            // 마우스 클릭 시 힘을 가할 방향을 계산합니다.
            Vector2 forceDirection = Vector2.up;

            // 힘이 가해질 오브젝트에 힘을 가합니다.
            if (targetRigidbody != null)
            {
                targetRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
            }
        }
    }
}