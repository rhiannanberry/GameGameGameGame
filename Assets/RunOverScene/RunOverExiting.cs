using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RunOverExiting : ExitingBehaviour
{
    [SerializeField] private float _exitTime = 1f;
    [SerializeField] private RectTransform _restartRect;
    [SerializeField] private TransitionData _restartTransitionData;
    [SerializeField] private RectTransform _mainMenuRect;
    [SerializeField] private TransitionData _mainMenuTransitionData;
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
                SceneManager.LoadScene("MainMenuScene");
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
