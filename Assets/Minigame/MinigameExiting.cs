using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameExiting : ExitingBehaviour
{
    [SerializeField] private float _exitTime = 5f;
    protected override void OnStateEnter() {
        Debug.Log("EXITING");
        PersistentDataManager.INSTANCE.SaveMinigameMasterList();
        StartCoroutine(EXIT());
    }

    protected override void OnStateExit() {
        
    }

    IEnumerator EXIT() {
        yield return new WaitForSeconds(_exitTime);
        SceneManager.LoadScene(PersistentDataManager.run.NextScene());
    }
}
