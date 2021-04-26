using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
/// <summary>
/// Script for the Notebook test scene
/// </summary>
public class NotebookTestScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterText;
    [SerializeField] private TextMeshProUGUI roomText;
    [SerializeField] private TextMeshProUGUI weaponText;
    private PlayerMasterController currentPlayer;
    
    /// <summary>
    /// If current player is set then update output texts
    /// </summary>
    void Update()
    {
        if(currentPlayer == null)
        {
            try
            {
                currentPlayer = FindObjectOfType<RoundManager>().GetCurrentPlayer();
            }
            catch
            {

            }
        }
        if(currentPlayer != null)
        {
            SetOutputStrings();
        }
    }

    /// <summary>
    /// Update player reference then update all text strings
    /// </summary>
    private void SetOutputStrings()
    {
        currentPlayer = FindObjectOfType<RoundManager>().GetCurrentPlayer();
        characterText.text = GetCharacterString();
        roomText.text = GetRoomString();
        weaponText.text = GetWeaponText();
    }

    /// <summary>
    /// Get character notebook text
    /// </summary>
    /// <returns>Character statuses</returns>
    private string GetCharacterString()
    {
        string output = "Characters\n\n";
        foreach (CharacterEnum character in System.Enum.GetValues(typeof(CharacterEnum)))
        {
            if(character != CharacterEnum.Initial)
            {
                output += string.Format("{0} = {1}\n", EnumToString.GetStringFromEnum(character), currentPlayer.GetNotebookValue(character));
            }
        }
        return output;
    }

    /// <summary>
    /// Get weapon notebook text
    /// </summary>
    /// <returns>Weapon statuses</returns>
    private string GetWeaponText()
    {
        string output = "Weapons\n\n";
        foreach (WeaponEnum weapon in System.Enum.GetValues(typeof(WeaponEnum)))
        {
            output += string.Format("{0} = {1}\n", EnumToString.GetStringFromEnum(weapon), currentPlayer.GetNotebookValue(weapon));
        }
        return output;
    }

    /// <summary>
    /// Get Room notebook text
    /// </summary>
    /// <returns>Room statuses</returns>
    private string GetRoomString()
    {
        string output = "Rooms\n\n";
        foreach (RoomEnum room in System.Enum.GetValues(typeof(RoomEnum)))
        {
            if(room != RoomEnum.Centre && room != RoomEnum.None)
            {
                output += string.Format("{0} = {1}\n", EnumToString.GetStringFromEnum(room), currentPlayer.GetNotebookValue(room));
            }
        }
        return output;
    }
}
