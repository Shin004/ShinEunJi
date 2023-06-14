using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌 체크
        {
            ReloadScene(); // 씬 리로드
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}