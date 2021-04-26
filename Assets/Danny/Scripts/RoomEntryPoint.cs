using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class for the room entry point Gameobject
/// </summary>
public class RoomEntryPoint : MonoBehaviour
{
    private RoomScript roomScript;
    /// <summary>
    /// Getter and setter for the roomscript where entry point is located
    /// </summary>
    public RoomScript RoomScript { get => roomScript; set => roomScript = value; }
}
