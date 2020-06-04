using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float timeDelay;
	public bool isPlaying = false;
	public GridManager gridManager;

	public void Play() {
		Debug.Log("Play!");
		isPlaying = true;
	}

	public void Pause() {
		Debug.Log("Pause!");
		isPlaying = false;
	}

	public void Reset() {
		Debug.Log("Reset!");
		isPlaying = false;
		gridManager.GenerateGrid();
	}

	public IEnumerator ExecuteAfterTime(float time) {
		yield return new WaitForSeconds(time);
	}


}