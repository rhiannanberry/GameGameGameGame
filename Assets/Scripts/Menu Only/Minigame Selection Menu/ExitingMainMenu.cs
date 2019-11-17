using System.Collections;
using UnityEngine;

public class ExitingMainMenu : ExitingBehaviour
{
    public float _exitTime = 1.5f;

    protected override void OnStateEnter() {
        StartCoroutine(EXIT());
    }

    protected override void OnStateExit() {

    }

    IEnumerator EXIT() {
        yield return new WaitForSeconds(_exitTime);
        SceneLoader._CompleteExitToScene();
    }

    public static void StartNewQuickplayRun() {
        PersistentDataManager.INSTANCE.CreateNewRun(PersistentDataManager.minigameMasterList.RandomReorder());
        SceneLoader._BeginExitToScene(PersistentDataManager.run.CurrentGame.SceneName);
    }

    public static void GotoCustomPlay() {
        SceneLoader._BeginExitToCustomPlay();
    }
}
