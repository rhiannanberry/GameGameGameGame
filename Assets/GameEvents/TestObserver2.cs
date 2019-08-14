using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObserver2 : Observer
{
    public TestObserver2() {
        EVENTTYPE = EventType.T2;
    }

    public override void OnNotify(Object entity, GameEvent gameEvent) {
        Debug.Log(gameEvent.TYPE.ToString());
    }
}
