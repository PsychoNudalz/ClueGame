using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIMode
{
    Thinking,
    Move,
    Suggestion,
    Accusation,
    EndTurn,
    Waiting,
    None,
    GameOver,
    Wait_Dice,
    Decide_Movement,
    Wait_PlayerMove,
    Decide_Suggest,
    Wait_Suggest,
    Decide_Accuse
}

public class AIControllerScript : MonoBehaviour
{
    [Header("AI Mode")]
    [SerializeField] AIMode currentAIMode = AIMode.Waiting;
    [SerializeField] AIMode previousAIMode = AIMode.None;
    [Header("AI Stats")]
    [SerializeField] bool isAIActive;
    [SerializeField] CharacterEnum currentCharacter;
    [SerializeField] PlayerMasterController currentPlayerController;
    [SerializeField] float decisionTime = 0.1f;
    [SerializeField] float pauseTime = 3f;
    [SerializeField] float maxTurnTime = 10f;
    float lastDecisionTime;
    float startTurnTime = 0;
    [SerializeField] List<Card> toGuessList;
    [Header("Other Components")]
    [SerializeField] UserController userController;
    [SerializeField] BoardManager boardManager;
    [SerializeField] RoundManager roundManager;


    // Start is called before the first frame update
    void Awake()
    {
        AssignAllComponents();
    }
    /// <summary>
    /// For assigning all connected components
    /// </summary>
    public void AssignAllComponents()
    {

        if (boardManager == null)
        {
            boardManager = FindObjectOfType<BoardManager>();
        }
        if (!userController)
        {
            userController = FindObjectOfType<UserController>();
        }
        if (!roundManager)
        {
            roundManager = FindObjectOfType<RoundManager>();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAIActive)
        {
            return;
        }
        if (Time.time - lastDecisionTime > decisionTime)
        {
            lastDecisionTime = Time.time;
            AIBehaviour();
            PrintStatus();
        }
    }

    void PrintStatus()
    {
        print("AI:" + currentCharacter.ToString() + ". Current Mode:" + currentAIMode + ". Previous Mode:" + previousAIMode);

    }

    public void SetActive(bool b = false, PlayerMasterController setPlayer = null)
    {
        if (b)
        {
            isAIActive = true;
            currentPlayerController = setPlayer;
            if (currentPlayerController != null)
            {
                currentCharacter = currentPlayerController.GetCharacter();
                print("AI active: " + currentPlayerController.ToString());
                startTurnTime = Time.time;
            }
            StartTurn();
        }
        else
        {
            if (isAIActive)
            {
                isAIActive = false;
                if (currentPlayerController != null)
                {
                    print("AI deactivate: " + currentPlayerController.ToString());
                }
            }

        }
    }
    void AIBehaviour()
    {
        Debug.Log("AI " + currentCharacter + " decided:");
        switch (currentAIMode)
        {
            case (AIMode.Thinking):
                Debug.Log("To Think");
                AIThink();
                break;

            case (AIMode.Move):
                Debug.Log("To Move");
                RollDice();
                break;

            case (AIMode.EndTurn):
                Debug.Log("End Turn");

                EndTurn();
                break;
            case (AIMode.Wait_Dice):
                Debug.Log("Waiting on dice");

                if (CanMove())
                {
                    Decide_Movement();
                }
                break;
            case (AIMode.Wait_PlayerMove):
                Debug.Log("Waiting on player move");
                if (!IsTokenMoving())
                {
                    if (currentPlayerController.GetTile() is FreeRollBoardTileScript)
                    {
                        SetAIMode(AIMode.Wait_Dice);
                    }
                    else
                    {
                        SetAIMode(AIMode.Thinking);

                    }
                }
                break;
            case (AIMode.Suggestion):
                Debug.Log("Suggest");
                Decide_Suggestion();
                break;
            case (AIMode.Wait_Suggest):
                Debug.Log("Waiting for card return");
                break;

            case (AIMode.Accusation):
                Debug.LogWarning(currentCharacter.ToString() + " Accuse");

                Decide_Accusation();
                break;
            default:
                if (Time.time - startTurnTime > maxTurnTime * 5f)
                {
                    Debug.Log("Max time spent");

                    SetAIMode(AIMode.EndTurn);
                }
                break;

        }
    }

