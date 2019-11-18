using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransitionEnteringBehaviour : EnteringBehaviour
{
    [SerializeField] private bool transitionIn = true;
    [SerializeField] private Vector2 direction = Vector2.left;

    [SerializeField] private bool _slide = true;
    [SerializeField] private GameObject _canvas = null;
    [SerializeField] private RectTransform _rt = null;
    [SerializeField] private TransitionData _transitionData = null;

    protected override void OnStateEnter() {
        if (_canvas != null) _canvas.SetActive(true);
        if (_rt != null) _rt.gameObject.SetActive(true);

        if (_slide) {
            StartCoroutine(_transitionData.StartTransitionSlide(_rt, transitionIn, direction, complete => {}));
        } else {
            StartCoroutine(_transitionData.StartTransitionScale(_rt, transitionIn, complete => {}));
        }
    }

    protected override void OnStateExit() {}
}
