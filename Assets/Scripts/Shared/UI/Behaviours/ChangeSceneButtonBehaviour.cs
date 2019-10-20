using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ChangeSceneButtonBehaviour : MonoBehaviour
{
    [Scene] public string destination;

    private void Start() {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(() => TransitionController.BeginExitToScene(destination));
    }
}
