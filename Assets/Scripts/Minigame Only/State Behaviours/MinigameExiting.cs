using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameExiting : ExitingBehaviour
{
    [Header("Master Transition")]
    [SerializeField] private float _exitTime = 5f;
    [SerializeField] private GameObject _transitionCanvas = null;

    [Header("Background Transition")]
    [SerializeField] private TransitionData _bgTransition = null;
    [SerializeField] private RectTransform _bgRect = null;

    protected override void OnStateEnter() {
        Debug.Log("EXITING");
        PersistentDataManager.INSTANCE.SaveMinigameMasterList();
        StartCoroutine(EXIT());
    }

    protected override void OnStateExit() {
        
    }

    IEnumerator EXIT() {
        _bgRect.anchorMin = new Vector2( -1, _bgRect.anchorMin.y );
        _bgRect.anchorMax = new Vector2(  0, _bgRect.anchorMax.y );

        _transitionCanvas.SetActive(true);

        yield return new WaitForSeconds(_exitTime - _bgTransition.Length);

        StartCoroutine(_bgTransition.Transition(
            (t) => {

                _bgRect.anchorMin = new Vector2( t - 1,     _bgRect.anchorMin.y );
                _bgRect.anchorMax = new Vector2( t,         _bgRect.anchorMax.y );
            
            },

            (complete) => {

                if ( complete ) SceneManager.LoadScene(PersistentDataManager.run.NextScene());
            
            }
        ));
        
    }

}
