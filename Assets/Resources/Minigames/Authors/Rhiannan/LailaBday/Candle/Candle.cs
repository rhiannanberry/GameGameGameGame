using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MinigameBehaviour
{
    public static int numCandles = 0;
    public Sprite outCandleSprite;
    public string blowSound;
    private SpriteRenderer sr;
    private bool _canInput = false;

    protected override void Start()
    {
        numCandles++;
        sr = GetComponent<SpriteRenderer>();

        base.Start();
    }

    protected override void OnStateEnter() {
        _canInput = true;
    }
    protected override void OnStateExit() {
        _canInput = false;
    }    

    
    public void PutOut() {   
        SoundManager._PlaySound(blowSound);     
        GetComponent<Animator>().enabled = false;
        sr.sprite = outCandleSprite;
        numCandles--;
        CheckWin();
    }

    public void OnMouseDown() {
        if (_canInput && GetComponent<Animator>().enabled)
            PutOut();
    }

    private void CheckWin() {
        if (numCandles == 0)
            PersistentDataManager.RUN.GameWon();
    }
}
