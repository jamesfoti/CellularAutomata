using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider progressSlider;
	public TMP_Text progressDisplay;
	
	

	private void Start() {
		Debug.Log("Level started loading!");
		LoadLevel(1);
		Debug.Log("level laoded");
	}

	public void LoadLevel(int sceneIndex) {
		StartCoroutine(LoadAsynchronously(sceneIndex));
	}

	IEnumerator LoadAsynchronously(int sceneIndex) {
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		
		while (!operation.isDone) {
			float progress = Mathf.Clamp01(operation.progress / .9f);
			progressSlider.value = progress;
			progressDisplay.text = progress * 100f + "%";

			yield return null;
		}
	}

}