    public void AIThink()
    {
        if (currentAIMode.Equals(AIMode.Thinking) && currentPlayerController.IsInRoom() && roundManager.CanSug && !roundManager.CanRoll)
        {
            SetAIMode(AIMode.Suggestion);
        }
        else
        if (currentAIMode.Equals(AIMode.Thinking) && roundManager.CanRoll)
        {
            SetAIMode(AIMode.Move);
        }
        else if (currentAIMode.Equals(AIMode.Thinking) && !roundManager.CanRoll && !roundManager.CanSug && roundManager.CanAcc && CanAccuse())
        {
            SetAIMode(AIMode.Accusation);
        }
        else
        {
            SetAIMode(AIMode.EndTurn);
        }
        print("AI thought to: " + currentAIMode);
    }

    public void SetAIMode(AIMode a)
    {
        print("AI changing from: " + currentAIMode + " to " + a);
        if (a.Equals(AIMode.GameOver))
        {
            Debug.LogWarning("AI is eliminated");
            startTurnTime = Time.time;
            DelayDecision(pauseTime);
        }
        previousAIMode = currentAIMode;
        currentAIMode = a;
    }

    public void RollDice()
    {
        userController.RollDice();
        SetAIMode(AIMode.Wait_Dice);
    }

    public void EndTurn()
    {
        SetAIMode(AIMode.None);
        userController.EndTurn();
    }

    void StartTurn()
    {
        SetAIMode(AIMode.EndTurn);
        SetAIMode(AIMode.Thinking);

        //Tempurary for now
        //SetAIMode(AIMode.Move);
    }

    bool CanMove()
    {
        return boardManager.MovableTile.Count != 0;
    }

    bool IsTokenMoving()
    {
        return currentPlayerController.IsMoving();
    }

    public void Decide_Movement()
    {
        SetAIMode(AIMode.Decide_Movement);
        List<BoardTileScript> movable = boardManager.MovableTile;
        List<RoomEntryBoardTileScript> possibleEntry = boardManager.GetRandomEntryTileInMovable();
        FreeRollBoardTileScript possibleFreeRoll = boardManager.GetFreeRollTileInMovable();
        FreeSuggestionTileScript possibleSuggestRoll = boardManager.GetFreeSuggesTileInMovable();


        //Deciding which tile to go
        BoardTileScript selectedTile = null;

        if (possibleSuggestRoll != null)
        {
            selectedTile = possibleSuggestRoll;
            print("AI going to possible FreeSuggestion:" + selectedTile.ToString());

        }

        //Decide Possible Entry
        else if (possibleEntry.Count != 0)
        {

            int j = 0;
            if (currentPlayerController.IsInRoom())
            {

                for (int i = 0; i < possibleEntry.Count; i++)
                {
                    j = i;
                    if (!possibleEntry[i].Room.Equals(currentPlayerController.GetCurrentRoom().Room))
                    {
                        print("found not the same:" + possibleEntry[i].Room + "  " + possibleEntry[i].Room.Equals(currentPlayerController.GetCurrentRoom()));
                        break;
                    }
                    print(possibleEntry[i].Room + "  " + possibleEntry[i].Room.Equals(currentPlayerController.GetCurrentRoom()));
                }
            }
            selectedTile = possibleEntry[j];

            print("AI going to possible Entry:" + selectedTile.ToString());
        }
        //Decide Possible Free Roll
        else if (possibleFreeRoll != null)
        {
            selectedTile = possibleFreeRoll;
            print("AI going to possible FreeRoll:" + selectedTile.ToString());
        }
        //Pick a random tile if none found
        else
        {
            //randomly selects one
            selectedTile = movable[Random.Range(0, movable.Count) % movable.Count];
        }
        userController.SelectTile(selectedTile);
        if (selectedTile is FreeSuggestionTileScript)
        {
            SetAIMode(AIMode.Suggestion);
        }
        else
        {
            SetAIMode(AIMode.Wait_PlayerMove);
        }
        if (currentPlayerController.IsInRoom() || selectedTile is RoomEntryBoardTileScript || selectedTile is FreeRollBoardTileScript || selectedTile is FreeSuggestionTileScript)
        {
            DelayDecision(pauseTime * 2);
        }

    }

