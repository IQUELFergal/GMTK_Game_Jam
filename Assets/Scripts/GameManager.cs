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

    public GameObject player;
    public GameObject start;

    bool canReset = true;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        endgameMenu.SetActive(false);
        endgameInteractable = GameObject.FindGameObjectWithTag("EndGameActivator").GetComponent<EndGameInteractable>();
        endgameInteractable.endGameEvent.AddListener(EndGame);
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

    public void FreezeGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void UnfreezeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        FreezeGame();
    }

    public void EndGame()
    {
        endgameMenu.SetActive(true);
        FreezeGame();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        UnfreezeGame();
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

    // reset position
    public void ResetPlayerPosition()
    {
        if (canReset)
        {
            player.SetActive(false);
            player.transform.position = start.transform.position;
            player.SetActive(true);
        }            
    }

    public void ContinuousResetPlayerPosition()
    {
        if (canReset)
        {
            ResetPlayerPosition();
            StartCoroutine(CancelResetPlayerPositio(3.0f));
        }
    }

    IEnumerator CancelResetPlayerPositio(float duration)
    {
        canReset = false;
        yield return new WaitForSeconds(duration);
        canReset = true;
    }
}
