public class PlayerGameEvent : GameEvent {
    public PlayerGame EVENT;
    
    public PlayerGameEvent(PlayerGame t) {
        TYPE = EventType.TEST;
        EVENT = t;
    }
}

public enum PlayerGame {START,WIN, LOSE, PAUSE,UNPAUSE};

