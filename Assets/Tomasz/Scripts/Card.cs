using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour
{
    new public Enum names;
    public abstract Enum GetCardType();

    //public abstract bool Equals(object obj);

}
