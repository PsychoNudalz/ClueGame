using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script for the notebook UI object
/// </summary>
public class NotebookScript : MonoBehaviour
{
    [SerializeField] private GameObject characterPanel;
    [SerializeField] private GameObject RoomPanel;
    [SerializeField] private GameObject WeaponPanel;
    [SerializeField] private RoundManager roundManager;
    private NotebookButton[] characterButtons;
    private NotebookButton[] roomButtons;
    private NotebookButton[] weaponButtons;

    /// <summary>
    /// Refresh notebook on enabling
    /// </summary>
    private void OnEnable()
    {
        RefreshNotebook();
    }

    /// <summary>
    /// Initialize notebook on awake
    /// </summary>
    private void Awake()
    {
        initializeNotebook();
    }

    /// <summary>
    /// Initialize notebook UI
    /// </summary>
    public void initializeNotebook()
    {
        CreateCharacterButtons();
        CreateRoomButtons();
        CreateWeaponButtons();
    }

    /// <summary>
    /// Refresh the notebook UI Object
    /// </summary>
    public void RefreshNotebook()
    {
        PlayerMasterController currentPlayer = roundManager.GetCurrentPlayer();
        RefreshCharacters(currentPlayer);
        RefreshRooms(currentPlayer);
        RefreshWeapons(currentPlayer);
    }
    /// <summary>
    /// Create the character buttons
    /// </summary>
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
    /// <summary>
    /// Create the room buttons
    /// </summary>
    private void CreateRoomButtons()
    {
        RoomEnum[] roomEnums = (RoomEnum[])Enum.GetValues(typeof(RoomEnum));
        NotebookButton[] buttons = RoomPanel.GetComponentsInChildren<NotebookButton>();
        for (int i = 0; i < roomEnums.Length-2; i++)
        {
            buttons[i].SetButtonType(roomEnums[i]);
        }
        roomButtons = buttons;
    }
    /// <summary>
    /// Create the Weapon buttons
    /// </summary>
    private void CreateWeaponButtons()
    {
        WeaponEnum[] weaponEnums = (WeaponEnum[])Enum.GetValues(typeof(WeaponEnum));
        NotebookButton[] buttons = WeaponPanel.GetComponentsInChildren<NotebookButton>();
        for (int i = 0; i < weaponEnums.Length; i++)
        {
            buttons[i].SetButtonType(weaponEnums[i]);
        }
        weaponButtons = buttons;
    }
    /// <summary>
    /// Toggle a notebook button
    /// </summary>
    /// <param name="notebookButton">Button to toggle</param>
    internal void ToggleButton(NotebookButton notebookButton)
    {
        PlayerMasterController currentPlayer = roundManager.GetCurrentPlayer();
        currentPlayer.SetNotebookValue(notebookButton.ButtonType,!currentPlayer.GetNotebookValue(notebookButton.ButtonType));
        RefreshNotebook();
    }
    /// <summary>
    /// Refresh all character button status.
    /// </summary>
    /// <param name="currentPlayer">Current Player</param>
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
    /// <summary>
    /// Refresh all room button status.
    /// </summary>
    /// <param name="currentPlayer">Current Player</param>
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

    /// <summary>
    /// Refresh all weapon button status.
    /// </summary>
    /// <param name="currentPlayer">Current Player</param>
    public void RefreshWeapons(PlayerMasterController currentPlayer)
    {

        if (currentPlayer != null)
        {
            foreach (NotebookButton button in weaponButtons)
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
