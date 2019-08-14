using System.Collections;
using System.Collections.Generic;

public enum EventType { GENERIC, TEST, T2 , PLAYERGAME};

public abstract class GameEvent
{
    public EventType TYPE = EventType.GENERIC;
    public System.Enum EVENT;
}


public enum T2 { WWWWWW };

public class T2Event : GameEvent {
    public T2 EVENT;
    public T2Event(T2 t) {
        TYPE = EventType.T2;
        EVENT = t;
    }
}

public enum Test { EEEEEE };

public class TestEvent : GameEvent {
    public Test EVENT;
    
    public TestEvent(Test t) {
        TYPE = EventType.TEST;
        EVENT = t;
    }
}