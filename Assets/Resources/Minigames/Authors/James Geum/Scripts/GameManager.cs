using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MinigameBehaviour {

	public static GameManager gm;

	[Tooltip("If not set, the player will default to the gameObject tagged as Player.")]
	public GameObject player;

	public enum gameStates {Playing, Death, GameOver, BeatLevel};
	public gameStates gameState = gameStates.Playing;

	public int score=0;
	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public GameObject mainCanvas;
	public Text mainScoreDisplay;
	public GameObject gameOverCanvas;
	public Text gameOverScoreDisplay;

	[Tooltip("Only need to set if canBeatLevel is set to true.")]
	public GameObject beatLevelCanvas;

	public AudioSource backgroundMusic;
	public AudioClip gameOverSFX;

	[Tooltip("Only need to set if canBeatLevel is set to true.")]
	public AudioClip beatLevelSFX;

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

		// setup score display
		Collect (0);

		// make other UI inactive
		gameOverCanvas.SetActive (false);
		if (canBeatLevel)
			beatLevelCanvas.SetActive (false);
	}

	protected override void OnStateEnter() {
		inGame = true;
    }

    protected override void OnStateExit() {
        inGame = false;
    }

	void Update () {
		if (inGame == false) return;

		if (canBeatLevel && PersistentDataManager.RUN.CurrentGame.ScoreMet()) {
			PersistentDataManager.RUN.GameWon();
		}
	}


	public void Collect(int amount) {
		PersistentDataManager.RUN.CurrentGame.CurrentScore += amount;
		//score += amount;
		if (canBeatLevel) {
			mainScoreDisplay.text = PersistentDataManager.RUN.CurrentGame.CurrentScore.ToString () + " of "+beatLevelScore.ToString ();
		} else {
			mainScoreDisplay.text = PersistentDataManager.RUN.CurrentGame.CurrentScore.ToString ();
		}

	}
}
