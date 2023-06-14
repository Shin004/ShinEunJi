using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowGameManager : MonoBehaviour
{

    public GameObject ArrowGame;
    public GameObject HeartGame;

    public int[] spriteCount; // 화면에 나올 스프라이트 개수
    public Sprite[] arrowSprites; // 방향키 스프라이트 배열
    public Transform spawnPoint; // 스프라이트 생성 위치
    public float spriteSpacing = 1f; // 스프라이트 간격
    public float spriteLifetime = 2f; // 스프라이트 유지 시간
    public Vector3 spriteScale = Vector3.one; // 스프라이트 크기

    public Text instructionText; // UI 텍스트 컴포넌트

    private List<int> spriteSequence; // 스프라이트 순서를 기억하는 리스트
    private int currentSpriteIndex; // 현재 체크해야 할 스프라이트 인덱스
    private bool isCheckingInput; // 입력을 체크 중인지 여부

    private int count = 0;
    private bool hasSpawnedSprites; // 스프라이트를 생성했는지 여부

    public Image gaugeImage; // 게이지를 표시할 이미지
    private float currentFill = 0f; // 현재 게이지의 채움 정도

    private void Start()
    {
        spriteSequence = new List<int>();


        // 게이지의 초기 값을 필렛 값으로 설정합니다.
        currentFill = 1;

        // 게이지 이미지에 초기 값을 반영합니다.
        gaugeImage.fillAmount = currentFill;
    }

    private void Update()
    {
        if (GameManager.isCheakLound && !hasSpawnedSprites)
        {
            SpawnSprites(spriteCount[count]);
            hasSpawnedSprites = true;
        }

        if (isCheckingInput)
        {
            CheckPlayerInput();
        }

        currentFill -= Time.deltaTime / spriteLifetime;

        if (currentFill <= 0)
        {
            currentFill = 0;
            // 추가로직: 게이지가 완전히 줄어들었을 때의 처리
            // 여기에 필요한 로직을 추가하세요.
        }

        gaugeImage.fillAmount = currentFill;
    }

    private void SpawnSprites(int count)
    {
        float totalWidth = (count - 1) * spriteSpacing;
        float startX = spawnPoint.localPosition.x - totalWidth / 2f;

        for (int i = 0; i < count; i++)
        {
            float xPos = startX + i * spriteSpacing;
            float yPos = spawnPoint.localPosition.y;

            int randomIndex = Random.Range(0, arrowSprites.Length);
            Sprite randomSprite = arrowSprites[randomIndex];

            GameObject spriteObject = new GameObject("ArrowSprite", typeof(Image));
            spriteObject.transform.SetParent(spawnPoint, false);
            spriteObject.transform.localPosition = new Vector3(xPos, yPos, 0f);
            spriteObject.transform.localScale = spriteScale;

            Image spriteRenderer = spriteObject.GetComponent<Image>();
            spriteRenderer.sprite = randomSprite;

            // 스프라이트의 인덱스를 spriteSequence 리스트에 추가
            spriteSequence.Add(randomIndex);
        }

        StartCoroutine(DestroySprites(spriteLifetime));
        

    }

    private void CheckPlayerInput()
    {
        if (currentSpriteIndex >= spriteSequence.Count)
            return; // 모든 스프라이트를 체크했으면 종료

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Left");
            CheckInputMatch(0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Up");
            CheckInputMatch(1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Down");
            CheckInputMatch(2);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Right");
            CheckInputMatch(3);
        }
    }

    private void CheckInputMatch(int expectedInput)
    {
        int spriteIndex = spriteSequence[currentSpriteIndex];

        if (expectedInput == spriteIndex)
        {
            currentSpriteIndex++;

            if (currentSpriteIndex >= spriteSequence.Count)
            {
                Debug.Log("성공");
                isCheckingInput = false;

                ArrowGame.SetActive(false);
                HeartGame.SetActive(true);
                GameManager.distance = 70;

                // 초기화
                currentFill = 1;
                currentSpriteIndex = 0;
                spriteSequence.Clear();
                count++;
                hasSpawnedSprites = false;

                // 모든 스프라이트 클리어 시
                if (count >= spriteCount.Length)
                {
                    instructionText.text = "모든 스프라이트 클리어!";
                    return;
                }
            }
        }
        else
        {
            Debug.Log("실패");
            isCheckingInput = false;

            // 초기화
            currentSpriteIndex = 0;
            spriteSequence.Clear();
            hasSpawnedSprites = false;
        }
    }

    private IEnumerator DestroySprites(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
        }

        isCheckingInput = true;
        currentSpriteIndex = 0;

        instructionText.text = "순서대로 알맞게 입력해주세요";
    }
}
