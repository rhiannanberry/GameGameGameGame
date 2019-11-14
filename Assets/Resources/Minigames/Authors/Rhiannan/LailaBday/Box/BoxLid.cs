using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLid : MinigameBehaviour
{
    private Vector2 startPos;
    public float moveDist = 1f;
    public float downSpeed = .1f;
    public float upSpeed = .1f;

    private bool _canInput = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        startPos = transform.position;
        base.Start();
    }

    protected override void OnStateEnter() {
        _canInput = true;
    }
    protected override void OnStateExit() {
        _canInput = false;
    }    

    // Update is called once per frame
    void Update()
    {
        if (!_canInput) return;
       

       
        if (startPos.y < transform.position.y) {
            transform.position -= new Vector3(0, downSpeed,0)*Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.position += new Vector3(0, upSpeed,0);
        }
        if (transform.position.y >= startPos.y + moveDist) {
            PersistentDataManager.run.GameWon();
            _canInput = false;
        }
        
    }
}
