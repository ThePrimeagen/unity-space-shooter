using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject hazard;

	public Vector3 spawnValues;

	public int initialHazardCount;
	public int levelHazardCountGrowth;

	public float spawnWait;
	public float spawnWaitReduce;
	public float timeBetweenWaves;
	public float startWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	public int estimatedTimeForHazardToReachGrownd;

	public int scoreByDodging;
	public int scoreByDestroying;

	private int level;

	private float timeSinceLastWave;

	private int hazardCount;

	private float currentSpawnWait;

	private int score;

	private bool gameOver;

	public void AddScoreByDestroyingAsteroid() {
		UpdateScore(scoreByDestroying);
	}

	public void AddScoreByDodgingAsteroid() {
		UpdateScore(scoreByDodging);
	}

	public void GameOver() {
		gameOverText.text = "Game Over";
		restartText.text = "Press 'r' for Restart";
		gameOver = true;
	}

	void Start() {
		Restart();
	}

	void Update() {

		if (gameOver) {
			if (Input.GetKeyDown(KeyCode.R)) {
				SceneManager.LoadScene("main");
			}
			return;
		}

		if (timeSinceLastWave > 0) {
			timeSinceLastWave -= Time.deltaTime;
			return;
		}

		StartCoroutine(SpawnWave());
		timeSinceLastWave = GetLevelDuration();
	}

	float GetLevelDuration() {
		float spawnTime = Mathf.Ceil(currentSpawnWait * hazardCount);
		return spawnTime + timeBetweenWaves +
			estimatedTimeForHazardToReachGrownd;
	}

	IEnumerator SpawnWave() {
		for (int i = 0; i < hazardCount && !gameOver; ++i) {
			SpawnHazard();

			yield return new WaitForSeconds(currentSpawnWait);
		}

		if (gameOver) {
			Debug.Log("GameOver at " + level);
		}

		currentSpawnWait -= spawnWaitReduce;
		hazardCount += levelHazardCountGrowth;
		level++;

		Debug.Log("Current Level " + level);
	}

	void SpawnHazard() {
		float x = spawnValues.x;
		Vector3 sPos = new Vector3(Random.Range(-x, x), 0f, spawnValues.z);
		Quaternion sRot = Quaternion.identity;

		Instantiate(hazard, sPos, sRot);
	}

	private void UpdateScore(int scoreToAdd) {
		score += scoreToAdd;
		scoreText.text = "Score " + score;
	}

	private void Restart() {
		level = 0;
		score = 0;
		hazardCount = initialHazardCount;
		timeSinceLastWave = startWait;
		currentSpawnWait = spawnWait;
		scoreText.text = "Score 0";
		restartText.text = "";
		gameOverText.text = "";
		gameOver = false;
	}
}
