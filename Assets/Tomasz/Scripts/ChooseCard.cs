using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCard : MonoBehaviour
{
    List<Card> cardsToShow;
    public void go(List<Card> cardList)
    {
        cardsToShow = cardList;
        List<GameObject> test = new List<GameObject>(GameObject.FindGameObjectsWithTag("slot"));
        foreach (Card c in cardsToShow) {
            
        }
    }


}
