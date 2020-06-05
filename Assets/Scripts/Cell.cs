using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour {

	public enum States {Dead, Alive}
	public States currentState;
	public States nextState;
	public int rowPos;
	public int colPos;
	public GameManager gameManager;
	public GridManager grid;
	public Material material;

	private int neighbors;

	private void Awake() {
		material = GetComponent<SpriteRenderer>().material;
		gameManager = FindObjectOfType<GameManager>();
		grid = FindObjectOfType<GridManager>();
	}

	private void Start() {
		currentState = States.Dead;
		material.color = Color.black; // Every cell starts out dead for now
	}

	private void Update() {
		neighbors = CountNearbyNeighbors();
	}

	public void UpdateCell() {
		if (currentState == States.Dead && neighbors == 3) {
			nextState = States.Alive;
		}
		else if (currentState == States.Alive && (neighbors < 2 || neighbors > 3)) {
			nextState = States.Dead;
		}
		else {
			nextState = currentState;
		}

		currentState = nextState;
		UpdateColors();
	}

	public void UpdateColors() {
		if (currentState == States.Alive) {
			material.color = Color.white;
		}
		else if (currentState == States.Dead) {
			material.color = Color.black;
		}
	}

	public int CountNearbyNeighbors() {
		int count = 0;
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				int row = (rowPos + i + grid.numRows) % grid.numRows;
				int col = (colPos + j + grid.numCols) % grid.numCols;
				
				if (grid.cells[row, col].currentState == Cell.States.Alive) {
					count++;
				}
			}
		}

		if (currentState == States.Alive) {
			// if this cell is alive, then don't include it
			count--;
		}

		return count;


	}


	void OnMouseDown() {
		if (currentState == States.Alive) {
			currentState = States.Dead;
		}
		else if (currentState == States.Dead) {
			currentState = States.Alive;
		}
		UpdateColors();
	}

}

