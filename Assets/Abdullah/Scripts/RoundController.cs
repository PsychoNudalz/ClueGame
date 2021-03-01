using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    TurnController player;
    Dice dice;
    PlayerControlScript move;
    void RollDice() 
    {
        dice.RollDice();
    }

    void MovePlayer() {
       
    }

    void MakeSuggestion() { }

    void MakeAccusation() { }

    void EndTurn() { }
}
