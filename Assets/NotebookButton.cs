using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class NotebookButton : MonoBehaviour
{
    [SerializeField] Enum buttonType;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] GameObject crossedOutImage;
    [SerializeField] NotebookScript notebookScript;

    public Enum ButtonType { get => buttonType;}

    public void SetButtonType(Enum buttonType)
    {
        this.buttonType = buttonType;
        buttonText.text = EnumToString.GetStringFromEnum(buttonType);
    }

    public void setCrossedOut(bool isCrossedOut = true)
    {
        crossedOutImage.SetActive(isCrossedOut);
    }
    
    public void ClickButton()
    {
        notebookScript.ToggleButton(this);
    }
}
