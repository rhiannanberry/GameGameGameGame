using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public static int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void BeginExitToScene(int i) {
        index = i;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }
}
