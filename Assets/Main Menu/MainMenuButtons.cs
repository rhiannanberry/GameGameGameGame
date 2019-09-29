using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private TransitionData _mainMenuEnterTransition = null;
    [SerializeField] private Transform _mainMenuTransform = null;
    [SerializeField] private Transform _settingsTransform = null;
    [SerializeField] private GameObject _ExitButton = null;
    // Start is called before the first frame update
    void Start()
    {
        #if !UNITY_STANDALONE
            _ExitButton.SetActive(false);
        #endif

        _settingsTransform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettings() {
        _settingsTransform.gameObject.SetActive(true);

        StartCoroutine( _mainMenuEnterTransition.Transition(
            (t) => {
                _mainMenuTransform.localScale = Vector2.one - Vector2.one * t ;
                _settingsTransform.localScale = Vector2.one * t ;
            },
            (complete) => {
                if (!complete) return;
                
                GameStateManager.INSTANCE.TriggerStateChange(GameState.INSCENE);
            }
        ));
    }

    public void ExitGame() {
        #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
