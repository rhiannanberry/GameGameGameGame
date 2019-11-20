using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[ExecuteInEditMode]
public class UITransitionEnteringBehaviour : EnteringBehaviour
{
    

    [SerializeField] private bool transitionIn = true;
    [SerializeField] private Vector2 direction = Vector2.left;

    [SerializeField] private bool _slide = true;
    [SerializeField] private GameObject _canvas = null;
    [SerializeField] private RectTransform _rt = null;
    [SerializeField] private TransitionData _transitionData = null;

    [Header("Editor Options")]
    [SerializeField] private bool hideInEditor = true;

    protected override void OnStateEnter() {
        if (_canvas != null) _canvas.SetActive(true);
        if (_rt != null) _rt.gameObject.SetActive(true);

        if (_slide) {
            StartCoroutine(_transitionData.StartTransitionSlide(_rt, transitionIn, direction, complete => {}));
        } else {
            StartCoroutine(_transitionData.StartTransitionScale(_rt, transitionIn, complete => {
                foreach (var t in _rt.GetComponentsInChildren<TextMeshProUGUI>()) {
                    t.ForceMeshUpdate();
                }
            }));
        }
    }

    protected override void OnStateExit() {}

    private void Update() {
        #if UNITY_EDITOR
        if (hideInEditor && EditorApplication.isPlaying == false && _rt != null && _rt.gameObject.activeSelf) {
            _rt.gameObject.SetActive(false);
        }
        #endif
    }
}
