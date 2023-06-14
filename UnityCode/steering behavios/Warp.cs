using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public Transform targetWarpPoint; // Ÿ�� ���� ������ �޴� ����


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾� �±׸� ���� ������Ʈ���� �浹 üũ
        {
            WarpToTarget(other.gameObject); // Ÿ�� ���� �������� ����
        }
    }

    private void WarpToTarget(GameObject player)
    {
        player.transform.position = targetWarpPoint.position;
    }
}
