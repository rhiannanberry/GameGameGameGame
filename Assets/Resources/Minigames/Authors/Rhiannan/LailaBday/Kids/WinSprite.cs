using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSprite : MonoBehaviour
{
    public Sprite sp;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetWinSprite() {
        sr.sprite = sp;
    }
} 
