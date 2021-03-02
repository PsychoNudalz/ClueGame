using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDiceValue : MonoBehaviour
{
    
    [SerializeField]
    Button rollButton;
    [SerializeField]
    Button resetButton;
    Text diceDisplay;
    Dice dice;
    int dieValue;

    private void Start()
    {
        diceDisplay = GetComponent<Text>();
        dice = FindObjectOfType<Dice>();
    }

    // Update is called once per frame
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
