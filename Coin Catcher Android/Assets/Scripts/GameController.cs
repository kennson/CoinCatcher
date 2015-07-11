using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

public class GameController : MonoBehaviour {

	public Camera cam;
	public GameObject[] ball;
	public float timeLeft;
	public Text timerText;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject splashScreen;
	public GameObject startButton;
	public HatController hatController;
	public Transform[] spawnPoints;

	private float maxWidth;
	private bool playing;

	private const int counterReset = 3;
	public static int counterForAds = counterReset;
	
	void Awake(){
		Application.targetFrameRate = 60;
		
		if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize("39578", false);
		}
	}

	void Start () {
		if (cam == null) {
			cam = Camera.main;		
		}
		playing = false;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = ball[0].renderer.bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		UpdateText ();
	}

	void FixedUpdate() {
		if (playing) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
					timeLeft = 0;		
			}
			UpdateText ();
		}
	}

	public void StartGame(){
		splashScreen.SetActive (false);
		startButton.SetActive (false);
		hatController.ToggleControl (true);
		StartCoroutine (Spawn ());
	}

	IEnumerator Spawn() {
		yield return new WaitForSeconds (2.0f);
		playing = true;
		while (timeLeft > 0) {
			GameObject balls = ball[Random.Range(0, ball.Length)];
			Vector3 spawnPosition = spawnPoints [Random.Range(0, spawnPoints.Length)].position;
			//Vector3 spawnPosition = new Vector3 ( Random.Range (-maxWidth, maxWidth),transform.position.y, 0.0f );
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (balls, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (Random.Range(1.0f, 2.0f));
		}
		yield return new WaitForSeconds (2.0f);
		gameOverText.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		restartButton.SetActive (true);
		counterForAds--;
		if (counterForAds <= 0) {
			resetCounter();
			Advertisement.Show(null, new ShowOptions {
				pause = true,
				resultCallback = result => {
					
				}
			});
		}
	}

	public static void resetCounter() {
		counterForAds = counterReset;
	}

	void UpdateText() {
		timerText.text = "Time Left:\n" + Mathf.RoundToInt (timeLeft);
	}
}
