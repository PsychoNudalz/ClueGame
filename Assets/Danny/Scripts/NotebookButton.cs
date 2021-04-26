using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
/// <summary>
/// Script for the notebook button Gameobject
/// </summary>
public class NotebookButton : MonoBehaviour
{
    [SerializeField] private Enum buttonType;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject crossedOutImage;
    [SerializeField] private NotebookScript notebookScript;

    /// <summary>
    /// Getter for the button type
    /// </summary>
    public Enum ButtonType { get => buttonType;}

    /// <summary>
    /// Set the button type
    /// </summary>
    /// <param name="buttonType">Ennum representing a character, room or weapon</param>
    public void SetButtonType(Enum buttonType)
    {
        this.buttonType = buttonType;
        buttonText.text = EnumToString.GetStringFromEnum(buttonType);
    }
    
    /// <summary>
    /// Set button graphic to crossed through
    /// </summary>
    /// <param name="isCrossedOut">true if crossed, false if not</param>
    public void setCrossedOut(bool isCrossedOut = true)
    {
        crossedOutImage.SetActive(isCrossedOut);
    }
    
    /// <summary>
    /// Method for clicking the button to toggle
    /// </summary>
    public void ClickButton()
    {
        notebookScript.ToggleButton(this);
    }
}
