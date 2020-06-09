using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

	private GameManager gameManager;
	private GridManager gridManager;
	private AutomataManager autoManager;
	private TextMeshProUGUI playText;

	private void Awake() {
		gameManager = GetComponent<GameManager>();
		gridManager = GetComponent<GridManager>();
		autoManager = GetComponent<AutomataManager>();
		playText = GameObject.Find("PlayText (TMP)").GetComponent<TextMeshProUGUI>();
	}

	public void PausePlayToggle() {
		if (gameManager.isPlaying) {
			playText.text = "Play";
			gameManager.isPlaying = false;
		}
		else if (!gameManager.isPlaying) {
			playText.text = "Pause";
			gameManager.isPlaying = true;
		}
	}

	public void Play() {
		if (!gameManager.isPlaying) {
			playText.text = "Pause";
			gameManager.isPlaying = true;
		}
	}

	public void Pause() {
		Debug.Log("Pause!");
		playText.text = "Play";
		gameManager.isPlaying = false;
	}

	public void Reset() {
		Debug.Log("Reset!");
		gameManager.isPlaying = false;
		playText.text = "Play";
		autoManager.ResetGrid();
	}

	public void PseudoRandomize() {
		Debug.Log("Pseudo Randomized!");
		autoManager.PseudoRandFillGrid();
	}

	public void Randomize() {
		Debug.Log("Randomized!");
		autoManager.RandomFillGrid();
	}
}