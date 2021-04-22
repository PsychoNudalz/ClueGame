using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameSetUpScript gameSetUp;
    [Header("UI Components")]
    [SerializeField] GameObject gameSetUpGO;

    private void Awake()
    {
        gameSetUpGO.SetActive(false);
    }

    public void QuitGame()
    {
        print("Quit game");
        Application.Quit();
    }

    public void PlayGame()
    {
        gameSetUpGO.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        gameSetUp.SaveChoices();
    }

}
