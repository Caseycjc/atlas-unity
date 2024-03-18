using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartGame()
    {
        // starts the game
    }

    public void RestartGame()
    {
        // Resets the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Quits the game
        Application.Quit();
    }
}
