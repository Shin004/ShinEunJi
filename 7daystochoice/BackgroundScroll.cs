using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 1f;
    public Image backgroundImage;
    public int maxInstances = 3;

    private RectTransform rectTransform;
    private float backgroundWidth;
    private RectTransform[] backgroundInstances;
    private int numInstances;
    private RectTransform currentObjectTransform;
    private float cameraWidth;
    private BoxCollider2D currentObjectCollider;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        backgroundWidth = rectTransform.rect.width;

        float cameraHeight = Camera.main.orthographicSize * 2f;
        cameraWidth = cameraHeight * Camera.main.aspect;

        numInstances = Mathf.Clamp(Mathf.CeilToInt(cameraWidth / backgroundWidth) + 1, 1, maxInstances);

        backgroundInstances = new RectTransform[numInstances];

        currentObjectTransform = CreateBackgroundInstance(-1);
        currentObjectCollider = currentObjectTransform.gameObject.AddComponent<BoxCollider2D>();

        for (int i = 0; i < numInstances; i++)
        {
            backgroundInstances[i] = CreateBackgroundInstance(i);
        }
    }

    private RectTransform CreateBackgroundInstance(int instanceIndex)
    {
        GameObject backgroundObject = new GameObject("BackgroundInstance");
        backgroundObject.transform.SetParent(transform.parent, false);

        RectTransform backgroundRectTransform = backgroundObject.AddComponent<RectTransform>();
        backgroundRectTransform.anchorMin = new Vector2(0f, 0.5f);
        backgroundRectTransform.anchorMax = new Vector2(0f, 0.5f);
        backgroundRectTransform.pivot = new Vector2(0f, 0.5f);
        backgroundRectTransform.sizeDelta = rectTransform.sizeDelta;

        Image image = backgroundObject.AddComponent<Image>();
        image.sprite = backgroundImage.sprite;

        // BoxCollider2D 컴포넌트 추가
        BoxCollider2D boxCollider = backgroundObject.AddComponent<BoxCollider2D>();

        float newX = rectTransform.anchoredPosition.x + backgroundWidth * instanceIndex;
        backgroundRectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);

        return backgroundRectTransform;
    }

    private void Update()
    {
        currentObjectTransform.anchoredPosition += Vector2.right * scrollSpeed * Time.deltaTime;

        for (int i = 0; i < numInstances; i++)
        {
            RectTransform backgroundInstance = backgroundInstances[i];
            backgroundInstance.anchoredPosition += Vector2.right * scrollSpeed * Time.deltaTime;

            if (backgroundInstance.anchoredPosition.x >= cameraWidth + backgroundWidth)
            {
                float prevX = GetPreviousBackgroundX(i);
                backgroundInstance.anchoredPosition = new Vector2(prevX - backgroundWidth, rectTransform.anchoredPosition.y);
            }
        }

        if (currentObjectTransform.anchoredPosition.x >= cameraWidth + backgroundWidth)
        {
            float prevX = GetPreviousBackgroundX(numInstances - 1);
            currentObjectTransform.anchoredPosition = new Vector2(prevX - backgroundWidth, rectTransform.anchoredPosition.y);
        }

        if (currentObjectTransform.anchoredPosition.x <= -backgroundWidth)
        {
            float nextX = GetNextBackgroundX(0);
            currentObjectTransform.anchoredPosition = new Vector2(nextX + backgroundWidth, rectTransform.anchoredPosition.y);
        }

        CheckImageCollisions();
    }

    private float GetPreviousBackgroundX(int currentIndex)
    {
        int prevIndex = (currentIndex - 1 + numInstances) % numInstances;
        return backgroundInstances[prevIndex].anchoredPosition.x;
    }

    private float GetNextBackgroundX(int currentIndex)
    {
        int nextIndex = (currentIndex + 1) % numInstances;
        return backgroundInstances[nextIndex].anchoredPosition.x;
    }

    private void CheckImageCollisions()
    {
        for (int i = 0; i < numInstances; i++)
        {
            RectTransform backgroundInstance = backgroundInstances[i];

            if (currentObjectCollider.bounds.Intersects(backgroundInstance.GetComponent<BoxCollider2D>().bounds))
            {
                Debug.Log("Collision detected between current object and background instance!");
            }
        }
    }
}
