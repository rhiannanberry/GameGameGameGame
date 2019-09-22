using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RunOverExiting : ExitingBehaviour
{
    [SerializeField] private RectTransform _restartRect = null;
    [SerializeField] private TransitionData _restartTransitionData = null;
    [SerializeField] private RectTransform _mainMenuRect = null;
    [SerializeField] private TransitionData _mainMenuTransitionData = null;
    public static bool restarting = false;

    protected override void OnStateEnter() {
        Debug.Log("EXITING");
        
        if (restarting) {
            StartCoroutine(RestartRun());
        } else {
            StartCoroutine(GoToMainMenu());
        }
    }

    protected override void OnStateExit() {
        
    }

    IEnumerator GoToMainMenu() {
        StartCoroutine( _mainMenuTransitionData.Transition( 
            (t) => {
                _mainMenuRect.localScale = Vector2.one * (1-t);
            },
            (complete) => {
                if (!complete) return;
                PersistentDataManager.run = null;
                SceneManager.LoadScene("Main Menu Scene");
            }
        ));
        yield return null;
    }

    IEnumerator RestartRun() {
        _restartRect.gameObject.SetActive(true);
        _restartRect.anchorMin = new Vector2 ( _restartRect.anchorMin.x, 1);
        StartCoroutine( _restartTransitionData.Transition( 
            (t) => {
                _restartRect.anchorMin = new Vector2( _restartRect.anchorMin.x, 1 - t);
            },
            (complete) => {
                if (!complete) return;
                PersistentDataManager.run.ResetRun();
                SceneManager.LoadScene(PersistentDataManager.run.CurrentScene());
            }
        ));
        yield return null;
    }


}
