using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameSetUpScript : MonoBehaviour
{
    [SerializeField] Toggle[] toggles;
    [SerializeField] bool[] toggleResults;
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
        foreach(PlayerMasterController p in allPlayers)
        {
            p.isAI = toggleResults[(int)p.GetCharacter()];
        }
    }
}
