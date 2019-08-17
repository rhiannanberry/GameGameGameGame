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

    private float _time = 0;


    protected override void OnStateEnter() {
        _rt.gameObject.GetComponent<Image>().enabled = true;
        StartCoroutine(Transition());
    }


    protected override void OnStateExit() {

    }

    public static void BeginExitToScene(string sceneName) {
        _sceneName = sceneName;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }

    private IEnumerator Transition() {
        

        while (_time <= _transitionData.Length) {

            float scaledTime01 =  _transitionData.GetEased(_time/_transitionData.Length);
            _rt.anchorMax = new Vector2( scaledTime01, _rt.anchorMax.y );
            _time += Time.deltaTime;
            yield return null;
        }
        

        SceneManager.LoadScene(_sceneName);
    }
}
