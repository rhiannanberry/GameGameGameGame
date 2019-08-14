using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PlayerGame;
using static MinigameState;

public class MinigameStateManager : Observer
{
    public MinigameState state;
    public MinigameStateManager() {
        EVENTTYPE = EventType.PLAYERGAME;
    }

    public override void OnNotify(Object entity, GameEvent gameEvent) {

        state = PAUSED;

        switch(((PlayerGameEvent) gameEvent).EVENT) {
            case WIN: Debug.Log("win"); break;

            case LOSE: Debug.Log("lose"); break;

            case PAUSE: Debug.Log("Pause"); break;

            case UNPAUSE: Debug.Log("Unpause"); state = RUNNING; break;

            case START: Debug.Log("Start"); state = RUNNING; break;
        }
    }
}

public enum MinigameState {RUNNING, PAUSED};