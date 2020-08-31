using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private GridManager gridManager;
	private AutomataManager autoManager;
	public TMP_Text playText;
	public TMP_Dropdown dropDownValue;
	public Slider fillPercentSlider;
	public Slider speedSlider;
	public TMP_Text generationCountDisplay;

	private bool isPlaying = false;
	private float lastTime = 0f;
	private int numGenerations = 0;

	private void Awake() {
		gridManager = GetComponent<GridManager>();
		autoManager = GetComponent<AutomataManager>();
		playText = GameObject.Find("PlayText (TMP)").GetComponent<TMP_Text>();
		dropDownValue = GameObject.Find("DropdownOptions").GetComponent<TMP_Dropdown>();
	}

	private void Start() {

		Debug.Log(Camera.main.pixelWidth);
		gridManager.GenerateGrid();
	}

	private void Update() {
		if (isPlaying) {
			if (Time.time - lastTime > speedSlider.maxValue - speedSlider.value) {
				string option = dropDownValue.captionText.text;
				if (option == "Life") {
					autoManager.Life();
				}
				else if (option == "Seeds") {
					autoManager.Seeds();
				}
				else if (option == "Highlife") {
					autoManager.HighLife();
				}
				else if (option == "Day N' Night") {
					autoManager.DayAndNight();
				}
				else if (option == "Diamoeba") {
					autoManager.Diamoeba();
				}
				else if (option == "Flock") {
					autoManager.Flock();
				}
				else if (option == "Maze") {
					autoManager.Maze();
				}
				else if (option == "Gems") {
					autoManager.Gems();
				}
				lastTime = Time.time;
				numGenerations++;
			}
		}
		generationCountDisplay.text = numGenerations.ToString();
	}

	public void PausePlayToggle() {
		if (isPlaying) {
			playText.text = "Play";
			isPlaying = false;
		}
		else if (!isPlaying) {
			playText.text = "Pause";
			isPlaying = true;
		}
	}

	public void Play() {
		if (!isPlaying) {
			playText.text = "Pause";
			isPlaying = true;
		}
	}

	public void Pause() {
		Debug.Log("Pause!");
		playText.text = "Play";
		isPlaying = false;
	}

	public void Reset() {
		Debug.Log("Reset!");
		isPlaying = false;
		playText.text = "Play";
		autoManager.ResetGrid();
		numGenerations = 0;
	}

	public void Randomize() {
		Debug.Log("Randomized!");
		autoManager.RandomFillGrid(false, "noSeed", (int)fillPercentSlider.value);
	}
}