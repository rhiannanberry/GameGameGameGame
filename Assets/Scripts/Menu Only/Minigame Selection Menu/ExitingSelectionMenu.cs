using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitingSelectionMenu : ExitingBehaviour
{
    public float _exitTime = 1.5f;
   
    protected override void OnStateEnter() {
        StartCoroutine(EXIT());
    }

    protected override void OnStateExit() {}

    IEnumerator EXIT() {
        yield return new WaitForSeconds(_exitTime);
        SceneLoader._CompleteExitToScene();
    }

}
