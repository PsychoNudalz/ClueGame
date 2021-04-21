using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuggestionTest : TestUIScript
{
    [SerializeField]public TMP_Dropdown roomDD;
    [SerializeField]public TMP_Dropdown weaponDD;
    [SerializeField]public TMP_Dropdown characterDD;


    private void Awake()
    {
        AssignAllComponents();
    }

    private void Start()
    {   
        PopulateList();
    }

    void PopulateList() {
        string[] roomNames = System.Enum.GetNames(typeof(Room));
        List <string> room = new List<string>(roomNames);
        roomDD.AddOptions(room);

        string[] weaponNames = System.Enum.GetNames(typeof(WeaponEnum));
        List<string> weapon = new List<string>(weaponNames);
        weaponDD.AddOptions(weapon);

        string[] characterNames = System.Enum.GetNames(typeof(CharacterEnum));
        List<string> character = new List<string>(characterNames);
        characterDD.AddOptions(character);

    }

    public void MakeSuggestion() {
        userController.SetRoom((Room)roomDD.value);
        userController.SetWeapon((WeaponEnum)weaponDD.value);
        userController.SetCharacter((CharacterEnum)characterDD.value);
        userController.MakeSuggestion();
        Debug.Log(((Room)roomDD.value).ToString()+", " + ((WeaponEnum)weaponDD.value).ToString() + ", " + ((CharacterEnum)characterDD.value).ToString());
    }



}
