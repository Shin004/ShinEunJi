using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public Transform targetWarpPoint; // 타겟 워프 지점을 받는 변수


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어 태그를 가진 오브젝트와의 충돌 체크
        {
            WarpToTarget(other.gameObject); // 타겟 워프 지점으로 워프
        }
    }

    private void WarpToTarget(GameObject player)
    {
        player.transform.position = targetWarpPoint.position;
    }
}
