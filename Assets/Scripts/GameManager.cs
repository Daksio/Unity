using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartScene()
    {
        // Загружаем текущую сцену заново
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}