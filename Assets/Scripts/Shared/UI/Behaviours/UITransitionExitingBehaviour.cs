using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransitionExitingBehaviour : ExitingBehaviour
{
    [SerializeField] private bool transitionIn = true;
    [SerializeField] private Vector2 direction = Vector2.left;

    [SerializeField] private GameObject _canvas = null;
    [SerializeField] private RectTransform _rt = null;
    [SerializeField] private TransitionData _transitionData = null;

    protected override void OnStateEnter() {
        if (_canvas != null) _canvas.SetActive(true);
        if (_rt != null) _rt.gameObject.SetActive(true);

        StartCoroutine(_transitionData.StartTransitionSlide(_rt, transitionIn, direction, complete => {}));
    }

    protected override void OnStateExit() {}

    public void ReverseDirection() {
        direction = -1 * direction;
    }
}
