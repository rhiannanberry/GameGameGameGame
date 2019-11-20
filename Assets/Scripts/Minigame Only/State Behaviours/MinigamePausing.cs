using UnityEngine;
using TMPro;

public class MinigamePausing : PauseBehaviour
{
    [SerializeField] private GameObject _pauseCanvas = null;
    [SerializeField] private TransitionData _mainMenuEnterTransition = null;
    [SerializeField] private Transform _mainMenuTransform = null;
    [SerializeField] private Transform _settingsTransform = null;
    [SerializeField] private Transform _creditsTransform = null;

    
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _title = null;
    [SerializeField] private TextMeshProUGUI _author = null;
    [SerializeField] private TextMeshProUGUI _description = null;
    [SerializeField] private TextMeshProUGUI _timeLimit = null;
    private bool _paused = false;

    protected override void Start() {
        base.Start();
        
        

        _pauseCanvas.SetActive(false);
    }

    protected override void OnStateEnter() {
        _paused = true;
        Minigame m = PersistentDataManager.RUN.CurrentGame;
        _title.text = m.Name;
        _author.text = "Made by: " + m.Author;
        _description.text = m.Description;
        _timeLimit.text = m.TimeLimit + " Seconds";
        _pauseCanvas.SetActive(true);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (_paused) {
                ReturnToGame();
            } else {
                GameStateManager.INSTANCE.TriggerStateChange(GameState.INPAUSE);
            }
        }
    }

    public void Pause() {
        if (!_paused) GameStateManager.INSTANCE.TriggerStateChange(GameState.INPAUSE);
    }

    protected override void OnStateExit() {
        _pauseCanvas.SetActive(false);
        _paused = false;
    }

    public void ExitRun() {
        PersistentDataManager.RUN.ExitEarly();
    }

    public void ReturnToGame() {
        GameStateManager.INSTANCE.TriggerStateChange(GameState.INGAME);
    }

    public void OpenSettings() {
        _settingsTransform.gameObject.SetActive(true);
        StartCoroutine(_mainMenuEnterTransition.StartTransitionScale(_mainMenuTransform.GetComponent<RectTransform>(), false, complete => {}));
        StartCoroutine(_mainMenuEnterTransition.StartTransitionScale(_settingsTransform.GetComponent<RectTransform>(), true, complete => {}));
    }

    public void OpenCredits() {
        _creditsTransform.gameObject.SetActive(true);
        StartCoroutine(_mainMenuEnterTransition.StartTransitionScale(_mainMenuTransform.GetComponent<RectTransform>(), false, complete => {}));
        StartCoroutine(_mainMenuEnterTransition.StartTransitionScale(_creditsTransform.GetComponent<RectTransform>(), true, complete => {}));
    }
    
}