    void Decide_Suggestion()
    {
        SetAIMode(AIMode.Decide_Suggest);
        LoadToGuessList(currentPlayerController.GetToGuessCards());
        userController.SetCharacter(Decide_Character());
        if (currentPlayerController.IsInRoom())
        {
            userController.SetRoom(currentPlayerController.GetCurrentRoom().Room);
        }
        else
        {
            userController.SetRoom(Decide_Room());
        }
        userController.SetWeapon(Decide_Weapon());
        SetAIMode(AIMode.Wait_Suggest);
        userController.MakeSuggestion();
        DelayDecision(pauseTime);
    }

    public void NotifySuggestion()
    {
        print("AI " + EnumToString.GetStringFromEnum(currentCharacter));
        SetAIMode(AIMode.Thinking);
    }

    void LoadToGuessList(List<Card> c)
    {
        toGuessList = new List<Card>(c);

    }

    CharacterEnum Decide_Character()
    {
        //return CharacterEnum.Initial;
        int offset = Random.Range(0, 21);
        Card temp;
        for (int i = 0; i < toGuessList.Count; i++)
        {
            temp = toGuessList[(i + offset) % toGuessList.Count];
            if (temp is CharacterCard)
            {
                return (CharacterEnum)temp.GetCardType();
            }
        }
        return (CharacterEnum)(Random.Range(0, 6) % 6);
    }

    WeaponEnum Decide_Weapon()
    {
        //return CharacterEnum.Initial;
        int offset = Random.Range(0, 21);
        Card temp;
        for (int i = 0; i < toGuessList.Count; i++)
        {
            temp = toGuessList[(i + offset) % toGuessList.Count];
            if (temp is WeaponCard)
            {
                return (WeaponEnum)temp.GetCardType();
            }
        }
        return (WeaponEnum)(Random.Range(0, 6) % 6);
    }
    Room Decide_Room()
    {
        //return CharacterEnum.Initial;
        int offset = Random.Range(0, 21);
        Card temp;
        for (int i = 0; i < toGuessList.Count; i++)
        {
            temp = toGuessList[(i + offset) % toGuessList.Count];
            if (temp is RoomCard)
            {
                return (Room)temp.GetCardType();
            }
        }
        return (Room)(Random.Range(0, 9) % 9);
    }

    void Decide_Accusation()
    {
        SetAIMode(AIMode.Decide_Accuse);
        LoadToGuessList(currentPlayerController.GetToGuessCards());
        userController.SetCharacter(Decide_Character());
        userController.SetRoom(Decide_Room());
        userController.SetWeapon(Decide_Weapon());
        userController.MakeAccusation();
        DelayDecision(pauseTime);
        SetAIMode(AIMode.Thinking);
    }

    bool CanAccuse()
    {
        LoadToGuessList(currentPlayerController.GetToGuessCards());

        if (toGuessList.Count > 6)
        {
            return false;
        }

        float chance = Random.Range(0, 100f);
        print("AI chance Accuse: " + chance);
        if (chance / 100f > ((toGuessList.Count - 3f) / 6f))
        {
            return true;
        }
        return false;
    }



    void DelayDecision(float t)
    {
        print("AI: delaying decision by:" + t.ToString() + "sec.");
        lastDecisionTime += t;

    }


}
