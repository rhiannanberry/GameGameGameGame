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

    public void OpenQuickPlay() {
        ExitingMainMenu.StartNewQuickplayRun();
    }

    public void OpenCustomPlay() {
        ExitingMainMenu.GotoCustomPlay();
    }

    public void OpenSettings() {
        _settingsTransform.gameObject.SetActive(true);
        HideMainMenu();
        StartCoroutine(_mainMenuEnterTransition.StartTransitionScale(_settingsTransform.GetComponent<RectTransform>(), true, complete => {}));
    }

    public void OpenCredits() {
        _creditsTransform.gameObject.SetActive(true);
        HideMainMenu();
        StartCoroutine(_mainMenuEnterTransition.StartTransitionScale(_creditsTransform.GetComponent<RectTransform>(), true, complete => {}));
    }

    private void HideMainMenu() {
        StartCoroutine(_mainMenuEnterTransition.StartTransitionScale(_mainMenuTransform.GetComponent<RectTransform>(), false, complete => {}));
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
