using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObserver : Observer
{
    public TestObserver() {
        EVENTTYPE = EventType.TEST;
    }

    public override void OnNotify(Object entity, GameEvent gameEvent) {
        Debug.Log(gameEvent.TYPE.ToString());
    }
}
