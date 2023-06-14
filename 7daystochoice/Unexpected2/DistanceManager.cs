using UnityEngine;
using UnityEngine.UI;

public class DistanceManager : MonoBehaviour
{
    public Text distanceText; // UI 텍스트 오브젝트를 연결할 변수
    public float speed = 1f; // 거리가 줄어드는 속도
    public GameObject HeartGame;
    public GameObject ArrowGame;
    public GameObject Heart;  // 하트게임오브젝트 리셋
    public GameObject GaugeBar; //게이지바 리셋
    public Transform min;


    bool TimeStop = false;


    private void Start()
    {
        GameManager.distance = 10;
    }

    private void Update()
    {
        if(!TimeStop)
        {
            // 시간에 따라 거리가 줄어들도록 계산
            GameManager.distance -= speed * Time.deltaTime;
        }
        

        // 거리 텍스트 업데이트
        UpdateDistanceText();
        Zerodistance();
    }

    private void UpdateDistanceText()
    {
        // 텍스트 내용을 포맷에 맞게 업데이트
        string text = "다음 지점까지: " + GameManager.distance.ToString("F2") + " m";
        distanceText.text = text;
    }

    void Zerodistance()
    {

        
        if(GameManager.distance <= 0 && !TimeStop)
        {
            TimeStop = true;
            GameManager.distance = 0;
            Heart.transform.position = new Vector3(Heart.transform.position.x, min.position.y, Heart.transform.position.z);  //하트 위치 초기화
            GaugeBar.transform.position = new Vector3(GaugeBar.transform.position.x, min.position.y+0.5f, GaugeBar.transform.position.z);  //하트 위치 초기화
            HeartGame.SetActive(false);  //게이지 게임 비활성화
            GameManager.isCheakLound = true; //방향키게임 시작
            ArrowGame.SetActive(true); //방향키 게임 활성화
            

        }
        if(TimeStop&&GameManager.distance >10 )
        {
            TimeStop = false;
        }
    }
}