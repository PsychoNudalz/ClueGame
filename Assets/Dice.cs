using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Die[] dice;
    int diceValue;
    private CameraCloseUp mainCamera;
    private bool setCloseUpCamera;

    // Start is called before the first frame update
    void Start()
    {
        dice = GetComponentsInChildren<Die>();
        diceValue = 0;
        mainCamera = GameObject.FindObjectOfType<Camera>().GetComponent<CameraCloseUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (setCloseUpCamera)
        {
            mainCamera.SetCloseUp(CameraTarget.Centre);
        }
    }

    public void RollDice()
    {
        setCloseUpCamera = true;
        foreach (Die die in dice)
        {
            die.RollDie();
        }
    }

    public void ResetDice()
    {

        if (CanReset())
        {
            setCloseUpCamera = false;
            mainCamera.ClearCloseUp();
            foreach (Die die in dice)
            {
                die.ResetDie();
            }
        }
    }

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

    public int GetValue()
    {
        CalculateValue();
        if(diceValue > 0)
        {
            return diceValue;
        }
        return 0;
    }

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
