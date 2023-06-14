using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;
    private Vector3 targetDirection = Vector3.up; // �ﰢ���� ���� ����

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ����Ű �Է¿� ���� �̵� ���� ���
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;

        // ���� ��ġ�� �̵� ���� ���ϱ�
        transform.position += movement;

        // �̵� ���⿡ ���� ��ǥ ȸ�� ���� ����
        if (movement.magnitude > 0.01f)
        {
            targetDirection = movement.normalized;
        }

        // ��ǥ ȸ�� ������ �ﰢ�� ���� �������� ���߱�
        float targetAngle = Vector3.SignedAngle(Vector3.up, targetDirection, Vector3.forward);

        // ���� ȸ�� �������� ��ǥ ȸ�� ������ �ε巴�� ȸ��
        float currentAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }
}
