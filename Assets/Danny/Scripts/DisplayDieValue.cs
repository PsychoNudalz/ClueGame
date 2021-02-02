using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDieValue : MonoBehaviour
{
    [SerializeField]
    Die die;
    [SerializeField]
    Button rollButton;
    [SerializeField]
    Button resetButton;

    int dieValue;
    // Update is called once per frame
    void Update()
    {
        rollButton.interactable = die.IsReadyToRoll();
        resetButton.interactable = die.CanReset();
        dieValue = die.GetValue();
        if(dieValue != 0)
        {
            GetComponent<Text>().text = "You rolled a " + dieValue + "!"; 
        }
        else
        {
            GetComponent<Text>().text = "";
        }
        
        }
}
