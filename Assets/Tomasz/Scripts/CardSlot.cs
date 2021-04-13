using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
    public Card card;
    public Image img;
    public TextMeshProUGUI txt;
    public bool isVisible = false;
    private void Start()
    {
        img = GetComponent<Image>();
        txt = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetCard(Card c)
    {
        card = c;
    }
    public void SetVisible()
    {
        isVisible = true;

    }


}
