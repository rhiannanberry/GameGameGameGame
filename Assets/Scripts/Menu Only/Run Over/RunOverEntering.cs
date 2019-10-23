using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOverEntering : EnteringBehaviour
{
    [SerializeField] private TransitionData _bgTransition = null;
    [SerializeField] private RectTransform _bgRect = null;
    protected override void OnStateEnter() {
        StartCoroutine( _bgTransition.Transition(
            (t) => {
                _bgRect.anchorMin = new Vector2( _bgRect.anchorMin.x,     t );
                _bgRect.anchorMax = new Vector2( _bgRect.anchorMax.x,   t+1 );
            },
            (complete) => {
                GameStateManager.INSTANCE.TriggerStateChange(GameState.INSCENE);
            }
        ));
    }

    protected override void OnStateExit() {

    }
}
