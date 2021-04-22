using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScreenScript : MonoBehaviour
{
    [SerializeField] CardSlot murdererCard;
    [SerializeField] CardSlot roomCard;
    [SerializeField] CardSlot weaponCard;
    [SerializeField] TextMeshProUGUI winText;
    private CardManager cardManager;
    private RoundManager roundManager;

    private void Awake()
    {
        cardManager = FindObjectOfType<CardManager>();
        roundManager = FindObjectOfType<RoundManager>();
    }
    private void OnEnable()
    {
        List<Card> answers = cardManager.answers;
        string player = EnumToString.GetStringFromEnum(roundManager.GetCurrentPlayer().GetCharacter());
        string murderer = EnumToString.GetStringFromEnum(answers[0].GetCardType());
        string weapon = EnumToString.GetStringFromEnum(answers[1].GetCardType());
        string room = EnumToString.GetStringFromEnum(answers[2].GetCardType());
        winText.text = string.Format("{0} wins!\n\n" +
                                     "The perpetrator was\n" +
                                     "{1} with the\n" +
                                     "{2} in the {3}", player, murderer, weapon, room);
        murdererCard.SetCard(answers[0]);
        weaponCard.SetCard(answers[1]);
        roomCard.SetCard(answers[2]);
    }
}
