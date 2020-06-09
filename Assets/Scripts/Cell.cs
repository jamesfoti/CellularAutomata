using UnityEngine;

public class Cell : MonoBehaviour {

	public enum States {Dead, Alive}
	public States currentState;
	public States nextState;
	public int rowPos;
	public int colPos;

	private GridManager grid;
	private AutomataManager autoManager;
	public SpriteRenderer render;


	private void Awake() {
		render = GetComponent<SpriteRenderer>();
		grid = FindObjectOfType<GridManager>();
		autoManager = FindObjectOfType<AutomataManager>();
	}

	private void Start() {
		// Every cell starts out dead for now
		currentState = States.Dead;
		render.color = autoManager.deadColor;
	}

	void OnMouseDown() {
		Debug.Log("Clicked on [" + grid.numRows + ", " + grid.numCols + "]");
		if (currentState == States.Alive) {
			currentState = States.Dead;
		}
		else if (currentState == States.Dead) {
			currentState = States.Alive;
		}
		autoManager.UpdateGridColors();
	}

}

