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

    public void StartAIDemo()
    {
        gameSetUp.ToggleResults = new bool[6]{ true,true,true,true,true,true};
        SceneManager.LoadScene("AI Scene");
    }


    public void LoadTestSence(string s)
    {
        Destroy(gameSetUpGO);
        SceneManager.LoadScene(s);
    }

}
