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
            // Ÿ�ٰ��� �Ÿ� ���
            float distance = Vector3.Distance(transform.position, target.position);

            // ���� ���
            if (distance <= fadeDistance)
            {
                targetAlpha = 1f;
            }
            else
            {
                targetAlpha = 0.5f;
            }

            // ���� �������� ��ǥ ������ �ε巴�� ��ȯ
            float currentAlpha = Mathf.Lerp(renderer.material.color.a, targetAlpha, fadeSpeed * Time.deltaTime);

            // ������ ������ ���ο� ���� ����
            Color newColor = renderer.material.color;
            newColor.a = currentAlpha;

            // ���ο� ������ ��ü�� ����
            renderer.material.color = newColor;
        }
    }
}
