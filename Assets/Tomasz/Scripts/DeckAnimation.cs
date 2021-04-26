using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class for the deck animation 
/// </summary>
public class DeckAnimation : MonoBehaviour
{
    bool isUp = false;
    bool stayUp = false;
    Animator deckAnimator;

    private void Start()
    {
        deckAnimator = GetComponent<Animator>();
    }
    /// <summary>
    /// Animation for the deck of cards to go up
    /// </summary>
    public void GoUp()
    {
        isUp = true;
        deckAnimator.SetBool("isUp", isUp);
    }
    /// <summary>
    /// Animation for the deck of cards to go down
    /// </summary>
    public void GoDown()
    {
        isUp = false;
        deckAnimator.SetBool("isUp", isUp);
    }
}
