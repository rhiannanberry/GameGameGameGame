using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RunOverExiting : ExitingBehaviour
{
    public static bool restarting = false;
    public TransitionData data;

    protected override void OnStateEnter() {
        StartCoroutine(EXIT());
    }

    protected override void OnStateExit() {}

    IEnumerator EXIT() {
        yield return new WaitForSeconds(data.Length+.1f);
        SceneLoader._CompleteExitToScene();
    }
}
