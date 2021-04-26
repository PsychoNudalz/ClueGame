using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
/// <summary>
/// This class handles the suggestion test scene, and handles the UI in the test scene
/// </summary>
public class SuggestionTest : TestUIScript
{

    public GameManagerScript gameManager;

    [Header("Player Hand Text")]
    public TextMeshProUGUI Player1;
    public TextMeshProUGUI Player2;
    public TextMeshProUGUI Player3;
    public TextMeshProUGUI Player4;
    public TextMeshProUGUI Player5;
    public TextMeshProUGUI Player6;
    public TextMeshProUGUI playerFound;
    public TextMeshProUGUI currentPLayer;
    public TextMeshProUGUI answers;

    [Header("Option Dropdown")]
    public TMP_Dropdown roomDD;
    public TMP_Dropdown weaponDD;
    public TMP_Dropdown characterDD;



    private void Awake()
    {
        if (!gameManager) {
            gameManager = FindObjectOfType<GameManagerScript>();
        }
        AssignAllComponents();
        PopulateList();
    }

    private void Start()
    {
        gameManager.StartGame();
        GetPlayerCardsForUI();
        AnswersText();
    }

    /// <summary>
    /// Set text to display answers on test scene
    /// </summary>
    void AnswersText() {
        string temp = "";
        foreach (Card c in cardManager.answers) {
            temp += c.ToString() + "\n";
        }
        answers.SetText("Answers: "+temp);
    }


    /// <summary>
    /// Add all enums to the dropdown lists in the test UI
    /// </summary>
    void PopulateList() 
    {
        string[] roomNames = System.Enum.GetNames(typeof(RoomEnum));
        List <string> room = new List<string>(roomNames);
        roomDD.AddOptions(room);

        string[] weaponNames = System.Enum.GetNames(typeof(WeaponEnum));
        List<string> weapon = new List<string>(weaponNames);
        weaponDD.AddOptions(weapon);

        string[] characterNames = System.Enum.GetNames(typeof(CharacterEnum));
        List<string> character = new List<string>(characterNames);
        characterDD.AddOptions(character);
    }

    /// <summary>
    /// get test choices and make suggestion
    /// </summary>
    public void MakeSuggestion()
    {
        
        userController.SetRoom((RoomEnum)roomDD.value);
        userController.SetWeapon((WeaponEnum)weaponDD.value);
        userController.SetCharacter((CharacterEnum)characterDD.value);

        RoomCard roomCard = cardManager.FindCard((RoomEnum)roomDD.value) as RoomCard;
        WeaponCard weaponCard = cardManager.FindCard((WeaponEnum)weaponDD.value) as WeaponCard;
        CharacterCard characterCard = cardManager.FindCard((CharacterEnum)characterDD.value) as CharacterCard;
        Card[] sugTest = { roomCard, weaponCard, characterCard };

        userController.MakeSuggestion();
        try
        {
            Tuple<PlayerMasterController, List<Card>> foundPlayers = roundManager.MakeSuggestion(new List<Card>(sugTest));


            string temp = foundPlayers.Item1.ToString();

            foreach (Card c in foundPlayers.Item2)
            {
                temp += c.ToString() + ",";
            }
            playerFound.SetText(temp);
            Debug.Log(((RoomEnum)roomDD.value).ToString() + ", " + ((WeaponEnum)weaponDD.value).ToString() + ", " + ((CharacterEnum)characterDD.value).ToString());
        }
        catch (System.NullReferenceException e) {
            playerFound.SetText("No players with suggested card found");
        }
        
    }


    /// <summary>
    /// set the text for every players' hand in the test UI
    /// </summary>
    void GetPlayerCardsForUI() 
    {
        string msScarletHandString = "";
        string profPlumHandString = "";
        string colMustardHandString = "";
        string mrsPeackcockHandString = "";
        string revGreenHandString = "";
        string msWhiteHandString = "";
        List<Card> msScarletHand = new List<Card>(turnController.CurrentPlayers[0].GetDeck());
        List<Card> profPlumHand = new List<Card>(turnController.CurrentPlayers[1].GetDeck());
        List<Card> colMustard = new List<Card>(turnController.CurrentPlayers[2].GetDeck());
        List<Card> mrsPeackcockHand = new List<Card>(turnController.CurrentPlayers[3].GetDeck());
        List<Card> revGreenHand = new List<Card>(turnController.CurrentPlayers[4].GetDeck());
        List<Card> msWhiteHand = new List<Card>(turnController.CurrentPlayers[5].GetDeck());

        foreach (Card c in msScarletHand) 
        {
            msScarletHandString += EnumToString.GetStringFromEnum(c.GetCardType())+"\n";
        }
        Debug.Log(msScarletHandString);
        Player1.SetText( "Ms Scarlet\n" + msScarletHandString);

        foreach (Card c in profPlumHand)
        {
            profPlumHandString += EnumToString.GetStringFromEnum(c.GetCardType()) + "\n";
        }
        Debug.Log(profPlumHandString);
        Player2.SetText("Prof Plum\n" + profPlumHandString);

        foreach (Card c in colMustard)
        {
            colMustardHandString += EnumToString.GetStringFromEnum(c.GetCardType()) + "\n";
        }
        Debug.Log(colMustardHandString);
        Player3.SetText("Col Mustard\n" + colMustardHandString);

        foreach (Card c in mrsPeackcockHand)
        {
            mrsPeackcockHandString += EnumToString.GetStringFromEnum(c.GetCardType()) + "\n";
        }
        Debug.Log(mrsPeackcockHandString);
        Player4.SetText("Mrs Peacock\n" + mrsPeackcockHandString);

        foreach (Card c in revGreenHand)
        {
            revGreenHandString += EnumToString.GetStringFromEnum(c.GetCardType()) + "\n";
        }
        Debug.Log(revGreenHandString);
        Player5.SetText("Rev Green\n" + revGreenHandString);

        foreach (Card c in msWhiteHand)
        {
            msWhiteHandString += EnumToString.GetStringFromEnum(c.GetCardType()) + "\n";
        }
        Debug.Log(msWhiteHandString);
        Player6.SetText("Ms White\n" + msWhiteHandString);


    }

    private void FixedUpdate()
    {
        currentPLayer.SetText("Current Player is: "+turnController.GetCurrentPlayer().ToString());
    }


}
