using System.Collections;
using UnityEngine;


public class MinigameExiting : ExitingBehaviour
{
    [Header("Master Transition")]
    [SerializeField] private float _exitTime = 5f;
    
    protected override void OnStateEnter() {
        PersistentDataManager.INSTANCE.SaveMinigameMasterList();
        StartCoroutine(EXIT());
    }

    protected override void OnStateExit() {
        
    }

    IEnumerator EXIT() {
        yield return new WaitForSeconds(_exitTime);
        SceneLoader._LoadScene(PersistentDataManager.run.NextScene());
    }

}
