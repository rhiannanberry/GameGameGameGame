using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FlavorText : MonoBehaviour
{
    private TextMeshProUGUI txt;

    public string[] win;
    public string[] lose;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
        System.Random random = new System.Random();
        if (PersistentDataManager.run.gameWon) {
            txt.text = win[random.Next(win.Length)];
        } else {
            txt.text = lose[random.Next(lose.Length)];
        }

    }


}
