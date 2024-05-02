using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject restartButton;
    public GameObject stopButton;
    public GameObject ammoPrefab;
    public Transform slingshotPosition;

    public void StartGame()
    {
        // hides start button and shows other UI elements
       if(startButton) startButton.SetActive(false);
       if(restartButton) restartButton.SetActive(true);
       if(stopButton) stopButton.SetActive(true);
       
       if (ammoPrefab != null && slingshotPosition != null)
       {
        GameObject ammoInstance = Instantiate(ammoPrefab, slingshotPosition.position, slingshotPosition.rotation);
        SlingshotAmmo slingshotAmmoScript = ammoInstance.GetComponent<SlingshotAmmo>();
        if (slingshotAmmoScript !=null)
        {
            slingshotAmmoScript.slingshotPosition = slingshotPosition;
        }
       }
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
