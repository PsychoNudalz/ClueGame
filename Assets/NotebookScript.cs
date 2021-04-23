using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookScript : MonoBehaviour
{
    [SerializeField] private GameObject characterPanel;
    [SerializeField] private GameObject RoomPanel;
    [SerializeField] private RoundManager roundManager;
    NotebookButton[] characterButtons;
    NotebookButton[] roomButtons;

    private void OnEnable()
    {
        RefreshNotebook();
    }

    private void Awake()
    {
        initializeNotebook();
    }

    public void initializeNotebook()
    {
        CreateCharacterButtons();
        CreateRoomButtons();
    }

    public void RefreshNotebook()
    {
        PlayerMasterController currentPlayer = roundManager.GetCurrentPlayer();
        RefreshCharacters(currentPlayer);
        RefreshRooms(currentPlayer);
    }

    private void CreateCharacterButtons()
    {
        CharacterEnum[] characterEnums = (CharacterEnum[])Enum.GetValues(typeof(CharacterEnum));
        NotebookButton[] buttons = characterPanel.GetComponentsInChildren<NotebookButton>();
        for (int i = 0; i < characterEnums.Length -1; i++)
        {
            buttons[i].SetButtonType(characterEnums[i]);
        }
        characterButtons = buttons;
    }

    private void CreateRoomButtons()
    {
        Room[] roomEnums = (Room[])Enum.GetValues(typeof(Room));
        NotebookButton[] buttons = RoomPanel.GetComponentsInChildren<NotebookButton>();
        for (int i = 0; i < roomEnums.Length-2; i++)
        {
            buttons[i].SetButtonType(roomEnums[i]);
        }
        roomButtons = buttons;
    }

    internal void ToggleButton(NotebookButton notebookButton)
    {
        PlayerMasterController currentPlayer = roundManager.GetCurrentPlayer();
        currentPlayer.SetNotebookValue(notebookButton.ButtonType,!currentPlayer.GetNotebookValue(notebookButton.ButtonType));
        RefreshNotebook();
    }

    public void RefreshCharacters(PlayerMasterController currentPlayer)
    {
        if(currentPlayer != null)
        {
            foreach (NotebookButton button in characterButtons)
            {
                button.setCrossedOut(currentPlayer.GetNotebookValue(button.ButtonType));
            }

        }
        else
        {
            print("currentPlayer is null");
        }
    }

    public void RefreshRooms(PlayerMasterController currentPlayer)
    {
        
        if (currentPlayer != null)
        {
            foreach (NotebookButton button in roomButtons)
            {
                button.setCrossedOut(currentPlayer.GetNotebookValue(button.ButtonType));
            }

        }
        else
        {
            print("currentPlayer is null");
        }
    }
}
