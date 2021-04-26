using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// for handling the main menu
/// </summary>
public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameSetUpScript gameSetUp;
    [Header("UI Components")]
    [SerializeField] GameObject gameSetUpGO;

    private void Awake()
    {
        gameSetUpGO.SetActive(false);
    }
    /// <summary>
    /// for quitting the game
    /// </summary>
    public void QuitGame()
    {
        print("Quit game");
        Application.Quit();
    }
    /// <summary>
    /// for showing the menu for the game set up
    /// </summary>
    public void PlayGame()
    {
        gameSetUpGO.SetActive(true);
    }

    /// <summary>
    /// to start the game and load the game scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        gameSetUp.SaveChoices();
    }
    /// <summary>
    /// to start the AI demo scene
    /// </summary>
    public void StartAIDemo()
    {
        gameSetUp.ToggleResults = new bool[6]{ true,true,true,true,true,true};
        SceneManager.LoadScene("AI Scene");
    }

    /// <summary>
    /// to load a certain test scene
    /// destroies the game setup game object
    /// </summary>
    /// <param name="s">name of the scene to be loaded</param>
    public void LoadTestSence(string s)
    {
        Destroy(gameSetUp.gameObject);
        SceneManager.LoadScene(s);
    }

}
