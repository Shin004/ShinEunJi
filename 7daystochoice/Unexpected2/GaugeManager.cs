using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{

    public Image gaugeImage; // 게이지를 표시할 이미지
    public float fillRate = 1f; // 게이지의 초기 필렛 값
    public float increaseSpeed = 1f; // 충돌 시 게이지가 올라가는 속도
    public float decreaseSpeed = 1f; // 충돌이 없을 때 게이지가 내려가는 속도

    public bool GaugeStart = true; // 게이지 작동 여부를 나타내는 변수

    public GameObject arrowGame; // 활성화할 ArrowGame 오브젝트

    private bool isColliding = false; // 충돌 상태를 나타내는 플래그
    private float currentFill = 0f; // 현재 게이지의 채움 정도


    bool delay = false;

    private void Start()
    {
        // 게이지의 초기 값을 필렛 값으로 설정합니다.
        currentFill = fillRate;

        // 게이지 이미지에 초기 값을 반영합니다.
        gaugeImage.fillAmount = currentFill;
    }

    private void Update()
    {
        if (!GaugeStart)
            return;

        if (isColliding)
        {
            // 충돌 중인 경우 게이지를 증가시킵니다.
            currentFill += increaseSpeed * Time.deltaTime;
        }
        else if(!isColliding&& !delay)
        {
            // 충돌이 없는 경우 게이지를 감소시킵니다.
            currentFill -= decreaseSpeed * Time.deltaTime;

            // 필렛 값이 0보다 작아지면 처리합니다.
            if (currentFill < 0f)
            {
                currentFill = 1;
                delay = true;
                Invoke("DelayFalse", 2f);
                GameManager.Heart--;

            }
        }

        // 게이지의 채움 정도를 0과 1 사이로 유지합니다.
        currentFill = Mathf.Clamp01(currentFill);

        // 게이지 이미지에 채움 정도를 반영합니다.
        gaugeImage.fillAmount = currentFill;

        /*
        // 게이지가 가득 찼을 때 게임 오브젝트를 활성화합니다.
        if (currentFill >= 1f)
        {
            GaugeStart = false; // 게이지 작동 중지

            // ArrowGame 오브젝트를 활성화합니다.
            if (arrowGame != null)
            {
                arrowGame.SetActive(true);
            }
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heart"))
        {
            Debug.Log("게이지 오름");
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Heart"))
        {
            Debug.Log("게이지 정지");
            isColliding = false;
        }
    }
    void DelayFalse()
    {
        delay = false;
    }
}