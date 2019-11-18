using System.Linq;
using UnityEngine;
using TMPro;

public class HUDDetails : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText = null;

    [Header("Lives")]
    [SerializeField] private Transform livesContainer = null;
    [SerializeField] private GameObject lifePrefab = null;
    public static float time = 0;

    public bool TimerActive { get { return time > 0; } }

    public void UpdateLives() {
        int currLives = livesContainer.childCount;
        int actualLives = PersistentDataManager.RUN.Lives;

        if (currLives > actualLives) {
            for (int i = currLives-1; i >= actualLives; i--) {
                GameObject.Destroy(livesContainer.GetChild(i).gameObject);
            }
        } else if (currLives < actualLives) {
            for (int i = currLives; i < actualLives; i++) {
                Instantiate(lifePrefab, livesContainer);
            }
        }
    }

    public void InitializeTimer() {
        time = PersistentDataManager.RUN.CurrentGame.TimeLimit;
        UpdateTimerUI();
    }

    public void UpdateTimer() {
        time -= Time.deltaTime;
        time = Mathf.Max(0, time);
        UpdateTimerUI();
        if (time == 0) {
            PersistentDataManager.RUN.TimeRanOut();
        }
    }

    private void UpdateTimerUI() {
        timerText.text = string.Format("{0:N1}", time);
    }
}
