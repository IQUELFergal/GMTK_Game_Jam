using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject endgameMenu;

    bool isPaused = false;

    public EndGameInteractable endgameInteractable;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        endgameMenu.SetActive(false);
        endgameInteractable = GameObject.FindGameObjectWithTag("EndGameActivator").GetComponent<EndGameInteractable>();
        endgameInteractable.endGameEvent.AddListener(PauseGame);
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void EndGame()
    {
        endgameMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        //Add a popup to ask if you are sure to quit
        Application.Quit();
    }


}
