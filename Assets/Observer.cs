using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class wants to know when another object does something

// managers would be observers 
public abstract class Observer
{
    public EventType EVENTTYPE = EventType.GENERIC;
    public virtual void ValidateAndNotify(Object entity, GameEvent gameEvent) {
        if (gameEvent.TYPE == EVENTTYPE) {
            OnNotify(entity, gameEvent);
        }
    }

    public abstract void OnNotify(Object entity, GameEvent gameEvent);
}
