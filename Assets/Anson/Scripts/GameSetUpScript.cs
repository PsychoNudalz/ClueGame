using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// for passing the selection of which player is human or AI from the main menu
/// </summary>
public class GameSetUpScript : MonoBehaviour
{
    [SerializeField] Toggle[] toggles;
    [SerializeField] bool[] toggleResults;

    public bool[] ToggleResults { get => toggleResults; set => toggleResults = value; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// passes the selection from the Toggle buttons
    /// </summary>
    public void SaveChoices()
    {
        toggleResults = new bool[6];
        int i = 0;
        foreach(Toggle t in toggles)
        {
            toggleResults[i] = t.isOn;
            i++;
        }
    }

    /// <summary>
    /// assigns the players in the game based on the saved AI selection 
    /// </summary>
    public void StartGame()
    {
        PlayerMasterController[] allPlayers = FindObjectsOfType<PlayerMasterController>();
        if (toggleResults.Length == 0)
        {
            Destroy(gameObject);
            return;
        }

        foreach (PlayerMasterController p in allPlayers)
        {
            p.isAI = toggleResults[(int)p.GetCharacter()];
        }
        Destroy(gameObject);
    }
}
