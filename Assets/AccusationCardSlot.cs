using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccusationCardSlot : MonoBehaviour
{
    public Card card;
    public Image img;
    public TextMeshProUGUI txt;
    public bool isVisible = false;
    private Sprite initialImage;
    private string initialText;

    private void Start()
    {
        initialImage = img.sprite;
        initialText = txt.text;
    }

    public void ResetSlot()
    {
        img.sprite = initialImage;
        txt.text = initialText;
        card = null;
    }

    public void SetCard(Card c)
    {
        card = c;
        SetCardDetails();
    }

    private void SetCardDetails()
    {
        if (!card)
        {
            return;
        }
        img.sprite = card.GetCardImage();
        txt.text = EnumToString.GetStringFromEnum(card.GetCardType());
    }

    public void SetVisible(bool isVisible = true)
    {
        this.isVisible = isVisible;
        gameObject.SetActive(isVisible);
    }


}
