using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RunOverExiting : ExitingBehaviour
{
    [SerializeField] private float _exitTime = 1f;
    public static bool restarting = false;

    protected override void OnStateEnter() {
        Debug.Log("EXITING");
        
        StartCoroutine(EXIT());
    }

    protected override void OnStateExit() {
        
    }

    

    IEnumerator EXIT() {
        yield return new WaitForSeconds(_exitTime);
        string destination;
        if (restarting) {
            PersistentDataManager.run.ResetRun();
            destination = PersistentDataManager.run.CurrentScene();
        } else {
            PersistentDataManager.run = null;
            destination = "MainMenuScene";
        }

        SceneManager.LoadScene(destination);
    }


}
