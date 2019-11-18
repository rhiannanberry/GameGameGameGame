
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Scene] [SerializeField] private string mainMenu = "";
    [Scene] [SerializeField] private string gameSelect = "";
    [Scene] [SerializeField] private string gameOver = "";
    private static SceneLoader INSTANCE;
    private string sceneName;

    void Awake() {
        INSTANCE = this;
    }

    public void LoadScene(string name) { SceneManager.LoadScene(name); }
    public void LoadScene(int index) { SceneManager.LoadScene(index); }
    public void LoadMainMenuScene() { SceneManager.LoadScene(mainMenu); }
    public void LoadGameSelectScene() { SceneManager.LoadScene(gameSelect); }
    public void LoadGameOverScene() { SceneManager.LoadScene(gameOver); }
    public void BeginExitToMainMenu() {
        INSTANCE.sceneName = mainMenu;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }
    public void BeginExitToScene(string s) {
        INSTANCE.sceneName = s;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }

    public static void _LoadScene(string name) { SceneManager.LoadScene(name); }
    public static void _LoadScene(int index) { SceneManager.LoadScene(index); }
    public static void _LoadMainMenuScene() { SceneManager.LoadScene(INSTANCE.mainMenu); }
    public static void _LoadGameSelectScene() { SceneManager.LoadScene(INSTANCE.gameSelect); }
    public static void _LoadGameOverScene() { SceneManager.LoadScene(INSTANCE.gameOver); }
    public static void _BeginExitToCustomPlay() {
        INSTANCE.sceneName = INSTANCE.gameSelect;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }
    public static void _BeginExitToScene(string s) {
        INSTANCE.sceneName = s;
        GameStateManager.INSTANCE.TriggerStateChange(GameState.EXITING);
    }
    public static void _CompleteExitToScene() { SceneManager.LoadScene(INSTANCE.sceneName); }
}
