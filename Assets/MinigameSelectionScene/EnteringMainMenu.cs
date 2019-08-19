using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteringMainMenu : EnteringBehaviour
{
    [SerializeField] private TransitionData _mainMenuEnterTransition;
    [SerializeField] private Transform _mainMenuTransform;
    protected override void OnStateEnter() {
        StartCoroutine( _mainMenuEnterTransition.Transition(
            (t) => {
                _mainMenuTransform.localScale = Vector2.one * t ;
            },
            (complete) => {
                GameStateManager.INSTANCE.TriggerStateChange(GameState.INSCENE);
            }
        ));
    }
    protected override void OnStateExit() {

    }
}
