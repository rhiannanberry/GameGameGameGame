using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameExiting : GameStateBehaviourNew
{
    [Header("Master Transition")]
    [SerializeField] private float _exitTime = 5f;
    [SerializeField] private GameObject _transitionCanvas;

    [Header("Background Transition")]
    [SerializeField] private TransitionData _bgTransition;
    [SerializeField] private RectTransform _bgRect;

    private float _time = 0f;

    public void OnStateEnterEXITING() {
        Debug.Log("EXITING");
        PersistentDataManager.INSTANCE.SaveMinigameMasterList();
        StartCoroutine(EXIT());
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
