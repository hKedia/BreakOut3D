using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	public static int score = 0;
	public static GameManager instanceOf;
	public int startingScore = 0;
	public static int currentHearts;
	public int maxHearts = 3;
	public Texture heartTextureFull;
	public Texture heartTextureEmpty;

	protected GameManager () {}

	// Use this for initialization
	void Start () {
//		DontDestroyOnLoad (GameManager.Instance);
		if(instanceOf == null){
			instanceOf = (GameManager)this;
		}
		//DontDestroyOnLoad (gameObject);
		currentHearts = maxHearts;
		score = startingScore;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void MissedBall(GameObject ball){
		currentHearts--;
		if (currentHearts < 1) {
			GameOver();
		} else {
			ResetBall(ball);
		}
	}

	void GameOver(){
        SceneManager.LoadScene("GameOver");

	}

	private void ResetBall(GameObject ball){
		Destroy(ball);
		GameObject paddleObj = GameObject.Find ("PlayerPaddle");
		PlayerPaddle playerScript = paddleObj.GetComponent<PlayerPaddle> ();
		playerScript.SpawnBall ();
	}

	void OnGUI(){
		GUI.Label (new Rect (5, 4, 100, 100), "Score: " + score);
		if (currentHearts >= 0) {
			if(currentHearts > 0){
				for (int i = 0; i < currentHearts; i++) {
					GUI.DrawTexture (new Rect (Screen.width - 85 + (i * 27), 5, 25, 25), heartTextureFull);
				}
			}
			if (currentHearts < maxHearts) {
				for (int i = currentHearts; i < maxHearts; i++) {
					GUI.DrawTexture (new Rect (Screen.width - 85 + (i * 27), 5, 25, 25), heartTextureEmpty);
				}
			}
		}
	}
}
