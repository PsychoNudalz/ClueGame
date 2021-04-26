using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script for Dice test script
/// </summary>
public class DisplayDiceValue : MonoBehaviour
{
    
    [SerializeField] private Button rollButton;
    [SerializeField] private Button resetButton;
    private Text diceDisplay;
    private Dice dice;
    private int dieValue;
    /// <summary>
    /// Set required variables
    /// </summary>
    private void Start()
    {
        diceDisplay = GetComponent<Text>();
        dice = FindObjectOfType<Dice>();
    }

    /// <summary>
    /// Set button interactable and update dice value text 
    /// </summary>
    void Update()
    {
        rollButton.interactable = dice.ReadyToRoll();
        resetButton.interactable = dice.CanReset();
        dieValue = dice.GetValue();
        if(dieValue != 0)
        {
            diceDisplay.text = "You rolled\na\n" + dieValue + "!"; 
        }
        else
        {
            diceDisplay.text = "";
        }
    }
}
