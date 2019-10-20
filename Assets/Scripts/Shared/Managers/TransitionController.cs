using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    private static int? index;
    private static string sceneName;

    public static void CompleteExitToScene() {
        if (index != null) {
            SceneManager.LoadScene((int)index);
        } else if (sceneName != null) {
            SceneManager.LoadScene(sceneName);
        } else {
            Debug.LogWarning("Destination scene name and index do not exist.");
        }
        index = null;
        sceneName = null;
    }

    public static void BeginExitToScene(string s) {
        sceneName = s;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }

    public static void BeginExitToScene(int i) {
        index = i;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }
}
