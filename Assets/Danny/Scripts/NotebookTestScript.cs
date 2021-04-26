using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NotebookTestScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI characterText;
    [SerializeField] TextMeshProUGUI roomText;
    [SerializeField] TextMeshProUGUI weaponText;
    PlayerMasterController currentPlayer;



    // Update is called once per frame
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

    private void SetOutputStrings()
    {
        currentPlayer = FindObjectOfType<RoundManager>().GetCurrentPlayer();
        characterText.text = GetCharacterString();
        roomText.text = GetRoomString();
        weaponText.text = GetWeaponText();
    }


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
    private string GetWeaponText()
    {
        string output = "Weapons\n\n";
        foreach (WeaponEnum weapon in System.Enum.GetValues(typeof(WeaponEnum)))
        {
            output += string.Format("{0} = {1}\n", EnumToString.GetStringFromEnum(weapon), currentPlayer.GetNotebookValue(weapon));
        }
        return output;
    }

    private string GetRoomString()
    {
        string output = "Rooms\n\n";
        foreach (Room room in System.Enum.GetValues(typeof(Room)))
        {
            if(room != Room.Centre && room != Room.None)
            {
                output += string.Format("{0} = {1}\n", EnumToString.GetStringFromEnum(room), currentPlayer.GetNotebookValue(room));
            }
        }
        return output;
    }
}
