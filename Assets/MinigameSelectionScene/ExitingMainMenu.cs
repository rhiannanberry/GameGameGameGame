using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ExitingMainMenu : ExitingBehaviour
{
    private static string _sceneName;
    
    [SerializeField] private RectTransform _rt;
    [SerializeField] private TransitionData _transitionData;

    protected override void OnStateEnter() {
        _rt.gameObject.GetComponent<Image>().enabled = true;
        StartCoroutine( _transitionData.Transition( 
            (t) => {
                _rt.anchorMin = new Vector2( _rt.anchorMin.x,   1 - t );
                //_rt.anchorMax = new Vector2( _rt.anchorMax.x,   t );
            },
            (complete) => {
                if (complete) SceneManager.LoadScene(_sceneName);
            }));
    }

    protected override void OnStateExit() {

    }

    private IEnumerator EXIT() {
    
        StartCoroutine(_transitionData.StartTransitionSlide(_rt));

        yield return new WaitForSeconds(_transitionData.Length);

        SceneManager.LoadScene(_sceneName);

    }


    public static void BeginExitToScene(string sceneName) {
        _sceneName = sceneName;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }
}
