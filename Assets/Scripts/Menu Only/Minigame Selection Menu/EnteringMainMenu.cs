using UnityEngine;

public class EnteringMainMenu : EnteringBehaviour
{
    public float enterSceneDelay = 2f;

    protected override void OnStateEnter() {
        Invoke("DelayedEnter", enterSceneDelay);
    }

    protected override void OnStateExit() {

    }

    private void DelayedEnter() {
        GameStateManager.INSTANCE.TriggerStateChange(GameState.INSCENE);
    }
}
