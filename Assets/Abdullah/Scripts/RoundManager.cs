using UnityEngine;

public class RoundManager : MonoBehaviour
{
    BoardManager boardManagerScript;
    TurnController turnControllerScript;
    PlayerMasterController currentPlayer;
    Dice dice;
    enum RoundMode { None, Move, Suggestion, Accusation };



    public void SetMode() {
        print("Mode Set");
    }

    public void RollDice() {
       dice.RollDice();
    }

    private void Update()
    {
        
    }

    void ShowMovableTiles() {
        
    }

    
    void SetCurrentPlayer(){
        turnControllerScript.SetCurrentPlayerToNext();
    }

    void GetCurrentPlayer() {
        turnControllerScript.GetCurrentPlayer();
    }

    void StartSuggestion() { 
       
    }

    void MakeSuggestion() { 
    
    }

    void FindPlyaerWithCard() { 
    
    }

    void PickCardToShow() { 
    
    }

    void AiPickCardToShow() {
    
    }

    bool CheckAccustation() {
        return true;
    }

    void EndRound() { 
    
    }

    void ResetRound(){

    }

    void CanPlayerTakeShortCute() { 
    
    }
}
