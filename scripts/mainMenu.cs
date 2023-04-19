using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void PlayGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

