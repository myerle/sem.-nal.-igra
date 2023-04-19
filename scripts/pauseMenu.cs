using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour
{
    
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public Button PauseButton;

    private void Start()
    {
        PauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    private void OnPauseButtonClick()
    {
        if (GameIsPaused)
        {
            resume();
        }
        else
        {
            pause();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
