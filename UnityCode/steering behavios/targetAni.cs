using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetAni : MonoBehaviour
{
    public Transform target;
    public float fadeDistance = 5f;
    public float fadeSpeed = 1f;

    private Renderer renderer;
    private float targetAlpha = 1f;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (target != null)
        {
            // 타겟과의 거리 계산
            float distance = Vector3.Distance(transform.position, target.position);

            // 투명도 계산
            if (distance <= fadeDistance)
            {
                targetAlpha = 1f;
            }
            else
            {
                targetAlpha = 0.5f;
            }

            // 현재 투명도에서 목표 투명도로 부드럽게 변환
            float currentAlpha = Mathf.Lerp(renderer.material.color.a, targetAlpha, fadeSpeed * Time.deltaTime);

            // 투명도를 적용한 새로운 색상 생성
            Color newColor = renderer.material.color;
            newColor.a = currentAlpha;

            // 새로운 색상을 물체에 적용
            renderer.material.color = newColor;
        }
    }
}
