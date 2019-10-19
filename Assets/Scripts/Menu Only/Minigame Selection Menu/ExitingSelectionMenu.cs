using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitingSelectionMenu : ExitingBehaviour
{
    [SerializeField] private RectTransform _rt = null;
    [SerializeField] private TransitionData _transitionData = null;

    protected override void OnStateEnter() {
        _rt.gameObject.GetComponent<Image>().enabled = true;
        StartCoroutine( _transitionData.Transition( 
            (t) => {
                _rt.anchorMin = new Vector2( _rt.anchorMin.x,   1 - t );
            },
            (complete) => {
                if (complete) TransitionController.CompleteExitToScene();
            }));
    }

    protected override void OnStateExit() {

    }
}
