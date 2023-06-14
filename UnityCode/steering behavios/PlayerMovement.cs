using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;
    private Vector3 targetDirection = Vector3.up; // 삼각형의 위쪽 방향

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 방향키 입력에 따라 이동 벡터 계산
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;

        // 현재 위치에 이동 벡터 더하기
        transform.position += movement;

        // 이동 방향에 따라 목표 회전 방향 설정
        if (movement.magnitude > 0.01f)
        {
            targetDirection = movement.normalized;
        }

        // 목표 회전 방향을 삼각형 위쪽 방향으로 맞추기
        float targetAngle = Vector3.SignedAngle(Vector3.up, targetDirection, Vector3.forward);

        // 현재 회전 각도에서 목표 회전 각도로 부드럽게 회전
        float currentAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }
}
