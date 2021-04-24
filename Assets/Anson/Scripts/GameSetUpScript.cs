using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameSetUpScript : MonoBehaviour
{
    [SerializeField] Toggle[] toggles;
    [SerializeField] bool[] toggleResults;

    public bool[] ToggleResults { get => toggleResults; set => toggleResults = value; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

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
