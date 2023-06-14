using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �÷��̾�� �浹 üũ
        {
            ReloadScene(); // �� ���ε�
        }
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}