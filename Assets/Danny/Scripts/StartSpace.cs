using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSpace : MonoBehaviour
{
    private Text tileText;
    // Start is called before the first frame update
    void Awake()
    {
        tileText = GetComponentInChildren<Text>();
        tileText.text = "START";
    }

    public void SetTileText(string nametext)
    {
        tileText.text = "Start\n" + nametext;
    }
}
