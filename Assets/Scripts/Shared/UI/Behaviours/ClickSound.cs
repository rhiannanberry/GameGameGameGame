using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickSound : MonoBehaviour
{
    void Awake() {
        GetComponent<Button>().onClick.AddListener(() => {SoundManager._PlaySound("click");});
    }
}
