using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MechFlavorText : MonoBehaviour {
    [SerializeField] private string[] winningText;
    private void Start() {
        if (FindObjectOfType<Draw>().HasDrawn) {
            GetComponent<TMP_Text>().text = winningText[Random.Range(0, winningText.Length)];
        } else {
            GetComponent<TMP_Text>().text = "Minimalist!";
        }
    }
}