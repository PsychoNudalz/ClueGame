using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookScript : MonoBehaviour
{
    [SerializeField] private GameObject characterPanel;
    [SerializeField] private RoundManager roundManager;

    private void OnEnable()
    {
        RefreshNotebook();
    }

    private void Awake()
    {
        initializeNotebook();
    }

    private void initializeNotebook()
    {
        CreateCharacterButtons();
    }

    public void RefreshNotebook()
    {
        RefreshCharacters();
    }

    private void CreateCharacterButtons()
    {
        CharacterEnum[] characterEnums = (CharacterEnum[])Enum.GetValues(typeof(CharacterEnum));
        NotebookButton[] buttons = characterPanel.GetComponentsInChildren<NotebookButton>();
        for (int i = 0; i < characterEnums.Length -1; i++)
        {
            buttons[i].SetButtonType(characterEnums[i]);
        }
    }

    internal void ToggleButton(NotebookButton notebookButton)
    {
        PlayerMasterController currentPlayer = roundManager.GetCurrentPlayer();
        currentPlayer.SetNotebookValue(notebookButton.ButtonType,!currentPlayer.GetNotebookValue(notebookButton.ButtonType));
        RefreshNotebook();
    }

    public void RefreshCharacters()
    {
        PlayerMasterController currentPlayer = roundManager.GetCurrentPlayer();
        if(currentPlayer != null)
        {
            foreach(NotebookButton button in characterPanel.GetComponentsInChildren<NotebookButton>())
            {
                print(button.ButtonType);
                button.setCrossedOut(currentPlayer.GetNotebookValue(button.ButtonType));
            }

        }
    }
}
