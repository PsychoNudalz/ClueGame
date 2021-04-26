using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this enum notes what mode the AI is in
/// </summary>
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
/// <summary>
/// this class controlls the AI's behaviour and actions
/// </summary>
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

    [Header("Debug")]
    [SerializeField] bool outputDebugText;
    [SerializeField] List<string> outputDebugStack;

    public AIMode CurrentAIMode { get => currentAIMode; }
    public AIMode PreviousAIMode { get => previousAIMode; }


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
            OutputDebugStatus();
        }
    }
    /// <summary>
    /// to output the current status and previous status of the AI
    /// </summary>
    void OutputDebugStatus()
    {
        OutputDebug("AI:" + currentCharacter.ToString() + ". Current Mode: " + currentAIMode + ". Previous Mode: " + previousAIMode);

    }


    /// <summary>
    /// to activate the AI or not
    /// set which player the AI is controlling
    /// </summary>
    /// <param name="b">to activate the AI or not</param>
    /// <param name="setPlayer">the player the AI gets to control</param>
    public void SetActive(bool b = false, PlayerMasterController setPlayer = null)
    {
        if (b)
        {
            isAIActive = true;
            currentPlayerController = setPlayer;
            if (currentPlayerController != null)
            {
                currentCharacter = currentPlayerController.GetCharacter();
                OutputDebug("AI active: " + currentPlayerController.ToString());
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
                    OutputDebug("AI deactivate: " + currentPlayerController.ToString());
                }
            }

        }
    }
    /// <summary>
    /// dictates what the AI will do depending on what mode it is in
    /// </summary>
    void AIBehaviour()
    {
        OutputDebug("AI " + currentCharacter + " currently: ");
        switch (currentAIMode)
        {
            case (AIMode.Thinking):
                OutputDebug("To Think");
                AIThink();
                break;

            case (AIMode.Move):
                OutputDebug("To Move");
                RollDice();
                break;

            case (AIMode.EndTurn):
                OutputDebug("End Turn");

                EndTurn();
                break;
            case (AIMode.Wait_Dice):
                OutputDebug("Waiting on dice");

                if (CanMove())
                {
                    Decide_Movement();
                }
                break;
            case (AIMode.Wait_PlayerMove):
                OutputDebug("Waiting on player move");
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
                OutputDebug("Suggest");
                Decide_Suggestion();
                break;
            case (AIMode.Wait_Suggest):
                OutputDebug("Waiting for card return");
                break;

            case (AIMode.Accusation):
                OutputDebug(currentCharacter.ToString() + " Accuse");
                Decide_Accusation();
                break;
            default:
                if (Time.time - startTurnTime > maxTurnTime * 5f)
                {
                    OutputDebug("Max time spent");

                    SetAIMode(AIMode.EndTurn);
                }
                break;

        }
    }

    /// <summary>
    /// the AI enter this when it is thinking to decide what to do
    /// this should match the flow chart of the AI
    /// </summary>
    void AIThink()
    {
        if (currentAIMode.Equals(AIMode.Thinking) && roundManager.CanRoll)
        {
            SetAIMode(AIMode.Move);
        }
        else
        if (currentAIMode.Equals(AIMode.Thinking) && currentPlayerController.IsInRoom() && roundManager.CanSug && !roundManager.CanRoll)
        {
            SetAIMode(AIMode.Suggestion);
        }

        else if (currentAIMode.Equals(AIMode.Thinking) && !roundManager.CanRoll && !roundManager.CanSug && roundManager.CanAcc && CanAccuse())
        {
            SetAIMode(AIMode.Accusation);
        }
        else
        {
            SetAIMode(AIMode.EndTurn);
        }
        OutputDebug("AI thought to: " + currentAIMode);
    }


    /// <summary>
    /// to set the AI's mode
    /// </summary>
    /// <param name="a">the mode to set the AI to</param>
    public void SetAIMode(AIMode a)
    {
        OutputDebug("AI changing from: " + currentAIMode + " to " + a);
        if (a.Equals(AIMode.GameOver))
        {
            OutputDebug("AI is eliminated");
            Debug.LogWarning("AI is eliminated");
            startTurnTime = Time.time;
            DelayDecision(pauseTime);
        }
        previousAIMode = currentAIMode;
        currentAIMode = a;
    }

    /// <summary>
    /// have AI to roll
    /// </summary>
    public void RollDice()
    {
        userController.RollDice();
        SetAIMode(AIMode.Wait_Dice);
    }

    /// <summary>
    /// have AI to end turn
    /// </summary>
    public void EndTurn()
    {
        SetAIMode(AIMode.None);
        userController.EndTurn();
    }
    /// <summary>
    /// have AI to start turn
    /// </summary>
    void StartTurn()
    {
        SetAIMode(AIMode.EndTurn);
        SetAIMode(AIMode.Thinking);

        //Tempurary for now
        //SetAIMode(AIMode.Move);
    }

    /// <summary>
    /// check if the AI player can move
    /// </summary>
    /// <returns>can the AI player move</returns>
    bool CanMove()
    {
        return boardManager.MovableTile.Count != 0;
    }


    /// <summary>
    /// check if the player token is still moving
    /// </summary>
    /// <returns>check if the token is still moving</returns>
    bool IsTokenMoving()
    {
        return currentPlayerController.IsMoving();
    }
    /// <summary>
    /// Decides where the AI should move to
    /// this should match the flow chart of the AI
    /// </summary>
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
            OutputDebug("AI going to possible FreeSuggestion: " + selectedTile.ToString());

        }

        //Decide Possible Entry
        else if (possibleEntry.Count != 0)
        {

            int j = 0;
            if (currentPlayerController.IsInRoom())
            {
                //Pick a tile that is not the same as the AI's current room
                for (int i = 0; i < possibleEntry.Count; i++)
                {
                    j = i;
                    if (!possibleEntry[i].Room.Equals(currentPlayerController.GetCurrentRoom().Room))
                    {
                        OutputDebug("found not the same room: " + possibleEntry[i].Room + "  " + possibleEntry[i].Room.Equals(currentPlayerController.GetCurrentRoom()));
                        break;
                    }
                    OutputDebug(possibleEntry[i].Room + "  " + possibleEntry[i].Room.Equals(currentPlayerController.GetCurrentRoom()));
                }
            }
            selectedTile = possibleEntry[j];

            OutputDebug("AI going to possible Entry: " + selectedTile.ToString());
        }
        //Decide Possible Free Roll
        else if (possibleFreeRoll != null)
        {
            selectedTile = possibleFreeRoll;
            OutputDebug("AI going to possible FreeRoll: " + selectedTile.ToString());
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
    /// <summary>
    /// Decides what the AI should suggest
    /// this should match the flow chart of the AI
    /// </summary>
    public void Decide_Suggestion()
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

    /// <summary>
    /// for the round manager to notify that another player finished picking a card to show
    /// </summary>
    public void NotifySuggestion()
    {
        OutputDebug("AI " + EnumToString.GetStringFromEnum(currentCharacter));
        SetAIMode(AIMode.Thinking);
    }
    /// <summary>
    /// Load player's guess list
    /// </summary>
    /// <param name="c">cards that it still needs to guess</param>
    void LoadToGuessList(List<Card> c)
    {
        toGuessList = new List<Card>(c);

    }
    /// <summary>
    /// to decide a random character from the To Guess List
    /// </summary>
    /// <returns>random character</returns>
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
    /// <summary>
    /// to decide a random weapon from the To Guess List
    /// 
    /// </summary>
    /// <returns>random weapon</returns>
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
    /// <summary>
    /// to decide a random Room from the To Guess List
    /// 
    /// </summary>
    /// <returns>random room</returns>
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
    /// <summary>
    /// Decides that cards to accuse based on the To Guess List
    /// </summary>
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

    /// <summary>
    /// check if the AI is comfident enough to accuse
    /// this should match the flow chart of the AI
    /// </summary>
    /// <returns></returns>
    bool CanAccuse()
    {
        LoadToGuessList(currentPlayerController.GetToGuessCards());
        OutputDebug("AI To Guess List Length: " + toGuessList.Count);

        if (toGuessList.Count > 6)
        {
            OutputDebug("AI to guess list too long to accuse");

            return false;
        }

        float chance = Random.Range(0, 100f);
        OutputDebug("AI chance Accuse: " + chance);
        if (chance / 100f > ((toGuessList.Count - 3f) / 6f))
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// to delay the time for the next time the AI does something in the Update
    /// </summary>
    /// <param name="t"></param>
    void DelayDecision(float t)
    {
        OutputDebug("AI: delaying decision by: " + t.ToString() + " sec.");
        lastDecisionTime += t;

    }

    /// <summary>
    /// for out putting the debug stack
    /// if outputDebugText is true, it will out put the the console as well
    /// </summary>
    /// <param name="s">string to be out put or stored in the debug stack</param>
    void OutputDebug(string s)
    {
        if (outputDebugText)
        {
            OutputDebug(s);
        }
        outputDebugStack.Add(Time.time + ": " + s);
        if (outputDebugStack.Count > 15)
        {
            outputDebugStack.RemoveAt(0);
        }
    }
    /// <summary>
    /// Get the Output Debug stack in string
    /// </summary>
    /// <returns>string of the debug stack</returns>
    public string GetOutputDebugString()
    {
        string temp = "";
        foreach (string s in outputDebugStack)
        {
            temp += s + " \n";
        }
        return temp;
    }


}
