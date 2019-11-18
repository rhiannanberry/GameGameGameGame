using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class GameManager : MinigameBehaviour {

	public static GameManager gm;

	[Tooltip("If not set, the player will default to the gameObject tagged as Player.")]
	public GameObject player;

	[Header("UI Elements")]
	public TextMeshProUGUI currentScore;
	public TextMeshProUGUI scoreToWin;


	private Health playerHealth;
	private bool inGame = false;


	override protected void Start () {
		base.Start();
		if (gm == null) 
			gm = gameObject.GetComponent<GameManager>();

		if (player == null) {
			player = GameObject.FindWithTag("Player");
		}

		playerHealth = player.GetComponent<Health>();

		UpdateUI();
	}

	protected override void OnStateEnter() {
		inGame = true;
    }

    protected override void OnStateExit() {
        inGame = false;
    }

	void Update () {
		if (inGame == false) return;

		if (PersistentDataManager.RUN.CurrentGame.ScoreMet()) {
			PersistentDataManager.RUN.GameWon();
		}
	}


	public void Collect() {
		PersistentDataManager.RUN.CurrentGame.CurrentScore += 1;
		UpdateUI();
	}

	private void UpdateUI() {
		currentScore.text = PersistentDataManager.RUN.CurrentGame.CurrentScore.ToString();
		scoreToWin.text = PersistentDataManager.RUN.CurrentGame.ScoreToWin.ToString();
	}
}
