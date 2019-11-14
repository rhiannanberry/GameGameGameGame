using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsBehaviour : MonoBehaviour
{
    [SerializeField] private TransitionData _mainMenuEnterTransition = null;
    [SerializeField] private Transform _mainMenuTransform = null;
    [SerializeField] private Transform _settingsTransform = null;
    [SerializeField] private Transform _creditsTransform = null;
    [SerializeField] private GameObject _ExitButton = null;
    // Start is called before the first frame update
    void Start()
    {
        #if !UNITY_STANDALONE
            _ExitButton.SetActive(false);
        #endif

        _settingsTransform.gameObject.SetActive(false);
    }

    public void OpenCustomPlay() {
        RectTransform rtMainMenu = _mainMenuTransform.GetComponent<RectTransform>();
       
        Vector2 mainMenuAnchorMin = rtMainMenu.anchorMin;
        Vector2 mainMenuAnchorMax = rtMainMenu.anchorMax;

        StartCoroutine( _mainMenuEnterTransition.Transition(
            (t) => {
                //
                rtMainMenu.anchorMin = mainMenuAnchorMin - Vector2.right*t;
                rtMainMenu.anchorMax = mainMenuAnchorMax - Vector2.right*t;
            },
            (complete) => {
                if (!complete) return;
                SceneManager.LoadScene("MinigameSelectionScene");
            }
        ));
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

    public void OpenCredits() {
        _creditsTransform.gameObject.SetActive(true);

        StartCoroutine( _mainMenuEnterTransition.Transition(
            (t) => {
                _mainMenuTransform.localScale = Vector2.one - Vector2.one * t ;
                _creditsTransform.localScale = Vector2.one * t ;
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
