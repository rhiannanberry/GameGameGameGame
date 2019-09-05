using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : ExitingBehaviour
{
    float moveUp = 1.5f;
    public Vector2 target;
    protected override void Start()
    {
        target = transform.position;
        base.Start();
    }

    protected override void OnStateEnter() {
        if(PersistentDataManager.run.gameWon) target += new Vector2(0,moveUp);
    }

    protected override void OnStateExit() {}

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, 5f * Time.deltaTime );
    }
}
