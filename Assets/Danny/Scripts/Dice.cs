using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for the Dice Gameobject
/// </summary>
public class Dice : MonoBehaviour
{
    private Die[] dice;
    private int diceValue;
    private CameraCloseUp mainCamera;
    private bool setCloseUpCamera;

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
    }

    /// <summary>
    /// Initialise Dice
    /// </summary>
    private void Initialise()
    {
        dice = GetComponentsInChildren<Die>();
        diceValue = 0;
        mainCamera = GameObject.FindObjectOfType<Camera>().GetComponent<CameraCloseUp>();
    }

    /// <summary>
    /// Set close up camera if set close up = true
    /// </summary>
    void Update()
    {
        if (setCloseUpCamera)
        {
            mainCamera.SetCloseUp(CameraTarget.Centre);
        }
    }

    /// <summary>
    /// Roll the Dice
    /// </summary>
    public void RollDice()
    {
        setCloseUpCamera = true;
        foreach (Die die in dice)
        {
            die.RollDie();
        }
    }

    /// <summary>
    /// Reset the dice
    /// </summary>
    /// <param name="forced">Set true if dice positions not right</param>
    public void ResetDice(bool forced = false)
    {
        try
        {
            if (CanReset() || forced)
            {
                setCloseUpCamera = false;
                mainCamera.ClearCloseUp();
                foreach (Die die in dice)
                {
                    die.ResetDie();
                }
            }
        }
        catch (System.NullReferenceException e) { }
        
    }

    /// <summary>
    /// Can Dice be reset
    /// </summary>
    /// <returns>true if dice can reset, false otherwise</returns>
    public bool CanReset()
    {
        bool canResetDice = true;
        {
            foreach(Die die in dice)
            {
                if (die.CanResetDie().Equals(false))
                {
                    canResetDice = false;
                }
            }
        }
        return canResetDice;
    }

    /// <summary>
    /// Calculate total value of all dice
    /// </summary>
    public void CalculateValue()
    {
        int value = 0;
        bool isValid = true;

        foreach(Die die in dice)
        {
            int dieValue = die.GetValueDie();
            if (dieValue == 0)
            {
                isValid = false;
                break;
            }
            else
            {
                value += dieValue;
            }
        }
        if (isValid)
        {
            diceValue = value;
        }
        else
        {
            diceValue = 0;
        }
    }

    /// <summary>
    /// Get value of all dice
    /// </summary>
    /// <returns>Value of all dice, 0 if invalid</returns>
    public int GetValue()
    {
        CalculateValue();
        if(diceValue > 0)
        {
            return diceValue;
        }
        return 0;
    }

    /// <summary>
    /// Are dice ready to roll
    /// </summary>
    /// <returns>true if ready to roll, false if not</returns>
    public bool ReadyToRoll()
    {
        bool areReady = true;
        {
            foreach(Die die in dice)
            {
                if (!die.IsReadyToRoll())
                {
                    areReady = false;
                }
            }
        }
        return areReady;
    }
}
