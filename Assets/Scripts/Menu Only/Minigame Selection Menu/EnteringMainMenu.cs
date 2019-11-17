using UnityEngine;

public class EnteringMainMenu : EnteringBehaviour
{

    protected override void OnStateEnter() {
        GameStateManager.INSTANCE.TriggerStateChange(GameState.INSCENE);
    }

    protected override void OnStateExit() {

    }
}
