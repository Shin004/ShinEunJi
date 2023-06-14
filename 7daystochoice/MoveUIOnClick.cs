using UnityEngine;

public class MoveUIOnClick : MonoBehaviour
{
    private RectTransform uiRectTransform; // UI 오브젝트의 RectTransform 컴포넌트

    private void Start()
    {
        uiRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // 마우스 클릭 시 클릭된 위치를 월드 좌표로 변환합니다.
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // UI 오브젝트의 위치를 클릭된 월드 좌표로 이동시킵니다.
            uiRectTransform.position = worldPosition;
        }
    }
}