using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckAnimation : MonoBehaviour
{
    bool isUp = false;
    bool stayUp = false;
    Animator deckAnimator;

    private void Start()
    {
        deckAnimator = GetComponent<Animator>();
    }
    public void GoUp()
    {
        isUp = true;
        deckAnimator.SetBool("isUp", isUp);
    }

    public void GoDown()
    {
        
        
            isUp = false;
            deckAnimator.SetBool("isUp", isUp);
        
    }


}
