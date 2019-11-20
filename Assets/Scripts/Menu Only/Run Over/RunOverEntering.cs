using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.Random;

public class RunOverEntering : EnteringBehaviour
{
    [SerializeField] private List<Sprite> possibleResults = null;
    [SerializeField] private TextMeshProUGUI timeInGame = null;
    [SerializeField] private TextMeshProUGUI timeInPause = null;

    [Header("Lives")]
    [SerializeField] private Transform livesContainer = null;
    [SerializeField] private GameObject lifePrefab = null;

    [SerializeField] private TextMeshProUGUI livesRemaining = null;
    [SerializeField] private Image result = null;

    private bool inScene = false;
    private Sprite sprev = null;

    protected override void Start() {
        base.Start();
        sprev = possibleResults[Random.Range(0, possibleResults.Count)];
        timeInGame.text = Timers.MINIGAME_STR() + " s";
        timeInPause.text = Timers.PAUSE_STR() + " s";

        foreach (Transform t in livesContainer.transform) {
            GameObject.Destroy(t.gameObject);
        }

        for (int i =0; i < PersistentDataManager.RUN.Lives; i++) {
            Instantiate(lifePrefab, livesContainer);
        }

        StartCoroutine(SetRandom());
    }
    
    protected override void OnStateEnter() {
        SoundManager._PlaySound("fanfare");
    }

    protected override void OnStateExit() {

       inScene = true;
    }
    

    private IEnumerator SetRandom() {
        while (!inScene) {
            Sprite s = possibleResults[Random.Range(0, possibleResults.Count)];
            while(s == sprev) s = possibleResults[Random.Range(0, possibleResults.Count)];
            result.sprite = s;
            sprev = s;
            yield return new WaitForSeconds(0.08f);
        }
    }
}
