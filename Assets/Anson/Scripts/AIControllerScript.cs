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
    Wait_Dice,
    Decide_Movement,
    Wait_PlayerMove,
    Decide_Suggest
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
    [Header("Other Components")]
    [SerializeField] UserController userController;
    [SerializeField] BoardManager boardManager;
    [SerializeField] RoundManager roundManager;


    // Start is called before the first frame update
    void Awake()
    {
        AssignAllComponents();
    }

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
                Decide_Suggestion();
                break;
            default:
                if (Time.time - startTurnTime > maxTurnTime)
                {
                    Debug.Log("Max time spent");

                    SetAIMode(AIMode.EndTurn);
                }
                break;

        }
    }

    public void AIThink()
    {
        if (currentAIMode.Equals(AIMode.Thinking) && previousAIMode.Equals(AIMode.Wait_PlayerMove)&&currentPlayerController.IsInRoom() && roundManager.CanSug)
        {
            SetAIMode(AIMode.Suggestion);
        }else if (currentAIMode.Equals(AIMode.Thinking) && roundManager.CanRoll)
        {
            SetAIMode(AIMode.Move);
        }
        else
        {
            SetAIMode(AIMode.EndTurn);
        }
    }

    public void SetAIMode(AIMode a)
    {
        print("AI changing from: " + currentAIMode + " to " + a);
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
        RoomEntryBoardTileScript possibleEntry = boardManager.GetEntryTileInMovable();
        FreeRollBoardTileScript possibleFreeRoll = boardManager.GetFreeRollTileInMovable();


        //Deciding which tile to go
        BoardTileScript selectedTile = null;
         if (possibleFreeRoll != null)
        {
            selectedTile = possibleFreeRoll;
            print("AI going to possible FreeRoll:" + selectedTile.ToString());
        }
        else if (possibleEntry != null)
        {
            selectedTile = possibleEntry;
            print("AI going to possible Entry:" + selectedTile.ToString());
        }
        else
        {
            //randomly selects one
            selectedTile = movable[Random.Range(0, movable.Count) % movable.Count];
        }
        userController.SelectTile(selectedTile);
        /*
        if (selectedTile != null && selectedTile is FreeRollBoardTileScript)
        {
            SetAIMode(AIMode.Wait_Dice);
        }
        else
        {
            SetAIMode(AIMode.Wait_PlayerMove);
        }
        */
        SetAIMode(AIMode.Wait_PlayerMove);
        if (currentPlayerController.IsInRoom()||selectedTile is RoomEntryBoardTileScript || selectedTile is FreeRollBoardTileScript)
        {
            DelayDecision(pauseTime);
        }

    }

    void Decide_Suggestion()
    {
        SetAIMode(AIMode.Decide_Suggest);
        userController.SetCharacter((CharacterEnum)Random.Range(0, 5));
        userController.SetRoom(currentPlayerController.GetCurrentRoom().Room);
        userController.SetWeapon((WeaponEnum)Random.Range(0, 5));
        userController.MakeSuggestion();
        DelayDecision(pauseTime);
        SetAIMode(AIMode.Thinking);
    }


    void DelayDecision(float t)
    {
        print("AI: delaying decision by:" + t.ToString()+"sec.");
        lastDecisionTime += t;

    }


}
